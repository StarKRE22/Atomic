// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
//
// namespace Atomic.Entities
// {
//     public class SceneEntityBaker_Bake_GameObject_Tests
//     {
//         [SetUp]
//         public void SetUp()
//         {
//             SceneEntityBakerTestDouble.CreateCallCount = 0;
//         }
//
//         [UnityTest]
//         public IEnumerator Bake_Should_Process_Self_Bakers()
//         {
//             // Arrange
//             var root = new GameObject("Root");
//             SceneEntityBakerTestDouble dummy = root.AddComponent<SceneEntityBakerTestDouble>();
//
//             yield return null;
//
//             // Act
//             var entities = SceneEntityBakerTestDouble.Bake(root);
//
//             //Wait for destroy bakers:
//             yield return null;
//             
//             // Assert
//             Assert.AreEqual(1, entities.Length);
//             Assert.AreEqual(1, SceneEntityBakerTestDouble.CreateCallCount);
//             Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerTestDouble>(true).Length);
//         }
//
//         [UnityTest]
//         public IEnumerator Bake_Should_Process_Child_Bakers()
//         {
//             // Arrange
//             var root = new GameObject("Root");
//             var child = new GameObject("Child");
//             child.transform.SetParent(root.transform);
//             child.AddComponent<SceneEntityBakerTestDouble>();
//
//             yield return null;
//
//             // Act
//             var entities = SceneEntityBakerTestDouble.Bake(root);
//
//             //Wait for destroy bakers:
//             yield return null;
//             
//             // Assert
//             Assert.AreEqual(1, entities.Length);
//             Assert.AreEqual(1, SceneEntityBakerTestDouble.CreateCallCount);
//             Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerTestDouble>(true).Length);
//         }
//
//         [UnityTest]
//         public IEnumerator Bake_Should_Handle_NoBakers_Gracefully()
//         {
//             // Arrange
//             var go = new GameObject("Empty");
//
//             yield return null;
//
//             // Act
//             var entities = SceneEntityBakerTestDouble.Bake(go);
//
//             //Wait for destroy bakers:
//             yield return null;
//             
//             // Assert
//             Assert.IsNotNull(entities);
//             Assert.AreEqual(0, entities.Length);
//             Assert.AreEqual(0, SceneEntityBakerTestDouble.CreateCallCount);
//         }
//
//         [UnityTest]
//         public IEnumerator Bake_Should_Process_Multiple_Bakers()
//         {
//             // Arrange
//             var go = new GameObject("Parent");
//             go.AddComponent<SceneEntityBakerTestDouble>();
//
//             var child1 = new GameObject("Child1");
//             child1.transform.SetParent(go.transform);
//             child1.AddComponent<SceneEntityBakerTestDouble>();
//
//             var child2 = new GameObject("Child2");
//             child2.transform.SetParent(go.transform);
//             child2.AddComponent<SceneEntityBakerTestDouble>();
//
//             yield return null;
//
//             // Act
//             var entities = SceneEntityBakerTestDouble.Bake(go);
//
//             //Wait for destroy bakers:
//             yield return null;
//             
//             // Assert
//             Assert.AreEqual(3, entities.Length);
//             Assert.AreEqual(3, SceneEntityBakerTestDouble.CreateCallCount);
//             Assert.AreEqual(0, Object.FindObjectsOfType<SceneEntityBakerTestDouble>(true).Length);
//         }
//     }
// }