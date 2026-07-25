using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        private const float EPSILON = 0.01f;

        public int QueryRadius(
            Vector3 target,
            float radius,
            T[] results,
            Predicate<T> predicate)
        {
            if (_root == null || radius <= 0)
                return 0;

            int count = 0;
            float sqrRadius = radius * radius;

            QueryRadiusRecursive(_root, target, sqrRadius, 0, results, ref count, predicate);

            return count;
        }

        private void QueryRadiusRecursive(
            Node node,
            Vector3 target,
            float sqrRadius,
            int depth,
            T[] results,
            ref int count,
            Predicate<T> predicate)
        {
            if (node == null || count == results.Length)
                return;

            Vector3 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            // ✅ проверяем точку
            if (dist <= sqrRadius + EPSILON)
            {
                var values = node.values;

                for (int i = 0; i < values.Count; i++)
                {
                    var v = values[i];

                    if (predicate == null || predicate(v))
                    {
                        results[count++] = v;

                        if (count == results.Length)
                            return;
                    }
                }
            }

            Axis axis = (Axis) (depth % 3);

            float axisDiff = axis switch
            {
                Axis.X => delta.x,
                Axis.Y => delta.y,
                Axis.Z => delta.z,
                _ => 0f
            };

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            QueryRadiusRecursive(primary, target, sqrRadius, depth + 1, results, ref count, predicate);

            // pruning
            if (axisDiff * axisDiff <= sqrRadius)
            {
                QueryRadiusRecursive(secondary, target, sqrRadius, depth + 1, results, ref count, predicate);
            }
        }

        public List<KeyValuePair<Vector3, T>> QueryRadius(Vector3 target, float radius)
        {
            var results = new List<KeyValuePair<Vector3, T>>();

            if (_root == null || radius <= 0)
                return results;

            float sqrRadius = radius * radius;

            QueryRadiusRecursive(_root, target, sqrRadius, 0, results);

            return results;
        }

        private void QueryRadiusRecursive(
            Node node,
            Vector3 target,
            float sqrRadius,
            int depth,
            List<KeyValuePair<Vector3, T>> results)
        {
            if (node == null)
                return;

            Vector3 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            if (dist <= sqrRadius + EPSILON)
            {
                var values = node.values;

                for (int i = 0; i < values.Count; i++)
                {
                    results.Add(new KeyValuePair<Vector3, T>(node.point, values[i]));
                }
            }

            Axis axis = (Axis) (depth % 3);

            float axisDiff = axis switch
            {
                Axis.X => delta.x,
                Axis.Y => delta.y,
                Axis.Z => delta.z,
                _ => 0f
            };

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            QueryRadiusRecursive(primary, target, sqrRadius, depth + 1, results);

            if (axisDiff * axisDiff <= sqrRadius)
            {
                QueryRadiusRecursive(secondary, target, sqrRadius, depth + 1, results);
            }
        }
    }
}