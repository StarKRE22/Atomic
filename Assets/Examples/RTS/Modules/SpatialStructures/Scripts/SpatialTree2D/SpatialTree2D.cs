using System.Collections.Generic;
using System.Text;
using UnityEngine;

// ReSharper disable TailRecursiveCall

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        internal sealed class Node
        {
            public Vector2 point;
            public List<T> values;
            public Node left;
            public Node right;
        }

        internal enum Axis
        {
            X = 0,
            Y = 1
        }

        internal Node _root;
        private int _count;

        public int Count => _count;

        private const float EPS = 0.0001f;
        
        public bool Contains(Vector2 point)
        {
            return FindNode(_root, point, 0) != null;
        }
        
        public bool Move(Vector2 from, Vector2 to, T value)
        {
            if (!Remove(from, value))
                return false;

            Add(to, value);
            return true;
        }
        
        public bool TryGetValues(Vector2 point, out IReadOnlyList<T> values)
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

      
        private Node FindNode(Node node, Vector2 point, int depth)
        {
            while (node != null)
            {
                Vector2 delta = point - node.point;

                if (delta.sqrMagnitude <= EPS * EPS)
                    return node;

                Axis axis = (Axis)(depth % 2);

                bool goLeft = axis switch
                {
                    Axis.X => delta.x <= 0f,
                    Axis.Y => delta.y <= 0f,
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
    }
}