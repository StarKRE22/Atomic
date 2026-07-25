using System;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        public bool Add(Vector3 point, T value)
        {
            return value == null
                ? throw new ArgumentNullException(nameof(value))
                : AddRecursive(ref _root, point, value, 0);
        }

        private bool AddRecursive(ref Node node, Vector3 point, T value, int depth)
        {
            if (node == null)
            {
                node = Pool.Rent(point, value);
                _count++;
                return true;
            }

            if (Approximately(node.point, point))
            {
                var values = node.values;

                if (values.Contains(value))
                    return false;

                values.Add(value);
                _count++;
                return true;
            }

            Axis axis = (Axis)(depth % 3);

            bool goLeft = axis switch
            {
                Axis.X => point.x <= node.point.x,
                Axis.Y => point.y <= node.point.y,
                Axis.Z => point.z <= node.point.z,
                _ => false
            };

            return goLeft
                ? AddRecursive(ref node.left, point, value, depth + 1)
                : AddRecursive(ref node.right, point, value, depth + 1);
        }
    }
}