using System;
using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed partial class SpatialTreeTests
    {
        [Test]
        public void Add_SingleElement_ReturnsTrue()
        {
            // Act
            bool result = _tree.Add(new Vector3(1, 2, 3), "Object1");

            // Assert
            Assert.IsTrue(result, "Добавление должно вернуть true.");
            Assert.AreEqual(1, _tree.Count, "Количество элементов должно быть 1.");
        }

        [Test]
        public void Add_DuplicateElement_ReturnsFalse()
        {
            // Arrange
            _tree.Add(new Vector3(1, 2, 3), "Object1");

            // Act
            bool result = _tree.Add(new Vector3(1, 2, 3), "Object1");

            // Assert
            Assert.IsFalse(result, "Повторное добавление того же элемента должно вернуть false.");
            Assert.AreEqual(1, _tree.Count, "Количество элементов не должно увеличиваться.");
        }

        [Test]
        public void Add_DifferentObjects_SamePosition_ReturnsTrue()
        {
            // Arrange
            bool firstInsert = _tree.Add(new Vector3(1, 2, 3), "Object1");
            bool secondInsert = _tree.Add(new Vector3(1, 2, 3), "Object2"); // Разные объекты, но одинаковая позиция

            // Assert
            Assert.IsTrue(firstInsert, "Первый объект должен добавиться.");
            Assert.IsTrue(secondInsert, "Второй объект с той же позицией, но другим значением, должен добавиться.");
            Assert.AreEqual(2, _tree.Count, "Количество элементов должно быть 2.");
        }

        [Test]
        public void Add_MultipleElements_PreservesCount()
        {
            // Act
            _tree.Add(new Vector3(0, 0, 0), "Root");
            _tree.Add(new Vector3(-1, -2, -3), "Left");
            _tree.Add(new Vector3(2, 3, 4), "Right");

            // Assert
            Assert.AreEqual(3, _tree.Count, "Дерево должно содержать 3 элемента.");
        }

        [Test]
        public void Add_NullValue_ThrowsException()
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => _tree.Add(new Vector3(5, 5, 5), null));
            Assert.AreEqual("value", ex.ParamName, "Ожидалось исключение при добавлении null.");
        }

        [Test]
        public void Add_Elements_AreCorrectlyPositioned()
        {
            // Arrange
            _tree.Add(new Vector3(0, 0, 0), "Root"); // Корень
            _tree.Add(new Vector3(-1, -1, -1), "Left"); // Должен быть влево
            _tree.Add(new Vector3(1, 1, 1), "Right"); // Должен быть вправо

            // Act & Assert
            Assert.IsTrue(_tree.Add(new Vector3(2, 2, 2), "Right2"), "Объект должен быть добавлен вправо.");
            Assert.IsTrue(_tree.Add(new Vector3(-2, -2, -2), "Left2"), "Объект должен быть добавлен влево.");
            Assert.AreEqual(5, _tree.Count, "Дерево должно содержать 5 элементов.");
        }
    }
}