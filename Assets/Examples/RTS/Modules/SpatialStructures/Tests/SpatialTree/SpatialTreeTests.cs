using NUnit.Framework;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTreeTests
    {
        private SpatialTree<string> _tree;

        [SetUp]
        public void Setup()
        {
            _tree = new SpatialTree<string>();
        }
    }
}