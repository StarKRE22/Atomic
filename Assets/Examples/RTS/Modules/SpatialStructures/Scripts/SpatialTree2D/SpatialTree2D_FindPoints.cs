using System;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        public List<T> FindAllInRadius(Vector2 target, float radius)
        {
            var result = new List<T>();

            if (_root == null || radius <= 0f)
                return result;

            float sqrRadius = radius * radius;
            FindAllInRadiusRecursive(_root, target, sqrRadius, 0, result);

            return result;
        }

        private void FindAllInRadiusRecursive(Node node, Vector2 target, float sqrRadius, int depth, List<T> result)
        {
            if (node == null)
                return;

            Vector2 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            if (dist <= sqrRadius + EPS)
            {
                var values = node.values;
                for (int i = 0; i < values.Count; i++)
                    result.Add(values[i]);
            }

            Axis axis = (Axis)(depth % 2);

            float axisDiff = axis == Axis.X ? delta.x : delta.y;
            float axisDiffSqr = axisDiff * axisDiff;

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            FindAllInRadiusRecursive(primary, target, sqrRadius, depth + 1, result);

            if (axisDiffSqr <= sqrRadius)
                FindAllInRadiusRecursive(secondary, target, sqrRadius, depth + 1, result);
        }
        
        public int FindAllInRadius(Vector2 target, float radius, T[] results, Predicate<T> predicate = null)
        {
            if (_root == null || radius <= 0f || results == null || results.Length == 0)
                return 0;

            int count = 0;
            float sqrRadius = radius * radius;

            FindAllInRadiusRecursive(_root, target, sqrRadius, 0, results, ref count, predicate);

            return count;
        }

        private void FindAllInRadiusRecursive(
            Node node,
            Vector2 target,
            float sqrRadius,
            int depth,
            T[] results,
            ref int count,
            Predicate<T> predicate)
        {
            if (node == null || count == results.Length)
                return;

            Vector2 delta = target - node.point;
            float dist = delta.sqrMagnitude;

            if (dist <= sqrRadius + EPS)
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

            Axis axis = (Axis)(depth % 2);

            float axisDiff = axis == Axis.X ? delta.x : delta.y;
            float axisDiffSqr = axisDiff * axisDiff;

            Node primary = axisDiff <= 0f ? node.left : node.right;
            Node secondary = axisDiff <= 0f ? node.right : node.left;

            FindAllInRadiusRecursive(primary, target, sqrRadius, depth + 1, results, ref count, predicate);

            if (axisDiffSqr <= sqrRadius)
            {
                FindAllInRadiusRecursive(secondary, target, sqrRadius, depth + 1, results, ref count, predicate);
            }
        }
    }
}