// using NUnit.Framework;
// using UnityEngine;
// using Modules.SpatialStructures;
// using System.Collections.Generic;
//
// namespace Modules.SpatialStructures
// {
//     public class QuadTreeQueryRadiusTests
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
//             tree.Insert(3, new Vector2(8f, 5f));
//             tree.Insert(4, new Vector2(9f, 9f));
//         }
//
//         [Test]
//         public void QueryRadius_AllInside_ReturnsAll()
//         {
//             var result = new List<int>();
//             int count = tree.QueryRadius(new Vector2(5f, 5f), 10f, result, true);
//
//             Assert.AreEqual(4, count);
//             CollectionAssert.AreEquivalent(new[] {1, 2, 3, 4}, result);
//         }
//
//         [Test]
//         public void QueryRadius_SmallRadius_ReturnsCenterOnly()
//         {
//             var result = new List<int>();
//             int count = tree.QueryRadius(new Vector2(5f, 5f), 0.1f, result, true);
//
//             Assert.AreEqual(1, count);
//             Assert.AreEqual(2, result[0]);
//         }
//
//         [Test]
//         public void QueryRadius_ExactBoundary_Included()
//         {
//             var result = new List<int>();
//             int count = tree.QueryRadius(new Vector2(5f, 5f), 3f, result, true);
//
//             Assert.Contains(3, result);
//         }
//
//         [Test]
//         public void QueryRadius_NoMatches_ReturnsZero()
//         {
//             var result = new List<int>();
//             int count = tree.QueryRadius(new Vector2(0f, 0f), 0.5f, result, true);
//
//             Assert.AreEqual(0, count);
//             Assert.IsEmpty(result);
//         }
//
//         [Test]
//         public void QueryRadius_WithClear_True_ClearsList()
//         {
//             var result = new List<int> {99};
//
//             tree.QueryRadius(new Vector2(5f, 5f), 10f, result, true);
//
//             Assert.IsFalse(result.Contains(99));
//         }
//
//         [Test]
//         public void QueryRadius_WithClear_False_Appends()
//         {
//             var result = new List<int> {99};
//
//             tree.QueryRadius(new Vector2(5f, 5f), 10f, result, false);
//
//             Assert.Contains(99, result);
//             Assert.AreEqual(5, result.Count);
//         }
//
//         [Test]
//         public void QueryRadius_OutsidePoints_Excluded()
//         {
//             var result = new List<int>();
//             int count = tree.QueryRadius(new Vector2(5f, 5f), 2f, result, true);
//
//             CollectionAssert.AreEquivalent(new[] {2}, result);
//             Assert.AreEqual(1, count);
//         }
//     }
// }