using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed class SpatialHash<T> where T : notnull
    {
        private readonly float invCellSize;

        private readonly Dictionary<Cell, List<Entry>> cells;
        private readonly Dictionary<T, Handle> lookup;

        private readonly struct Cell : IEquatable<Cell>
        {
            private readonly int X;
            private readonly int Y;
            private readonly int Z;

            public Cell(int x, int y, int z)
            {
                X = x;
                Y = y;
                Z = z;
            }

            public bool Equals(Cell other) => X == other.X && Y == other.Y && Z == other.Z;

            public override int GetHashCode()
            {
                unchecked
                {
                    return (X * 73856093) ^ (Y * 19349663) ^ (Z * 83492791);
                }
            }
        }

        private struct Entry
        {
            public Vector3 Position;
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

        public SpatialHash(float cellSize, int capacity = 128)
        {
            invCellSize = 1f / cellSize;
            cells = new Dictionary<Cell, List<Entry>>(capacity);
            lookup = new Dictionary<T, Handle>(capacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Cell GetCell(Vector3 p)
        {
            int x = Mathf.FloorToInt(p.x * invCellSize);
            int y = Mathf.FloorToInt(p.y * invCellSize);
            int z = Mathf.FloorToInt(p.z * invCellSize);
            return new Cell(x, y, z);
        }

        public bool Insert(T obj, Vector3 position)
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

        public bool Move(T obj, Vector3 newPosition)
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

            RemoveInternal(obj, handle);

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

        public bool Remove(T obj)
        {
            if (!lookup.TryGetValue(obj, out var handle))
                return false;

            RemoveInternal(obj, handle);
            lookup.Remove(obj);
            return true;
        }

        private void RemoveInternal(T obj, Handle handle)
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

        public int QueryRadius(Vector3 center, float radius, T[] buffer)
        {
            int count = 0;
            float sqrRadius = radius * radius;

            int minX = Mathf.FloorToInt((center.x - radius) * invCellSize);
            int maxX = Mathf.FloorToInt((center.x + radius) * invCellSize);
            int minY = Mathf.FloorToInt((center.y - radius) * invCellSize);
            int maxY = Mathf.FloorToInt((center.y + radius) * invCellSize);
            int minZ = Mathf.FloorToInt((center.z - radius) * invCellSize);
            int maxZ = Mathf.FloorToInt((center.z + radius) * invCellSize);

            for (int x = minX; x <= maxX; x++)
            for (int y = minY; y <= maxY; y++)
            for (int z = minZ; z <= maxZ; z++)
            {
                var cell = new Cell(x, y, z);

                if (!cells.TryGetValue(cell, out var list))
                    continue;

                for (int i = 0; i < list.Count; i++)
                {
                    var entry = list[i];

                    Vector3 delta = entry.Position - center;
                    if (delta.sqrMagnitude > sqrRadius)
                        continue;

                    buffer[count++] = entry.Value;

                    if (count >= buffer.Length)
                        return count;
                }
            }

            return count;
        }

        public void Clear()
        {
            cells.Clear();
            lookup.Clear();
        }
    }
}