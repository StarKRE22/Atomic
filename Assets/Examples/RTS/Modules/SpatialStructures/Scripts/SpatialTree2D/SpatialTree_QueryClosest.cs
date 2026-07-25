using System;
using UnityEngine;
// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        public bool QueryClosest(Vector3 target, out Vector3 closestPoint, out T closestValue)
        {
            if (_root == null)
            {
                closestPoint = default;
                closestValue = default;
                return false;
            }

            Node bestNode = null;
            float bestDist = float.MaxValue;

            QueryClosest(_root, target, ref bestNode, ref bestDist, 0);

            closestPoint = bestNode.point;

            // 👉 берём первый элемент (или можно кастомную стратегию)
            closestValue = bestNode.values[0];

            return true;
        }
        
        private void QueryClosest(Node node, Vector3 target, ref Node best, ref float bestDist, int depth)
        {
            if (node == null)
                return;

            Vector3 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            if (dist < bestDist)
            {
                best = node;
                bestDist = dist;
            }

            Axis axis = (Axis)(depth % 3);

            float axisDiff = axis switch
            {
                Axis.X => delta.x,
                Axis.Y => delta.y,
                Axis.Z => delta.z,
                _ => 0f
            };

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            // сначала идём в "правильную" сторону
            QueryClosest(primary, target, ref best, ref bestDist, depth + 1);

            // pruning
            if (axisDiff * axisDiff <= bestDist)
                QueryClosest(secondary, target, ref best, ref bestDist, depth + 1);
        }
    }
}