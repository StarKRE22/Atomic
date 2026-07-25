using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTree2DTests
    {
        [Test]
        public void FindValues_EmptyTree_ReturnsEmptyList()
        {
            var result = tree2D.FindAllInRadius(new Vector2(5, 5), 3f);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FindValues_SingleNode_InsideRadius_ReturnsValue()
        {
            Vector2 point = new Vector2(5, 5);
            var value = new object();

            tree2D.Add(point, value);

            var result = tree2D.FindAllInRadius(new Vector2(6, 6), 2f);

            Assert.AreEqual(1, result.Count);
            Assert.Contains(value, result);
        }

        [Test]
        public void FindValues_SingleNode_OutsideRadius_ReturnsEmptyList()
        {
            tree2D.Add(new Vector2(2, 2), new object());

            var result = tree2D.FindAllInRadius(new Vector2(10, 10), 3f);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FindValues_MultipleNodes_FindsAllWithinRadius()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();

            tree2D.Add(new Vector2(1, 1), a);
            tree2D.Add(new Vector2(5, 5), b);
            tree2D.Add(new Vector2(7, 7), c);
            tree2D.Add(new Vector2(9, 9), d);

            var result = tree2D.FindAllInRadius(new Vector2(6, 6), 3f);

            Assert.AreEqual(2, result.Count);
            Assert.Contains(b, result);
            Assert.Contains(c, result);
        }

        [Test]
        public void FindValues_DuplicatePoints_ReturnsAllValues()
        {
            Vector2 point = new Vector2(5, 5);

            var a = new object();
            var b = new object();

            tree2D.Add(point, a);
            tree2D.Add(point, b);

            var result = tree2D.FindAllInRadius(point, 1f);

            Assert.AreEqual(2, result.Count);
            Assert.Contains(a, result);
            Assert.Contains(b, result);
        }

        [Test]
        public void FindValues_RadiusIsZero_ReturnsEmptyList()
        {
            tree2D.Add(new Vector2(4, 4), new object());

            var result = tree2D.FindAllInRadius(new Vector2(4, 4), 0f);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void FindValues_NegativeRadius_ReturnsEmptyList()
        {
            tree2D.Add(new Vector2(4, 4), new object());

            var result = tree2D.FindAllInRadius(new Vector2(4, 4), -1f);

            Assert.AreEqual(0, result.Count);
        }
    }
}