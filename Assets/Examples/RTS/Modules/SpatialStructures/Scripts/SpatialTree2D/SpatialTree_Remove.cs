using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        public bool Remove(Vector3 point, T value)
        {
            return RemoveRecursive(ref _root, point, value, 0);
        }

        private bool RemoveRecursive(ref Node node, Vector3 point, T value, int depth)
        {
            if (node == null)
                return false;

            Vector3 delta = point - node.point;
            if (delta.sqrMagnitude <= EPS * EPS)
            {
                if (!node.values.Remove(value))
                    return false;

                _count--;

                if (node.values.Count > 0)
                    return true;

                RemoveNode(ref node, depth);
                return true;
            }

            Axis axis = (Axis) (depth % 3);

            bool goLeft = axis switch
            {
                Axis.X => delta.x <= 0f,
                Axis.Y => delta.y <= 0f,
                Axis.Z => delta.z <= 0f,
                _ => false
            };

            return goLeft
                ? RemoveRecursive(ref node.left, point, value, depth + 1)
                : RemoveRecursive(ref node.right, point, value, depth + 1);
        }

        private void RemoveNode(ref Node node, int depth)
        {
            Axis axis = (Axis) (depth % 3);

            if (node.left == null && node.right == null)
            {
                Pool.Return(node);
                node = null;
                return;
            }

            if (node.right != null)
            {
                Node min = FindMin(node.right, axis, depth + 1);

                node.point = min.point;

                node.values.Clear();
                node.values.AddRange(min.values);
                
                RemoveNode(ref node.right, min, depth + 1);
            }
            else
            {
                Node max = FindMax(node.left, axis, depth + 1);

                node.point = max.point;

                node.values.Clear();
                node.values.AddRange(max.values);

                RemoveNode(ref node.left, max, depth + 1);
            }
        }

        private void RemoveNode(ref Node node, Node target, int depth)
        {
            if (node == null)
                return;

            if (ReferenceEquals(node, target))
            {
                if (node.left == null && node.right == null)
                {
                    Pool.Return(node);
                    node = null;
                    return;
                }

                RemoveNode(ref node, depth);
                return;
            }

            Axis axis = (Axis) (depth % 3);

            Vector3 delta = target.point - node.point;

            bool goLeft = axis switch
            {
                Axis.X => delta.x <= 0f,
                Axis.Y => delta.y <= 0f,
                Axis.Z => delta.z <= 0f,
                _ => false
            };

            if (goLeft)
                RemoveNode(ref node.left, target, depth + 1);
            else
                RemoveNode(ref node.right, target, depth + 1);
        }
    }
}