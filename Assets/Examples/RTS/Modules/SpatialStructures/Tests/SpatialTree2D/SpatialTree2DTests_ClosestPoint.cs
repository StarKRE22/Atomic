using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void ClosestPoint_EmptyTree_ReturnsFalse()
        {
            bool result = tree2D.QueryClosest(new Vector2(5, 5), out Vector2 closest, out object value);

            Assert.IsFalse(result);
        }

        [Test]
        public void ClosestPoint_SingleNode_ReturnsSamePointAndValue()
        {
            Vector2 point = new Vector2(5, 5);
            object obj = new object();

            tree2D.Add(point, obj);

            bool result = tree2D.QueryClosest(new Vector2(7, 7), out Vector2 closest, out object value);

            Assert.IsTrue(result);
            Assert.AreEqual(point, closest);
            Assert.AreSame(obj, value);
        }

        [Test]
        public void ClosestPoint_MultipleNodes_FindsNearest()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();
            var e = new object();

            tree2D.Add(new Vector2(2, 3), a);
            tree2D.Add(new Vector2(5, 4), b);
            tree2D.Add(new Vector2(9, 6), c);
            tree2D.Add(new Vector2(4, 7), d);
            tree2D.Add(new Vector2(8, 1), e);

            Vector2 target = new Vector2(6, 5);

            bool result = tree2D.QueryClosest(target, out Vector2 closest, out object value);

            Assert.IsTrue(result);
            Assert.AreEqual(new Vector2(5, 4), closest);
            Assert.AreSame(b, value);
        }

        [Test]
        public void ClosestPoint_TargetExactlyOnNode_ReturnsSamePointAndValue()
        {
            Vector2 exactPoint = new Vector2(3, 3);
            var obj = new object();

            tree2D.Add(new Vector2(5, 5), new object());
            tree2D.Add(new Vector2(7, 7), new object());
            tree2D.Add(exactPoint, obj);

            bool result = tree2D.QueryClosest(new Vector2(3, 3), out Vector2 closest, out object value);

            Assert.IsTrue(result);
            Assert.AreEqual(exactPoint, closest);
            Assert.AreSame(obj, value);
        }

        [Test]
        public void ClosestPoint_CheckDifferentQuadrants()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();

            tree2D.Add(new Vector2(1, 1), a);
            tree2D.Add(new Vector2(-2, 2), b);
            tree2D.Add(new Vector2(3, -3), c);
            tree2D.Add(new Vector2(-4, -4), d);

            tree2D.QueryClosest(new Vector2(0, 0), out Vector2 closest1, out object value1);
            Assert.AreEqual(new Vector2(1, 1), closest1);
            Assert.AreSame(a, value1);

            tree2D.QueryClosest(new Vector2(-3, 3), out Vector2 closest2, out object value2);
            Assert.AreEqual(new Vector2(-2, 2), closest2);
            Assert.AreSame(b, value2);

            tree2D.QueryClosest(new Vector2(4, -4), out Vector2 closest3, out object value3);
            Assert.AreEqual(new Vector2(3, -3), closest3);
            Assert.AreSame(c, value3);

            tree2D.QueryClosest(new Vector2(-5, -5), out Vector2 closest4, out object value4);
            Assert.AreEqual(new Vector2(-4, -4), closest4);
            Assert.AreSame(d, value4);
        }
    }
}