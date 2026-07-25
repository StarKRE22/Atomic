using UnityEngine;
// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        public bool QueryClosest(Vector2 target, out Vector2 closestPoint, out T closestValue)
        {
            if (_root == null)
            {
                closestPoint = default;
                closestValue = default;
                return false;
            }

            Node bestNode = null;
            float bestDist = float.MaxValue;

            QueryClosestRecursive(_root, target, ref bestNode, ref bestDist, 0);

            closestPoint = bestNode.point;
            var values = bestNode.values;
            closestValue = values[0];

            return true;
        }

        private void QueryClosestRecursive(Node node, Vector2 target, ref Node best, ref float bestDist, int depth)
        {
            if (node == null)
                return;

            Vector2 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            if (dist < bestDist)
            {
                best = node;
                bestDist = dist;
            }

            Axis axis = (Axis)(depth % 2);

            float axisDiff = axis == Axis.X ? delta.x : delta.y;

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            QueryClosestRecursive(primary, target, ref best, ref bestDist, depth + 1);

            if (axisDiff * axisDiff <= bestDist)
                QueryClosestRecursive(secondary, target, ref best, ref bestDist, depth + 1);
        }
    }
}