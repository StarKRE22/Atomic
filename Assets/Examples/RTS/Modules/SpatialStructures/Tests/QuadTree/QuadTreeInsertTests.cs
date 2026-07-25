// using NUnit.Framework;
// using UnityEngine;
// using System.Collections.Generic;
//
// namespace Modules.SpatialStructures
// {
//   
//     public class QuadTreeInsertTests
//     {
//         private QuadTree<int> tree;
//
//         [SetUp]
//         public void Setup()
//         {
//             tree = new QuadTree<int>(new Vector2(0f, 0f), new Vector2(10f, 10f), capacity: 2, maxDepth: 4);
//         }
//
//         [Test]
//         public void Insert_Inside_ReturnsTrue()
//         {
//             Assert.IsTrue(tree.Insert(1, new Vector2(5f, 5f)));
//         }
//
//         [Test]
//         public void Insert_Outside_ReturnsFalse()
//         {
//             Assert.IsFalse(tree.Insert(1, new Vector2(-1f, 5f)));
//         }
//
//         [Test]
//         public void Insert_FillsCapacity_NoSubdivision()
//         {
//             Assert.IsTrue(tree.Insert(1, new Vector2(2f, 2f)));
//             Assert.IsTrue(tree.Insert(2, new Vector2(3f, 3f)));
//         }
//
//         [Test]
//         public void Insert_TriggersSubdivision()
//         {
//             tree.Insert(1, new Vector2(2f, 2f));
//             tree.Insert(2, new Vector2(3f, 3f));
//             Assert.IsTrue(tree.Insert(3, new Vector2(7f, 7f)));
//         }
//
//         [Test]
//         public void Insert_ItemsStillQueryable_AfterSubdivision()
//         {
//             tree.Insert(1, new Vector2(2f, 2f));
//             tree.Insert(2, new Vector2(3f, 3f));
//             tree.Insert(3, new Vector2(7f, 7f));
//
//             var result = new List<int>();
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.Contains(1, result);
//             Assert.Contains(2, result);
//             Assert.Contains(3, result);
//         }
//
//         [Test]
//         public void Insert_MaxDepth_StopsSubdivision()
//         {
//             var smallTree = new QuadTree<int>(
//                 new Vector2(0f, 0f),
//                 new Vector2(10f, 10f),
//                 capacity: 1,
//                 maxDepth: 0
//             );
//
//             Assert.IsTrue(smallTree.Insert(1, new Vector2(2f, 2f)));
//             Assert.IsFalse(smallTree.Insert(2, new Vector2(3f, 3f)));
//         }
//
//         [Test]
//         public void Insert_MultipleQuadrants_CorrectlyStored()
//         {
//             tree.Insert(1, new Vector2(1f, 1f));
//             tree.Insert(2, new Vector2(9f, 1f));
//             tree.Insert(3, new Vector2(1f, 9f));
//             tree.Insert(4, new Vector2(9f, 9f));
//
//             var result = new List<int>();
//             tree.QueryAABB(new Vector2(0f, 0f), new Vector2(10f, 10f), result, true);
//
//             Assert.AreEqual(4, result.Count);
//         }
//
//         [Test]
//         public void Insert_DuplicatePositions_AllStored()
//         {
//             tree.Insert(1, new Vector2(5f, 5f));
//             tree.Insert(2, new Vector2(5f, 5f));
//
//             var result = new List<int>();
//             tree.QueryAABB(new Vector2(5f, 5f), new Vector2(5f, 5f), result, true);
//
//             Assert.AreEqual(2, result.Count);
//         }
//     }
// }