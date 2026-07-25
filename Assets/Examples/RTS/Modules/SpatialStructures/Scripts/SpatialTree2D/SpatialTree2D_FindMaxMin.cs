// ReSharper disable TailRecursiveCall
namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2D<T>
    {
        internal Node FindMin(Node node, Axis axis, int depth)
        {
            if (node == null)
                return null;

            Axis current = (Axis)(depth % 2);

            if (current == axis)
                return node.left == null
                    ? node
                    : FindMin(node.left, axis, depth + 1);

            Node min = node;

            Node left = FindMin(node.left, axis, depth + 1);
            if (left != null && CompareAxis(left, min, axis) < 0)
                min = left;

            Node right = FindMin(node.right, axis, depth + 1);
            if (right != null && CompareAxis(right, min, axis) < 0)
                min = right;

            return min;
        }

        internal Node FindMax(Node node, Axis axis, int depth)
        {
            if (node == null)
                return null;

            Axis current = (Axis)(depth % 2);

            if (current == axis)
                return node.right == null
                    ? node
                    : FindMax(node.right, axis, depth + 1);

            Node max = node;

            Node left = FindMax(node.left, axis, depth + 1);
            if (left != null && CompareAxis(left, max, axis) > 0)
                max = left;

            Node right = FindMax(node.right, axis, depth + 1);
            if (right != null && CompareAxis(right, max, axis) > 0)
                max = right;

            return max;
        }

        private static int CompareAxis(Node a, Node b, Axis axis)
        {
            return axis switch
            {
                Axis.X => a.point.x.CompareTo(b.point.x),
                Axis.Y => a.point.y.CompareTo(b.point.y),
                _ => 0
            };
        }
    }
}