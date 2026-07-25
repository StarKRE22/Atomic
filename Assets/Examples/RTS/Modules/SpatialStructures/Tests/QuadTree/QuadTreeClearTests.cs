// using NUnit.Framework;
// using UnityEngine;
// using System.Collections.Generic;
//
// namespace Modules.SpatialStructures
// {
//     public class QuadTreeClearTests
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
//         }
//
//         [Test]
//         public void Clear_RemovesAllItems()
//         {
//             tree.Clear();
//
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(0, count);
//             Assert.IsEmpty(result);
//         }
//
//         [Test]
//         public void Clear_AllowsReinsertion()
//         {
//             tree.Clear();
//
//             Assert.IsTrue(tree.Insert(10, new Vector2(2f, 2f)));
//
//             var result = new List<int>();
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(1, result.Count);
//             Assert.AreEqual(10, result[0]);
//         }
//
//         [Test]
//         public void Clear_MultipleTimes_NoErrors()
//         {
//             tree.Clear();
//             tree.Clear();
//             tree.Clear();
//
//             var result = new List<int>();
//             int count = tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(0, count);
//         }
//
//         [Test]
//         public void Clear_AfterSubdivision_RemovesDeepItems()
//         {
//             var deepTree = new QuadTree<int>(new Vector2(0f, 0f), new Vector2(10f, 10f), 1, 4);
//
//             deepTree.Insert(1, new Vector2(1f, 1f));
//             deepTree.Insert(2, new Vector2(2f, 2f));
//             deepTree.Insert(3, new Vector2(3f, 3f));
//
//             deepTree.Clear();
//
//             var result = new List<int>();
//             int count = deepTree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(0, count);
//         }
//
//         [Test]
//         public void Clear_DoesNotBreakFutureSubdivision()
//         {
//             tree.Clear();
//
//             tree.Insert(1, new Vector2(1f, 1f));
//             tree.Insert(2, new Vector2(2f, 2f));
//             tree.Insert(3, new Vector2(8f, 8f));
//
//             var result = new List<int>();
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(3, result.Count);
//         }
//     }
// }