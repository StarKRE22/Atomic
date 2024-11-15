// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using Unity.PerformanceTesting;
// using UnityEngine;
// using UnityEngine.TestTools;
//
// namespace Atomic.Entities
// {
//     [TestFixture]
//     public sealed class MeasureTests
//     {
//         private List<GameObject> gameObjects;
//
//         [SetUp]
//         public void SetUp()
//         {
//             gameObjects = new List<GameObject>();
//
//             for (int i = 0; i < 1000; i++)
//             {
//                 gameObjects.Add(new GameObject("Entity", typeof(SceneEntity)));
//             }
//         }
//
//
//         [UnityTest, Performance]
//         public IEnumerator GetComponentTests()
//         {
//             Measure
//                 .Method(() =>
//                 {
//                     for (int i = 0; i < gameObjects.Count; i++)
//                     {
//                         IEntity entity = gameObjects[i].GetComponent<IEntity>();
//                         
//                     }
//                 })
//                 .WarmupCount(5)
//                 .MeasurementCount(20)
//                 .Run();
//             
//             yield break;
//         }
//
//         [TearDown]
//         public void TearDown()
//         {
//             foreach (var gameObject in this.gameObjects)
//             {
//                 GameObject.DestroyImmediate(gameObject);
//                 
//             }
//         }
//     }
// }