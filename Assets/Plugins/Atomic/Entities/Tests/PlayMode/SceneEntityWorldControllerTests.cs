// using System.Collections;
// using NUnit.Framework;
// using UnityEditor.SceneManagement;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.TestTools;
//
// namespace Atomic.Entities
// {
//     [TestFixture]
//     public sealed class SceneEntityWorldControllerTests
//     {
//         private const string SCENE_PATH =
//             "Assets/Plugins/Atomic/Entities/Tests/Assets/Scenes/EntityWorldWithController.unity";
//
//         private SceneEntityWorld _world;
//
//         [UnitySetUp]
//         public IEnumerator Setup()
//         {
//             AsyncOperation operation = EditorSceneManager
//                 .LoadSceneAsyncInPlayMode(SCENE_PATH, new LoadSceneParameters(LoadSceneMode.Additive));
//
//             yield return operation;
//             yield return new WaitForEndOfFrame();
//
//             _world = GameObject.FindObjectOfType<SceneEntityWorld>();
//
//             Assert.AreEqual(4, _world.EntityCount);
//         }
//
//         [UnityTest]
//         public IEnumerator Lifecycle()
//         {
//             yield return new WaitForSeconds(0.1f);
//
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//
//                 Assert.IsTrue(stub.initialized);
//                 Assert.IsTrue(stub.enabled);
//                 Assert.IsTrue(stub.updated);
//
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);
//             }
//
//             yield return new WaitForFixedUpdate();
//
//
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//
//                 Assert.IsTrue(stub.fixedUpdated);
//                 Assert.IsTrue(stub.lateUpdated);
//
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);
//             }
//
//             var controller = _world.GetComponent<SceneEntityWorldController>();
//
//
//             controller.enabled = false;
//
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.IsTrue(stub.disabled);
//             }
//
//             GameObject.Destroy(controller);
//
//             yield return null;
//
//
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.IsTrue(stub.disposed);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
//             }
//         }
//
//         [UnityTest]
//         public IEnumerator EnableDisableController()
//         {
//             yield return new WaitForSeconds(0.1f);
//
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.IsTrue(stub.initialized);
//                 Assert.IsTrue(stub.enabled);
//                 Assert.IsTrue(stub.updated);
//             }
//             
//             //Act:
//             var controller = _world.GetComponent<SceneEntityWorldController>();
//             controller.enabled = false;
//
//             //Assert:
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.IsTrue(stub.disabled);
//             }
//
//             //Act:
//             controller.enabled = true;
//
//             //Assert:
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[^1]);
//             }
//         }
//         
//         [UnityTest]
//         public IEnumerator RemoveBehaviour()
//         {
//             //Arrange:
//             yield return new WaitForSeconds(0.1f);
//             
//             //Pre-Assert:
//             foreach (IEntity entity in _world.Entities)
//             {
//                 EntityBehaviourStub stub = entity.GetBehaviour<EntityBehaviourStub>();
//                 Assert.IsTrue(stub.initialized);
//                 Assert.IsTrue(stub.enabled);
//                 Assert.IsTrue(stub.updated);
//                 
//                 //Act:
//                 bool success = entity.DelBehaviour(stub);
//             
//                 //Assert:
//                 Assert.IsTrue(success);
//                 Assert.IsTrue(stub.disabled);
//                 Assert.IsTrue(stub.disposed);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
//                 Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
//             }
//         }
//     }
// }