using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        public bool Add(Vector2 point, T value)
        {
            return AddRecursive(ref _root, point, value, 0);
        }

        private bool AddRecursive(ref Node node, Vector2 point, T value, int depth)
        {
            if (node == null)
            {
                node = Pool.Rent(point, value);
                _count++;
                return true;
            }

            Vector2 delta = point - node.point;

            if (delta.sqrMagnitude <= EPS * EPS)
            {
                node.values.Add(value);
                _count++;
                return true;
            }

            Axis axis = (Axis)(depth % 2);

            bool goLeft = axis switch
            {
                Axis.X => delta.x <= 0f,
                Axis.Y => delta.y <= 0f,
                _ => false
            };

            return goLeft
                ? AddRecursive(ref node.left, point, value, depth + 1)
                : AddRecursive(ref node.right, point, value, depth + 1);
        }
    }
}