using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTreeTests
    {
        [Test]
        public void ClosestPoint_EmptyTree_ReturnsFalse()
        {
            // Act
            bool result = _tree.QueryClosest(new Vector3(1, 1, 1), out Vector3 closest, out string value);

            // Assert
            Assert.IsFalse(result, "В пустом дереве не должно быть ближайших точек.");
            Assert.AreEqual(default(Vector3), closest, "Ближайшая точка должна быть значением по умолчанию.");
            Assert.IsNull(value, "Значение должно быть null.");
        }

        [Test]
        public void ClosestPoint_FindsNearest()
        {
            // Arrange
            _tree.Add(new Vector3(1, 2, 3), "A");
            _tree.Add(new Vector3(5, 5, 5), "B");
            _tree.Add(new Vector3(-1, -1, -1), "C");

            Vector3 target = new Vector3(2, 2, 2);

            // Act
            bool result = _tree.QueryClosest(target, out Vector3 closest, out string value);

            // Assert
            Assert.IsTrue(result, "Должна быть найдена ближайшая точка.");
            Assert.AreEqual(new Vector3(1, 2, 3), closest, "Ближайшая точка должна быть (1,2,3).");
            Assert.AreEqual("A", value, "Значение должно соответствовать 'A'.");
        }

        [Test]
        public void ClosestPoint_ExactMatch()
        {
            // Arrange
            _tree.Add(new Vector3(4, 4, 4), "Exact");

            // Act
            bool result = _tree.QueryClosest(new Vector3(4, 4, 4), out Vector3 closest, out string value);

            // Assert
            Assert.IsTrue(result, "Точная точка должна быть найдена.");
            Assert.AreEqual(new Vector3(4, 4, 4), closest, "Ближайшая точка должна быть (4,4,4).");
            Assert.AreEqual("Exact", value, "Значение должно быть 'Exact'.");
        }

        [Test]
        public void ClosestPoint_HandlesDifferentDepths()
        {
            // Arrange
            _tree.Add(new Vector3(0, 2, 0), "Root");
            _tree.Add(new Vector3(-3, -5, -5), "Left");
            _tree.Add(new Vector3(7, 4, 6), "Right");
            _tree.Add(new Vector3(3, 3, 3), "MiddleRight");
            Debug.Log(_tree);
            

            Vector3 target = new Vector3(4, 4, 4);

            // Act
            bool result = _tree.QueryClosest(target, out Vector3 closest, out string value);

            // Assert
            Assert.IsTrue(result, "Должна быть найдена ближайшая точка.");
            Assert.AreEqual(new Vector3(3, 3, 3), closest, "Ближайшая точка должна быть (3,3,3).");
            Assert.AreEqual("MiddleRight", value, "Значение должно соответствовать 'MiddleRight'.");
        }

        [Test]
        public void ClosestPoint_3DSearch()
        {
            // Arrange
            _tree.Add(new Vector3(1, 5, 3), "A");
            _tree.Add(new Vector3(2, 8, 2), "B");
            _tree.Add(new Vector3(9, 7, 5), "C");
            _tree.Add(new Vector3(4, 1, 6), "D");

            Vector3 target = new Vector3(3, 2, 5);

            // Act
            bool result = _tree.QueryClosest(target, out Vector3 closest, out string value);

            // Assert
            Assert.IsTrue(result, "Должна быть найдена ближайшая точка.");
            Assert.AreEqual(new Vector3(4, 1, 6), closest, "Ближайшая точка должна быть (4,1,6).");
            Assert.AreEqual("D", value, "Значение должно соответствовать 'D'.");
        }
    }
}