using System.Collections.Generic;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed class QuadTree<T>
    {
        // =========================
        // HANDLE (наружный!)
        // =========================

        private struct Handle
        {
            internal Node node;
            internal int index;
        }

        // =========================
        // ENTRY
        // =========================

        private struct Entry
        {
            public T item;
            public Vector2 pos;
        }

        // =========================
        // NODE
        // =========================

        private sealed class Node
        {
            public float minX, minY, maxX, maxY;
            public int depth;

            public Entry[] items;
            public int count;

            public Node nw, ne, sw, se;
            public bool divided;

            public Node parent;

            public Node(float minX, float minY, float maxX, float maxY, int capacity, int depth, Node parent)
            {
                this.minX = minX;
                this.minY = minY;
                this.maxX = maxX;
                this.maxY = maxY;
                this.depth = depth;
                this.parent = parent;

                items = new Entry[capacity];
                count = 0;
            }

            public bool Contains(Vector2 p)
            {
                return p.x >= minX && p.x <= maxX &&
                       p.y >= minY && p.y <= maxY;
            }

            public bool Overlaps(float minX, float minY, float maxX, float maxY)
            {
                return !(minX > this.maxX || maxX < this.minX ||
                         minY > this.maxY || maxY < this.minY);
            }
        }

        // =========================
        // FIELDS
        // =========================

        private readonly int capacity;
        private readonly int maxDepth;

        private Node root;

        // внешний lookup (можно убрать если не нужен)
        private readonly Dictionary<T, Handle> lookup;

        // =========================
        // CTOR
        // =========================

        public QuadTree(Vector2 min, Vector2 max, int capacity = 8, int maxDepth = 8)
        {
            this.capacity = capacity;
            this.maxDepth = maxDepth;

            root = new Node(min.x, min.y, max.x, max.y, capacity, 0, null);

            lookup = new Dictionary<T, Handle>(capacity * 4);
        }

        // =========================
        // INSERT
        // =========================

        public bool Insert(T item, Vector2 pos)
        {
            if (lookup.ContainsKey(item))
                return false;

            var handle = Insert(root, item, pos);
            if (handle.node == null)
                return false;

            lookup[item] = handle;
            return true;
        }

        private Handle Insert(Node node, T item, Vector2 pos)
        {
            if (!node.Contains(pos))
                return default;

            // если есть место — кладём сюда
            if (node.count < capacity || node.depth >= maxDepth)
            {
                int index = node.count;

                node.items[index] = new Entry { item = item, pos = pos };
                node.count++;

                return new Handle { node = node, index = index };
            }

            // делим
            if (!node.divided)
                Subdivide(node);

            return InsertIntoChild(node, item, pos);
        }

        private Handle InsertIntoChild(Node node, T item, Vector2 pos)
        {
            float midX = (node.minX + node.maxX) * 0.5f;
            float midY = (node.minY + node.maxY) * 0.5f;

            if (pos.x < midX)
                return pos.y < midY ? Insert(node.sw, item, pos) : Insert(node.nw, item, pos);
            else
                return pos.y < midY ? Insert(node.se, item, pos) : Insert(node.ne, item, pos);
        }

        private void Subdivide(Node node)
        {
            float midX = (node.minX + node.maxX) * 0.5f;
            float midY = (node.minY + node.maxY) * 0.5f;

            node.nw = new Node(node.minX, midY, midX, node.maxY, capacity, node.depth + 1, node);
            node.ne = new Node(midX, midY, node.maxX, node.maxY, capacity, node.depth + 1, node);
            node.sw = new Node(node.minX, node.minY, midX, midY, capacity, node.depth + 1, node);
            node.se = new Node(midX, node.minY, node.maxX, midY, capacity, node.depth + 1, node);

            node.divided = true;

            // перераспределяем
            for (int i = node.count - 1; i >= 0; i--)
            {
                var e = node.items[i];
                node.count--;

                var handle = InsertIntoChild(node, e.item, e.pos);
                lookup[e.item] = handle;
            }
        }

        // =========================
        // REMOVE O(1)
        // =========================

        public bool Remove(T item)
        {
            if (!lookup.TryGetValue(item, out var handle))
                return false;

            var node = handle.node;
            int index = handle.index;

            int last = node.count - 1;
            var lastEntry = node.items[last];

            node.items[index] = lastEntry;
            node.count--;

            if (!EqualityComparer<T>.Default.Equals(lastEntry.item, item))
            {
                lookup[lastEntry.item] = new Handle { node = node, index = index };
            }

            lookup.Remove(item);

            TryMerge(node);

            return true;
        }

        // =========================
        // MERGE
        // =========================

        private void TryMerge(Node node)
        {
            while (node != null && node.divided)
            {
                int total =
                    node.nw.count + node.ne.count +
                    node.sw.count + node.se.count;

                if (total > capacity)
                    return;

                // собираем обратно
                Copy(node, node.nw);
                Copy(node, node.ne);
                Copy(node, node.sw);
                Copy(node, node.se);

                node.nw = node.ne = node.sw = node.se = null;
                node.divided = false;

                node = node.parent;
            }
        }

        private void Copy(Node target, Node source)
        {
            for (int i = 0; i < source.count; i++)
            {
                var e = source.items[i];

                int idx = target.count++;
                target.items[idx] = e;

                lookup[e.item] = new Handle { node = target, index = idx };
            }
        }

        // =========================
        // MOVE (умный)
        // =========================

        public bool Move(T item, Vector2 newPos)
        {
            if (!lookup.TryGetValue(item, out var handle))
                return false;

            var node = handle.node;

            if (node.Contains(newPos))
            {
                node.items[handle.index].pos = newPos;
                return true;
            }

            Remove(item);
            return Insert(item, newPos);
        }

        // =========================
        // QUERY NON-ALLOC
        // =========================

        public int QueryRadius(Vector2 center, float radius, List<T> results)
        {
            float r2 = radius * radius;

            Query(root, center, r2,
                center.x - radius, center.y - radius,
                center.x + radius, center.y + radius,
                results);

            return results.Count;
        }

        private void Query(Node node, Vector2 center, float r2,
            float minX, float minY, float maxX, float maxY,
            List<T> results)
        {
            if (!node.Overlaps(minX, minY, maxX, maxY))
                return;

            for (int i = 0; i < node.count; i++)
            {
                var e = node.items[i];
                if ((e.pos - center).sqrMagnitude <= r2)
                    results.Add(e.item);
            }

            if (!node.divided)
                return;

            Query(node.nw, center, r2, minX, minY, maxX, maxY, results);
            Query(node.ne, center, r2, minX, minY, maxX, maxY, results);
            Query(node.sw, center, r2, minX, minY, maxX, maxY, results);
            Query(node.se, center, r2, minX, minY, maxX, maxY, results);
        }

        // =========================
        // CLEAR
        // =========================

        public void Clear()
        {
            root = new Node(root.minX, root.minY, root.maxX, root.maxY, capacity, 0, null);
            lookup.Clear();
        }
    }
}