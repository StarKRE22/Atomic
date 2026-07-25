using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTreeTests
    {
        [Test]
        public void FindAllInRadius_EmptyTree_ReturnsEmpty()
        {
            // Act
            var result = _tree.QueryRadius(new Vector3(0, 0, 0), 5);

            // Assert
            Assert.IsEmpty(result, "В пустом дереве поиск должен вернуть пустой список.");
        }

        [Test]
        public void FindAllInRadius_FindsCorrectPoints()
        {
            // Arrange
            _tree.Add(new Vector3(1, 1, 1), "A");
            _tree.Add(new Vector3(3, 3, 3), "B");
            _tree.Add(new Vector3(6, 6, 6), "C");

            Vector3 target = new Vector3(2, 2, 2);
            const float radius = 3.0f;

            // Act
            var result = _tree.QueryRadius(target, radius);

            // Assert
            Assert.AreEqual(2, result.Count, "Должно быть найдено 2 точки.");
            
            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(1, 1, 1), "A"), result,
                "Точка (1,1,1) должна быть в результате.");

            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(3, 3, 3), "B"), result,
                "Точка (3,3,3) должна быть в результате.");
        }

        [Test]
        public void FindAllInRadius_IncludesBoundaryPoints()
        {
            // Arrange
            _tree.Add(new Vector3(0, 0, 0), "A");
            _tree.Add(new Vector3(3, 3, 3), "B"); // Граница радиуса
            _tree.Add(new Vector3(5, 5, 5), "C"); // Вне радиуса

            Vector3 target = new Vector3(0, 0, 0);
            float radius = Mathf.Sqrt(27); // Радиус до точки (3,3,3)

            // Act
            var result = _tree.QueryRadius(target, radius);

            // Assert
            Assert.AreEqual(2, result.Count, "Должно быть найдено 2 точки.");

            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(0, 0, 0), "A"), result,
                "Точка (0,0,0) должна быть в результате.");

            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(3, 3, 3), "B"), result,
                "Точка (3,3,3) должна быть в результате.");
        }

        [Test]
        public void FindAllInRadius_3DCheck()
        {
            // Arrange
            _tree.Add(new Vector3(1, 2, 3), "A");
            _tree.Add(new Vector3(4, 5, 6), "B");
            _tree.Add(new Vector3(-2, -1, -3), "C");

            Vector3 target = new Vector3(0, 0, 0);
            const float radius = 5.0f;

            // Act
            var result = _tree.QueryRadius(target, radius);

            // Assert
            Assert.AreEqual(2, result.Count, "Должно быть найдено 2 точки.");

            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(1, 2, 3), "A"), result,
                "Точка (1,2,3) должна быть в результате.");

            Assert.Contains(new KeyValuePair<Vector3, string>(new Vector3(-2, -1, -3), "C"), result,
                "Точка (-2,-1,-3) должна быть в результате.");
        }

        [Test]
        public void FindAllInRadius_NoPoints_ReturnsEmpty()
        {
            // Arrange
            _tree.Add(new Vector3(10, 10, 10), "A");
            _tree.Add(new Vector3(-10, -10, -10), "B");

            Vector3 target = new Vector3(0, 0, 0);
            const float radius = 3.0f; // Слишком маленький радиус

            // Act
            var result = _tree.QueryRadius(target, radius);

            // Assert
            Assert.IsEmpty(result, "Не должно быть найдено точек.");
        }
    }
}