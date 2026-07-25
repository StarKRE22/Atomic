using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree<T>
    {
        private const float EPS = 0.0001f;
        
        internal sealed class Node
        {
            public Vector3 point;
            public List<T> values;
            public Node left;
            public Node right;
        }

        internal enum Axis
        {
            X = 0,
            Y = 1,
            Z = 2
        }
        
        internal Node _root = null;
        private int _count;

        public int Count => _count;
        
        public bool Contains(Vector3 point)
        {
            return FindNode(_root, point, 0) != null;
        }
        
        public bool TryGetValues(Vector3 point, out IReadOnlyList<T> values)
        {
            Node node = FindNode(_root, point, 0);

            if (node != null)
            {
                values = node.values;
                return true;
            }

            values = null;
            return false;
        }
        
        public bool Move(Vector3 from, Vector3 to, T value)
        {
            if (!Remove(from, value))
                return false;

            Add(to, value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool Approximately(Vector3 a, Vector3 b)
        {
            return (a - b).sqrMagnitude <= EPS * EPS;
        }

        private Node FindNode(Node node, Vector3 point, int depth)
        {
            while (node != null)
            {
                if (node.point == point)
                    return node;

                Axis axis = (Axis)(depth % 3);

                bool goLeft = axis switch
                {
                    Axis.X => point.x <= node.point.x,
                    Axis.Y => point.y <= node.point.y,
                    Axis.Z => point.z <= node.point.z,
                    _ => false
                };

                node = goLeft ? node.left : node.right;
                depth++;
            }

            return null;
        }
        
        public override string ToString()
        {
            var sb = new StringBuilder();
            PrintTree(_root, sb, 0, "Root");
            return sb.ToString();
        }

        private void PrintTree(Node node, StringBuilder sb, int depth, string prefix)
        {
            if (node == null)
                return;

            sb.AppendLine(new string(' ', depth * 4) +
                          $"{prefix}:[{node.point}] → Count:{node.values.Count}");

            PrintTree(node.left, sb, depth + 1, "L");
            PrintTree(node.right, sb, depth + 1, "R");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int CompareAxis(Node a, Node b, Axis axis)
        {
            return axis switch
            {
                Axis.X => a.point.x.CompareTo(b.point.x),
                Axis.Y => a.point.y.CompareTo(b.point.y),
                Axis.Z => a.point.z.CompareTo(b.point.z),
                _ => 0
            };
        }
    }
}