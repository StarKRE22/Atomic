using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed class SpatialGrid<T>
    {
        public float CellSize => cellSize;
        public int SizeX => sizeX;
        public int SizeY => sizeY;
        public int SizeZ => sizeZ;

        private readonly float cellSize;
        private readonly float invCellSize;

        private readonly int sizeX;
        private readonly int sizeY;
        private readonly int sizeZ;

        private readonly Cell[] cells;
        private readonly Dictionary<T, Handle> lookup;

        internal struct Entry
        {
            public T Value;
            public Vector3 Position;
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
            public Vector3 GetPosition(int index) => items[index].Position;

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

        public SpatialGrid(int sizeX, int sizeY, int sizeZ, float cellSize, int initialCapacity = 4)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            this.sizeZ = sizeZ;

            this.cellSize = cellSize;
            this.invCellSize = 1f / cellSize;

            int total = sizeX * sizeY * sizeZ;

            cells = new Cell[total];
            for (int i = 0; i < total; i++)
                cells[i] = new Cell(initialCapacity);

            lookup = new Dictionary<T, Handle>(total);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool TryGetIndex(Vector3 pos, out int index)
        {
            int x = (int)(pos.x * invCellSize);
            int y = (int)(pos.y * invCellSize);
            int z = (int)(pos.z * invCellSize);

            if (x < 0 || x >= sizeX ||
                y < 0 || y >= sizeY ||
                z < 0 || z >= sizeZ)
            {
                index = -1;
                return false;
            }

            index = x + sizeX * (y + sizeY * z);
            return true;
        }

        public bool Insert(T obj, Vector3 position)
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

        public bool Move(T obj, Vector3 newPosition)
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

        public bool Remove(T obj)
        {
            if (!lookup.TryGetValue(obj, out Handle item))
                return false;

            RemoveInternal(obj, item);
            lookup.Remove(obj);

            return true;
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

        public int QueryRadius(Vector3 center, float radius, T[] buffer)
        {
            float r2 = radius * radius;
            int count = 0;

            int minX = (int)((center.x - radius) * invCellSize);
            int maxX = (int)((center.x + radius) * invCellSize);
            int minY = (int)((center.y - radius) * invCellSize);
            int maxY = (int)((center.y + radius) * invCellSize);
            int minZ = (int)((center.z - radius) * invCellSize);
            int maxZ = (int)((center.z + radius) * invCellSize);

            for (int x = minX; x <= maxX; x++)
            {
                if (x < 0 || x >= sizeX) continue;

                for (int y = minY; y <= maxY; y++)
                {
                    if (y < 0 || y >= sizeY) continue;

                    for (int z = minZ; z <= maxZ; z++)
                    {
                        if (z < 0 || z >= sizeZ) continue;

                        ref var cell = ref cells[x + sizeX * (y + sizeY * z)];

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
            }

            return count;
        }

        public void Clear()
        {
            for (int i = 0; i < cells.Length; i++)
                cells[i].count = 0;

            lookup.Clear();
        }

        public int GetCellCount(int x, int y, int z)
        {
            if (x < 0 || x >= sizeX ||
                y < 0 || y >= sizeY ||
                z < 0 || z >= sizeZ)
                return 0;

            return cells[x + sizeX * (y + sizeY * z)].count;
        }

        public T GetCellValue(int x, int y, int z, int index)
        {
            return cells[x + sizeX * (y + sizeY * z)].items[index].Value;
        }

        public Vector3 GetCellPosition(int x, int y, int z, int index)
        {
            return cells[x + sizeX * (y + sizeY * z)].items[index].Position;
        }

        public Cell GetCell(int x, int y, int z)
        {
            return x < 0 || x >= sizeX ||
                   y < 0 || y >= sizeY ||
                   z < 0 || z >= sizeZ
                ? default
                : cells[x + sizeX * (y + sizeY * z)];
        }
    }
}