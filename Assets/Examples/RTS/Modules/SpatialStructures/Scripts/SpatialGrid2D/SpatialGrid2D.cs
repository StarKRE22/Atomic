using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Profiling;

namespace Modules.SpatialStructures
{
    public sealed class SpatialGrid2D<T>
    {
#if ENABLE_PROFILER
        private static readonly ProfilerMarker InsertMarker = new($"SpatialGrid<{typeof(T).Name}>.Insert");
        private static readonly ProfilerMarker MoveMarker = new($"SpatialGrid<{typeof(T).Name}>.Move");
        private static readonly ProfilerMarker RemoveMarker = new($"SpatialGrid<{typeof(T).Name}>.Remove");
        private static readonly ProfilerMarker QueryMarker = new($"SpatialGrid<{typeof(T).Name}>.QueryRadius");
        private static readonly ProfilerMarker ClearMarker = new($"SpatialGrid<{typeof(T).Name}>.Clear");
#endif

        public float CellSize => cellSize;
        public int SizeX => sizeX;
        public int SizeY => sizeY;

        private readonly float cellSize;
        private readonly float invCellSize;

        private readonly int sizeX;
        private readonly int sizeY;

        private readonly Cell[] cells;
        private readonly Dictionary<T, Handle> lookup;

        // ---------------- DATA ----------------

        internal struct Entry
        {
            public T Value;
            public Vector2 Position;
        }

        private struct Handle
        {
            public int CellIndex;
            public int IndexInCell;
        }

        public sealed class Cell
        {
            public int Count => count;

            internal Entry[] items;
            internal int count;

            internal Cell(int capacity)
            {
                items = new Entry[capacity];
                count = 0;
            }

            public T GetValue(int index) => items[index].Value;
            public Vector2 GetPosition(int index) => items[index].Position;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal int Add(in Entry entry)
            {
                if (count == items.Length)
                    Grow();

                items[count] = entry;
                return count++;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            internal void RemoveAtSwapBack(int index, out Entry moved)
            {
                int last = count - 1;
                moved = items[last];

                items[index] = moved;
                count--;
            }

            private void Grow()
            {
                int newSize = items.Length == 0 ? 4 : items.Length * 2;
                Array.Resize(ref items, newSize);
            }
        }

        // ---------------- CTOR ----------------

        public SpatialGrid2D(int sizeX, int sizeY, float cellSize, int cellCapacity = 4)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;

            this.cellSize = cellSize;
            this.invCellSize = 1f / cellSize;

            int total = sizeX * sizeY;

            cells = new Cell[total];
            for (int i = 0; i < total; i++)
                cells[i] = new Cell(cellCapacity);

            lookup = new Dictionary<T, Handle>(total);
        }

        // ---------------- INDEX ----------------

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetIndex(Vector2 pos, out int index)
        {
            int x = (int) (pos.x * invCellSize);
            int y = (int) (pos.y * invCellSize);

            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
            {
                index = -1;
                return false;
            }

            index = x + sizeX * y;
            return true;
        }

        // ---------------- INSERT ----------------

        public bool Insert(T obj, Vector2 position)
        {
#if ENABLE_PROFILER
            using (InsertMarker.Auto())
#endif
            {
                if (lookup.ContainsKey(obj))
                    return false;

                if (!TryGetIndex(position, out int cellIndex))
                    return false;

                ref var cell = ref cells[cellIndex];

                int indexInCell = cell.Add(new Entry
                {
                    Value = obj,
                    Position = position
                });

                lookup[obj] = new Handle
                {
                    CellIndex = cellIndex,
                    IndexInCell = indexInCell
                };

                return true;
            }
        }

        // ---------------- MOVE ----------------

        public bool Move(T obj, Vector2 newPosition)
        {
#if ENABLE_PROFILER
            using (MoveMarker.Auto())
#endif
            {
                if (!lookup.TryGetValue(obj, out var item))
                    return false;

                if (!TryGetIndex(newPosition, out int newCell))
                    return false;

                int oldCell = item.CellIndex;

                if (oldCell == newCell)
                {
                    cells[oldCell].items[item.IndexInCell].Position = newPosition;
                    return true;
                }

                RemoveInternal(obj, item);

                ref var cell = ref cells[newCell];

                int index = cell.Add(new Entry
                {
                    Value = obj,
                    Position = newPosition
                });

                lookup[obj] = new Handle
                {
                    CellIndex = newCell,
                    IndexInCell = index
                };

                return true;
            }
        }

        // ---------------- REMOVE ----------------

        public bool Remove(T obj)
        {
#if ENABLE_PROFILER
            using (RemoveMarker.Auto())
#endif
            {
                if (!lookup.TryGetValue(obj, out Handle item))
                    return false;

                RemoveInternal(obj, item);
                lookup.Remove(obj);

                return true;
            }
        }

        private void RemoveInternal(T obj, Handle item)
        {
            ref var cell = ref cells[item.CellIndex];

            cell.RemoveAtSwapBack(item.IndexInCell, out var moved);

            if (!EqualityComparer<T>.Default.Equals(moved.Value, obj))
            {
                lookup[moved.Value] = new Handle
                {
                    CellIndex = item.CellIndex,
                    IndexInCell = item.IndexInCell
                };
            }
        }

        // ---------------- QUERY ----------------

        public int QueryRadius(Vector2 center, float radius, T[] buffer)
        {
#if ENABLE_PROFILER
            using (QueryMarker.Auto())
#endif
            {
                float r2 = radius * radius;
                int count = 0;

                int minX = (int) ((center.x - radius) * invCellSize);
                int maxX = (int) ((center.x + radius) * invCellSize);
                int minY = (int) ((center.y - radius) * invCellSize);
                int maxY = (int) ((center.y + radius) * invCellSize);

                for (int x = minX; x <= maxX; x++)
                {
                    if (x < 0 || x >= sizeX) continue;

                    for (int y = minY; y <= maxY; y++)
                    {
                        if (y < 0 || y >= sizeY) continue;

                        ref var cell = ref cells[x + sizeX * y];

                        var items = cell.items;
                        int len = cell.count;

                        for (int i = 0; i < len; i++)
                        {
                            ref var entry = ref items[i];

                            if ((entry.Position - center).sqrMagnitude > r2)
                                continue;

                            if (count >= buffer.Length)
                                return count;

                            buffer[count++] = entry.Value;
                        }
                    }
                }

                return count;
            }
        }

        // ---------------- CLEAR ----------------

        public void Clear()
        {
#if ENABLE_PROFILER
            using (ClearMarker.Auto())
#endif
            {
                for (int i = 0; i < cells.Length; i++)
                    cells[i].count = 0;

                lookup.Clear();
            }
        }

        // ---------------- DEBUG ----------------

        public int GetCellCount(int x, int y)
        {
            if (x < 0 || x >= sizeX || y < 0 || y >= sizeY)
                return 0;

            return cells[x + sizeX * y].count;
        }

        public T GetCellValue(int x, int y, int index)
        {
            ref var cell = ref cells[x + sizeX * y];
            return cell.items[index].Value;
        }

        public Vector2 GetCellPosition(int x, int y, int index)
        {
            ref var cell = ref cells[x + sizeX * y];
            return cell.items[index].Position;
        }

        public Cell GetCell(int x, int y)
        {
            return x < 0 || x >= sizeX || y < 0 || y >= sizeY
                ? null
                : cells[x + sizeX * y];
        }

        public IEnumerable<(Vector2Int cell, int count)> GetAllCells()
        {
            for (int y = 0; y < sizeY; y++)
            {
                int row = y * sizeX;

                for (int x = 0; x < sizeX; x++)
                {
                    var cell = cells[row + x];
                    int count = cell.count;
                    yield return (new Vector2Int(x, y), count);
                }
            }
        }
    }
}