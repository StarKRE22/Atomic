namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        public void Clear()
        {
            ClearRecursive(_root);
            _root = null;
            _count = 0;
        }

        private void ClearRecursive(Node node)
        {
            if (node == null)
                return;

            ClearRecursive(node.left);
            ClearRecursive(node.right);

            Pool.Return(node);
        }
    }
}