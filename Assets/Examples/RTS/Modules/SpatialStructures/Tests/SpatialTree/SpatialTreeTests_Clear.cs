using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTreeTests
    {
        [Test]
        public void Clear_EmptiesTree()
        {
            // Arrange
            _tree.Add(new Vector3(1, 1, 1), "Object1");
            _tree.Add(new Vector3(2, 2, 2), "Object2");

            // Act
            _tree.Clear();

            // Assert
            Assert.AreEqual(0, _tree.Count, "После очистки дерево должно быть пустым.");
            Assert.IsTrue(_tree.Add(new Vector3(1, 1, 1), "Object1"), "После очистки объект можно добавить заново.");
        }

        [Test]
        public void Clear_EmptyTree_NoErrors()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _tree.Clear(), "Очистка пустого дерева не должна вызывать исключения.");
            Assert.AreEqual(0, _tree.Count, "Пустое дерево после очистки должно оставаться пустым.");
        }
    }
}