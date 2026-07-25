using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        public void Rebuild(IEnumerable<KeyValuePair<Vector3, T>> pairs)
        {
            Node newRoot = null;
            int newCount = 0;

            foreach (var (point, value) in pairs)
            {
                AddRecursive(ref newRoot, point, value, 0);
                newCount++;
            }

            Clear();

            _root = newRoot;
            _count = newCount;
        }
    }
}