using NUnit.Framework;
using UnityEngine;

namespace Modules.SpatialStructures
{
    public sealed class SpatialTreeTests_Remove
    {
        private SpatialTree<int> tree;

        [SetUp]
        public void Setup()
        {
            tree = new SpatialTree<int>();
            tree.Add(new Vector3(3, 6, 7), 1); // добавление элемента (3, 6, 7)
            tree.Add(new Vector3(17, 15, 20), 2);
            tree.Add(new Vector3(10, 11, 12), 3);
            tree.Add(new Vector3(5, 8, 9), 4);
            tree.Add(new Vector3(12, 10, 14), 5);
        }

        [Test]
        public void Test_Remove_LeafNode()
        {
            // Удаление листа
            bool result = tree.Remove(new Vector3(5, 8, 9), 4);
            Assert.IsTrue(result);
            Assert.AreEqual(4, tree.Count); // После удаления количество элементов должно уменьшиться
        }

        [Test]
        public void Test_Remove_NodeWithOneChild()
        {
            // Удаление узла с одним потомком
            bool result = tree.Remove(new Vector3(3, 6, 7), 1);
            Assert.IsTrue(result);
            Assert.AreEqual(4, tree.Count); // После удаления количество элементов должно уменьшиться
        }

        [Test]
        public void Test_Remove_NodeWithTwoChildren()
        {
            // Удаление узла с двумя потомками
            bool result = tree.Remove(new Vector3(10, 11, 12), 3);
            Assert.IsTrue(result);
            Assert.AreEqual(4, tree.Count); // После удаления количество элементов должно уменьшиться
        }

        [Test]
        public void Test_Remove_NonExistentNode()
        {
            // Удаление несуществующего узла
            bool result = tree.Remove(new Vector3(99, 99, 99), 99);
            Assert.IsFalse(result); // Узел не существует, поэтому возвращаемое значение должно быть false
        }

        [Test]
        public void Test_Remove_AndCheckTreeIntegrity()
        {
            // Удаление и проверка целостности дерева
            Debug.Log($"{tree}");

            bool success = tree.Remove(new Vector3(17, 15, 20), 2);
            Assert.IsTrue(success);
            Assert.AreEqual(4, tree.Count); // Количество элементов должно уменьшиться
            
            Debug.Log($"{tree}");

            success = tree.Remove(new Vector3(12, 10, 14), 5); // Удалим элемент с одним потомком
            Assert.IsTrue(success);
            Assert.AreEqual(3, tree.Count); // Количество элементов должно уменьшиться
        }
    }
}