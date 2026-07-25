using System.Collections.Generic;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public partial class SpatialTree<T>
    {
        private static class Pool
        {
            private static readonly Stack<Node> _pool = new();

            public static Node Rent(Vector3 point, T value)
            {
                if (_pool.TryPop(out var node))
                {
                    node.point = point;

                    if (node.values == null)
                        node.values = new List<T>();
                    else
                        node.values.Clear();

                    node.values.Add(value);

                    node.left = null;
                    node.right = null;

                    return node;
                }

                return new Node
                {
                    point = point,
                    values = new List<T> { value }
                };
            }

            public static void Return(Node node)
            {
                node.values?.Clear();
                node.left = null;
                node.right = null;

                _pool.Push(node);
            }
        }
    }
}