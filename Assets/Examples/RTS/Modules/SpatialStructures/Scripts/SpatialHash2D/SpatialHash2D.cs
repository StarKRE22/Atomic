using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Unity.Profiling;

namespace Modules.SpatialStructures
{
    public sealed class SpatialHash2D<T> where T : notnull
    {
        public float CellSize => cellSize;

        private readonly float cellSize;
        private readonly float invCellSize;

        private readonly Dictionary<Cell, List<Entry>> cells;
        private readonly Dictionary<T, Handle> lookup;

#if ENABLE_PROFILER
        private static readonly ProfilerMarker InsertMarker = new($"SpatialHash<{typeof(T).Name}>.Insert");
        private static readonly ProfilerMarker MoveMarker = new($"SpatialHash<{typeof(T).Name}>.Move");
        private static readonly ProfilerMarker RemoveMarker = new($"SpatialHash<{typeof(T).Name}>.Remove");
        private static readonly ProfilerMarker QueryRadiusMarker = new($"SpatialHash<{typeof(T).Name}>.QueryRadius");
        private static readonly ProfilerMarker QueryClosestMarker = new($"SpatialHash<{typeof(T).Name}>.QueryClosest");
        private static readonly ProfilerMarker ClearMarker = new($"SpatialHash<{typeof(T).Name}>.Clear");
#endif

        private readonly struct Cell : IEquatable<Cell>
        {
            internal readonly int X;
            internal readonly int Y;

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }

            public bool Equals(Cell other) => X == other.X && Y == other.Y;

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 73856093) ^ (Y * 19349663);
                }
            }
        }

        private struct Entry
        {
            public Vector2 Position;
            public T Value;
        }

        private readonly struct Handle
        {
            public readonly Cell Cell;
            public readonly int Index;

            public Handle(Cell cell, int index)
            {
                Cell = cell;
                Index = index;
            }
        }

        public SpatialHash2D(float cellSize, int capacity = 128)
        {
            this.cellSize = cellSize;
            this.invCellSize = 1f / cellSize;
            this.cells = new Dictionary<Cell, List<Entry>>(capacity);
            this.lookup = new Dictionary<T, Handle>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Cell GetCell(Vector2 p)
        {
            int x = Mathf.FloorToInt(p.x * invCellSize);
            int y = Mathf.FloorToInt(p.y * invCellSize);
            return new Cell(x, y);
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

                Cell cell = GetCell(position);
                if (!cells.TryGetValue(cell, out var list))
                {
                    list = new List<Entry>(8);
                    cells[cell] = list;
                }

                int index = list.Count;
                list.Add(new Entry
                {
                    Position = position,
                    Value = obj
                });

                lookup[obj] = new Handle(cell, index);
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
                if (!lookup.TryGetValue(obj, out var handle))
                    return false;

                Cell newCell = GetCell(newPosition);

                if (handle.Cell.Equals(newCell))
                {
                    var list = cells[handle.Cell];
                    var entry = list[handle.Index];
                    entry.Position = newPosition;
                    list[handle.Index] = entry;
                    return true;
                }

                RemoveInternal(handle);

                if (!cells.TryGetValue(newCell, out var newList))
                {
                    newList = new List<Entry>(8);
                    cells[newCell] = newList;
                }

                int newIndex = newList.Count;
                newList.Add(new Entry
                {
                    Position = newPosition,
                    Value = obj
                });

                lookup[obj] = new Handle(newCell, newIndex);

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
                if (!lookup.TryGetValue(obj, out Handle handle))
                    return false;

                RemoveInternal(handle);
                lookup.Remove(obj);
                return true;
            }
        }

        private void RemoveInternal(Handle handle)
        {
            var list = cells[handle.Cell];

            int lastIndex = list.Count - 1;
            var last = list[lastIndex];

            if (handle.Index != lastIndex)
            {
                list[handle.Index] = last;
                lookup[last.Value] = new Handle(handle.Cell, handle.Index);
            }

            list.RemoveAt(lastIndex);

            if (list.Count == 0)
                cells.Remove(handle.Cell);
        }

        // ---------------- QUERY ----------------

        public int QueryRadius(Vector2 center, float radius, T[] buffer)
        {
#if ENABLE_PROFILER
            using (QueryRadiusMarker.Auto())
#endif
            {
                int count = 0;
                float sqrRadius = radius * radius;

                int minX = Mathf.FloorToInt((center.x - radius) * invCellSize);
                int maxX = Mathf.FloorToInt((center.x + radius) * invCellSize);
                int minY = Mathf.FloorToInt((center.y - radius) * invCellSize);
                int maxY = Mathf.FloorToInt((center.y + radius) * invCellSize);

                for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                {
                    var cell = new Cell(x, y);

                    if (!cells.TryGetValue(cell, out var list))
                        continue;

                    for (int i = 0; i < list.Count; i++)
                    {
                        var entry = list[i];

                        Vector2 delta = entry.Position - center;
                        if (delta.sqrMagnitude > sqrRadius)
                            continue;

                        buffer[count++] = entry.Value;

                        if (count >= buffer.Length)
                            return count;
                    }
                }

                return count;
            }
        }
        
        public int QueryPoint(Vector2 point, T[] buffer)
        {
#if ENABLE_PROFILER
            using (QueryRadiusMarker.Auto())
#endif
            {
                int count = 0;

                Cell cell = GetCell(point);
                if (!cells.TryGetValue(cell, out var list))
                    return 0;

                for (int i = 0; i < list.Count; i++)
                {
                    var entry = list[i];

                    // точное совпадение (можно заменить на epsilon при необходимости)
                    if (entry.Position != point)
                        continue;

                    buffer[count++] = entry.Value;
                    if (count >= buffer.Length)
                        return count;
                }

                return count;
            }
        }

        public bool QueryClosest(
            Vector2 center,
            float radius,
            Func<T, bool> predicate,
            out T closest
        )
        {
#if ENABLE_PROFILER
            using (QueryClosestMarker.Auto())
#endif
            {
                closest = default;

                float sqrRadius = radius * radius;
                float bestSqr = float.MaxValue;

                int minX = Mathf.FloorToInt((center.x - radius) * invCellSize);
                int maxX = Mathf.FloorToInt((center.x + radius) * invCellSize);
                int minY = Mathf.FloorToInt((center.y - radius) * invCellSize);
                int maxY = Mathf.FloorToInt((center.y + radius) * invCellSize);

                for (int x = minX; x <= maxX; x++)
                for (int y = minY; y <= maxY; y++)
                {
                    var cell = new Cell(x, y);

                    if (!cells.TryGetValue(cell, out var list))
                        continue;

                    for (int i = 0; i < list.Count; i++)
                    {
                        var entry = list[i];
                        if (!predicate(entry.Value))
                            continue;

                        Vector2 delta = entry.Position - center;
                        float sqr = delta.sqrMagnitude;

                        if (sqr > sqrRadius)
                            continue;

                        if (sqr >= bestSqr)
                            continue;

                        bestSqr = sqr;
                        closest = entry.Value;
                    }
                }

                return bestSqr < float.MaxValue;
            }
        }

        // ---------------- UTILS ----------------

        public void Clear()
        {
#if ENABLE_PROFILER
            using (ClearMarker.Auto())
#endif
            {
                cells.Clear();
                lookup.Clear();
            }
        }

        public IEnumerable<(Vector2Int cell, int count)> GetAllCells()
        {
            foreach ((Cell key, List<Entry> entries) in cells)
                yield return (new Vector2Int(key.X, key.Y), entries.Count);
        }
    }
}