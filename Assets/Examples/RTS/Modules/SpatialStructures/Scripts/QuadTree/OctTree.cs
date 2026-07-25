using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed class OctTree<T>
    {
        private struct Entry
        {
            public T item;
            public Vector3 pos;
        }

        private readonly float minX, minY, minZ;
        private readonly float maxX, maxY, maxZ;

        private readonly int capacity;
        private readonly int maxDepth;

        private readonly Entry[] items;
        private int count;

        private OctTree<T>[] children;
        private bool divided;
        private readonly int depth;

        public OctTree(Vector3 min, Vector3 max, int capacity = 8, int maxDepth = 8, int depth = 0)
        {
            minX = min.x;
            minY = min.y;
            minZ = min.z;

            maxX = max.x;
            maxY = max.y;
            maxZ = max.z;

            this.capacity = capacity;
            this.maxDepth = maxDepth;
            this.depth = depth;

            items = new Entry[capacity];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Contains(Vector3 p)
        {
            return p.x >= minX && p.x <= maxX &&
                   p.y >= minY && p.y <= maxY &&
                   p.z >= minZ && p.z <= maxZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool Overlaps(float qMinX, float qMinY, float qMinZ,
                              float qMaxX, float qMaxY, float qMaxZ)
        {
            return !(qMinX > maxX || qMaxX < minX ||
                     qMinY > maxY || qMaxY < minY ||
                     qMinZ > maxZ || qMaxZ < minZ);
        }

        public bool Insert(T item, Vector3 pos)
        {
            if (!Contains(pos))
                return false;

            if (count < capacity)
            {
                items[count++] = new Entry { item = item, pos = pos };
                return true;
            }

            if (depth >= maxDepth)
                return false;

            if (!divided)
                Subdivide();

            return InsertIntoChild(item, pos);
        }

        private bool InsertIntoChild(T item, Vector3 pos)
        {
            float midX = (minX + maxX) * 0.5f;
            float midY = (minY + maxY) * 0.5f;
            float midZ = (minZ + maxZ) * 0.5f;

            int index =
                (pos.x >= midX ? 1 : 0) |
                (pos.y >= midY ? 2 : 0) |
                (pos.z >= midZ ? 4 : 0);

            return children[index].Insert(item, pos);
        }

        private void Subdivide()
        {
            float midX = (minX + maxX) * 0.5f;
            float midY = (minY + maxY) * 0.5f;
            float midZ = (minZ + maxZ) * 0.5f;

            children = new OctTree<T>[8];

            children[0] = new OctTree<T>(new Vector3(minX, minY, minZ), new Vector3(midX, midY, midZ), capacity, maxDepth, depth + 1);
            children[1] = new OctTree<T>(new Vector3(midX, minY, minZ), new Vector3(maxX, midY, midZ), capacity, maxDepth, depth + 1);
            children[2] = new OctTree<T>(new Vector3(minX, midY, minZ), new Vector3(midX, maxY, midZ), capacity, maxDepth, depth + 1);
            children[3] = new OctTree<T>(new Vector3(midX, midY, minZ), new Vector3(maxX, maxY, midZ), capacity, maxDepth, depth + 1);

            children[4] = new OctTree<T>(new Vector3(minX, minY, midZ), new Vector3(midX, midY, maxZ), capacity, maxDepth, depth + 1);
            children[5] = new OctTree<T>(new Vector3(midX, minY, midZ), new Vector3(maxX, midY, maxZ), capacity, maxDepth, depth + 1);
            children[6] = new OctTree<T>(new Vector3(minX, midY, midZ), new Vector3(midX, maxY, maxZ), capacity, maxDepth, depth + 1);
            children[7] = new OctTree<T>(new Vector3(midX, midY, midZ), new Vector3(maxX, maxY, maxZ), capacity, maxDepth, depth + 1);

            divided = true;

            for (int i = 0; i < count; i++)
            {
                var e = items[i];
                InsertIntoChild(e.item, e.pos);
            }

            count = 0;
        }

        public int QueryAABB(Vector3 min, Vector3 max, List<T> result, bool clear = false)
        {
            if (clear) result.Clear();
            return QueryAABBInternal(min.x, min.y, min.z, max.x, max.y, max.z, result);
        }

        private int QueryAABBInternal(float qMinX, float qMinY, float qMinZ,
                                     float qMaxX, float qMaxY, float qMaxZ,
                                     List<T> result)
        {
            if (!Overlaps(qMinX, qMinY, qMinZ, qMaxX, qMaxY, qMaxZ))
                return 0;

            int found = 0;

            for (int i = 0; i < count; i++)
            {
                var p = items[i].pos;

                if (p.x >= qMinX && p.x <= qMaxX &&
                    p.y >= qMinY && p.y <= qMaxY &&
                    p.z >= qMinZ && p.z <= qMaxZ)
                {
                    result.Add(items[i].item);
                    found++;
                }
            }

            if (divided)
            {
                for (int i = 0; i < 8; i++)
                    found += children[i].QueryAABBInternal(qMinX, qMinY, qMinZ, qMaxX, qMaxY, qMaxZ, result);
            }

            return found;
        }

        public int QueryRadius(Vector3 center, float radius, List<T> result, bool clear = false)
        {
            if (clear) result.Clear();

            float r2 = radius * radius;

            float minX = center.x - radius;
            float minY = center.y - radius;
            float minZ = center.z - radius;
            float maxX = center.x + radius;
            float maxY = center.y + radius;
            float maxZ = center.z + radius;

            return QueryRadiusInternal(center, r2, minX, minY, minZ, maxX, maxY, maxZ, result);
        }

        private int QueryRadiusInternal(Vector3 center, float r2,
            float qMinX, float qMinY, float qMinZ,
            float qMaxX, float qMaxY, float qMaxZ,
            List<T> result)
        {
            if (!Overlaps(qMinX, qMinY, qMinZ, qMaxX, qMaxY, qMaxZ))
                return 0;

            int found = 0;

            for (int i = 0; i < count; i++)
            {
                var e = items[i];
                if ((e.pos - center).sqrMagnitude <= r2)
                {
                    result.Add(e.item);
                    found++;
                }
            }

            if (divided)
            {
                for (int i = 0; i < 8; i++)
                    found += children[i].QueryRadiusInternal(center, r2, qMinX, qMinY, qMinZ, qMaxX, qMaxY, qMaxZ, result);
            }

            return found;
        }

        public void Clear()
        {
            count = 0;

            if (!divided)
                return;

            for (int i = 0; i < 8; i++)
                children[i].Clear();

            divided = false;
        }
    }
}