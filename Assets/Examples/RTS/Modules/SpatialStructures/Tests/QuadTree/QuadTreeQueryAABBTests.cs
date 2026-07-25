// using NUnit.Framework;
// using UnityEngine;
// using System.Collections.Generic;
//
// namespace Modules.SpatialStructures
// {
//     public class QuadTreeQueryAABBTests
//     {
//         private QuadTree<int> tree;
//
//         [SetUp]
//         public void Setup()
//         {
//             tree = new QuadTree<int>(new Vector2(0f, 0f), new Vector2(10f, 10f), 2, 4);
//
//             tree.Insert(1, new Vector2(1f, 1f));
//             tree.Insert(2, new Vector2(5f, 5f));
//             tree.Insert(3, new Vector2(9f, 9f));
//             tree.Insert(4, new Vector2(7f, 2f));
//         }
//
//         [Test]
//         public void Query_AllItems_ReturnsAll()
//         {
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(4, count);
//             CollectionAssert.AreEquivalent(new[] {1, 2, 3, 4}, result);
//         }
//
//         [Test]
//         public void Query_SubArea_ReturnsSubset()
//         {
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(0f, 0f), new Vector2(6f, 6f), result, true);
//
//             Assert.AreEqual(2, count);
//             CollectionAssert.AreEquivalent(new[] {1, 2}, result);
//         }
//
//         [Test]
//         public void Query_SinglePoint_ReturnsCorrectItem()
//         {
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(5f, 5f), new Vector2(5f, 5f), result, true);
//
//             Assert.AreEqual(1, count);
//             Assert.AreEqual(2, result[0]);
//         }
//
//         [Test]
//         public void Query_NoOverlap_ReturnsZero()
//         {
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(20f, 20f), new Vector2(30f, 30f), result, true);
//
//             Assert.AreEqual(0, count);
//             Assert.IsEmpty(result);
//         }
//
//         [Test]
//         public void Query_WithClear_True_ClearsList()
//         {
//             var result = new List<int> {99};
//
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.IsFalse(result.Contains(99));
//         }
//
//         [Test]
//         public void Query_WithClear_False_Appends()
//         {
//             var result = new List<int> {99};
//
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, false);
//
//             Assert.Contains(99, result);
//             Assert.AreEqual(5, result.Count);
//         }
//
//         [Test]
//         public void Query_OnBoundary_Inclusive()
//         {
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(9f, 9f), new Vector2(9f, 9f), result, true);
//
//             Assert.AreEqual(1, count);
//             Assert.AreEqual(3, result[0]);
//         }
//     }
// }