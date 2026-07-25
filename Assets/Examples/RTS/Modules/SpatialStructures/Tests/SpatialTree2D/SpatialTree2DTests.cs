using NUnit.Framework;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        private SpatialTree2D<object> tree2D;

        [SetUp]
        public void Setup()
        {
            tree2D = new SpatialTree2D<object>();
        }
    }
}