using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        public bool Remove(Vector2 point, T value)
        {
            return RemoveRecursive(ref _root, point, value, 0);
        }

        private bool RemoveRecursive(ref Node node, Vector2 point, T value, int depth)
        {
            if (node == null)
                return false;

            Vector2 delta = point - node.point;

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

            Axis axis = (Axis)(depth % 2);

            bool goLeft = axis switch
            {
                Axis.X => delta.x <= 0f,
                Axis.Y => delta.y <= 0f,
                _ => false
            };

            return goLeft
                ? RemoveRecursive(ref node.left, point, value, depth + 1)
                : RemoveRecursive(ref node.right, point, value, depth + 1);
        }

        private void RemoveNode(ref Node node, int depth)
        {
            Axis axis = (Axis)(depth % 2);

            if (node.left == null && node.right == null)
            {
                node = null;
                return;
            }

            if (node.right != null)
            {
                Node min = FindMin(node.right, axis, depth + 1);

                node.point = min.point;

                // 🔥 копируем значения
                node.values.Clear();
                node.values.AddRange(min.values);

                // 🔥 удаляем ВСЮ ноду min
                RemoveNode(ref node.right, depth + 1);
            }
            else
            {
                Node max = FindMax(node.left, axis, depth + 1);

                node.point = max.point;

                node.values.Clear();
                node.values.AddRange(max.values);

                RemoveNode(ref node.left, depth + 1);
            }
        }
    }
}