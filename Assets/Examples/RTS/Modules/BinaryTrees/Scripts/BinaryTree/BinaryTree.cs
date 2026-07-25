using System.Collections.Generic;

namespace Modules.BinaryTrees
{
    public sealed class BinaryTree<T>
    {
        private sealed class Node
        {
            public T value;
            public Node left;
            public Node right;

            public Node(T value)
            {
                this.value = value;
            }
        }

        private Node _root;
        private int _count;
        private readonly IComparer<T> _comparer;

        public int Count => _count;

        public BinaryTree(IComparer<T> comparer = null)
        {
            _comparer = comparer ?? Comparer<T>.Default;
        }

        // -------------------- ADD --------------------

        public bool Add(T value)
        {
            return AddRecursive(ref _root, value);
        }

        private bool AddRecursive(ref Node node, T value)
        {
            if (node == null)
            {
                node = new Node(value);
                _count++;
                return true;
            }

            int cmp = _comparer.Compare(value, node.value);

            if (cmp == 0)
                return false;

            return cmp < 0
                ? AddRecursive(ref node.left, value)
                : AddRecursive(ref node.right, value);
        }

        // -------------------- CONTAINS --------------------

        public bool Contains(T value)
        {
            Node node = _root;

            while (node != null)
            {
                int cmp = _comparer.Compare(value, node.value);

                if (cmp == 0)
                    return true;

                node = cmp < 0 ? node.left : node.right;
            }

            return false;
        }

        // -------------------- REMOVE --------------------

        public bool Remove(T value)
        {
            return RemoveRecursive(ref _root, value);
        }

        private bool RemoveRecursive(ref Node node, T value)
        {
            if (node == null)
                return false;

            int cmp = _comparer.Compare(value, node.value);

            if (cmp < 0)
                return RemoveRecursive(ref node.left, value);

            if (cmp > 0)
                return RemoveRecursive(ref node.right, value);

            // --- найден узел ---

            if (node.left == null)
            {
                node = node.right;
                _count--;
                return true;
            }

            if (node.right == null)
            {
                node = node.left;
                _count--;
                return true;
            }

            Node min = FindMin(node.right);
            node.value = min.value;

            return RemoveRecursive(ref node.right, min.value);
        }

        // -------------------- HELPERS --------------------

        private Node FindMin(Node node)
        {
            while (node.left != null)
                node = node.left;

            return node;
        }

        public void Clear()
        {
            _root = null;
            _count = 0;
        }
    }
}