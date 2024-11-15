using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities.Tests
{
    public sealed class SceneEntityControllerTests
    {
        // private const string PREFAB_PATH = "Assets/Plugins/Atomic/Entities/Tests/Assets/Prefabs/EntityWithController.prefab";

        // [UnityTest]
        // public IEnumerator Lifecycle()
        // {
        //     //Arrange:
        //     GameObject entityPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
        //     GameObject entityGO = GameObject.Instantiate(entityPrefab);
        //     
        //     var sceneEntity = entityGO.GetComponent<SceneEntity>();
        //     var sceneEntityController = entityGO.GetComponent<SceneEntityController>(); 
        //     
        //     EntityBehaviourStub stub = sceneEntity.GetBehaviour<EntityBehaviourStub>();
        //     
        //     yield return null;
        //     
        //     //Assert:
        //     Assert.IsTrue(stub.initialized);
        //     Assert.IsTrue(stub.enabled);
        //     Assert.IsTrue(stub.updated);
        //     
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);
        //
        //     yield return new WaitForFixedUpdate();
        //     
        //     Assert.IsTrue(stub.fixedUpdated);
        //     Assert.IsTrue(stub.lateUpdated);
        //
        //     sceneEntityController.enabled = false;
        //     Assert.IsTrue(stub.disabled);
        //     
        //     GameObject.Destroy(entityGO);
        //     
        //     yield return null;
        //     Assert.IsTrue(stub.disposed);
        //     
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
        // }
        
        // [UnityTest]
        // public IEnumerator EnableDisableController()
        // {
        //     //Arrange:
        //     GameObject entityPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
        //     GameObject entityGO = GameObject.Instantiate(entityPrefab);
        //
        //     var sceneEntity = entityGO.GetComponent<SceneEntity>();
        //     var sceneController = entityGO.GetComponent<SceneEntityController>();
        //     var stub = sceneEntity.GetBehaviour<EntityBehaviourStub>();
        //     
        //     yield return null;
        //     
        //     //Assert:
        //     Assert.IsTrue(stub.initialized);
        //     Assert.IsTrue(stub.enabled);
        //     Assert.IsTrue(stub.updated);
        //     
        //     //Act:
        //     sceneController.enabled = false;
        //     
        //     //Assert:
        //     Assert.IsTrue(stub.disabled);
        //
        //     //Act:
        //     sceneController.enabled = true;
        //     
        //     //Assert:
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[^1]);
        //     
        //     //Finalize:
        //     GameObject.Destroy(entityGO);
        // }

        // [UnityTest]
        // public IEnumerator RemoveBehaviour()
        // {
        //     //Arrange:
        //     GameObject entityPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PREFAB_PATH);
        //     GameObject entityGO = GameObject.Instantiate(entityPrefab);
        //
        //     var entity = entityGO.GetComponent<SceneEntity>();
        //     var stub = entity.GetBehaviour<EntityBehaviourStub>();
        //     
        //     yield return null;
        //     
        //     //Pre-Assert:
        //     Assert.IsTrue(stub.initialized);
        //     Assert.IsTrue(stub.enabled);
        //     Assert.IsTrue(stub.updated);
        //     
        //     //Act:
        //     bool success = entity.DelBehaviour(stub);
        //     
        //     //Assert:
        //     Assert.IsTrue(success);
        //     Assert.IsTrue(stub.disabled);
        //     Assert.IsTrue(stub.disposed);
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
        //     Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
        //     
        //     //Finalize:
        //     GameObject.Destroy(entityGO);
        // }
        
        
        [UnityTest]
        public IEnumerator AddBehaviourBeforeAwake()
        {
            //Arrange:
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            var controller = entityGO.AddComponent<SceneEntityController>();

            EntityBehaviourStub stub = new EntityBehaviourStub();
            bool success = entity.AddBehaviour(stub);
            Assert.IsTrue(success);
            
            Assert.IsFalse(stub.initialized);
            Assert.IsFalse(stub.enabled);
        
            yield return null;
            
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);
        
            yield return null;
            
            Assert.IsTrue(stub.updated);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
        
            //Finalize:
            GameObject.Destroy(controller);
            
            yield return null;
            Assert.IsTrue(stub.disabled);
            Assert.IsTrue(stub.disposed);
            
            Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
        }
        
        [UnityTest]
        public IEnumerator AddBehaviourWhenAlreadyPlaying()
        {
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            entityGO.AddComponent<SceneEntityController>();
            yield return new WaitForSeconds(0.25f);


            EntityBehaviourStub stub = new EntityBehaviourStub();
            bool success = entity.AddBehaviour(stub);
            
            Assert.IsTrue(success);
            
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);
        
            yield return null;
            
            Assert.IsTrue(stub.updated);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
        
            //Finalize:
            GameObject.Destroy(entityGO);
        }
        
        [UnityTest]
        public IEnumerator AddAndRemoveSceneControllerInRuntime()
        {
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            EntityBehaviourStub stub = new EntityBehaviourStub();
            entity.AddBehaviour(stub);
            
            yield return new WaitForSeconds(0.25f);
            
            Assert.IsFalse(stub.initialized);
            Assert.IsFalse(stub.enabled);
            Assert.IsFalse(stub.updated);
            
            var sceneEntityController = entityGO.AddComponent<SceneEntityController>();
            
            //Arrange:
            yield return null;
            
            //Assert:
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.IsTrue(stub.updated);
            
            Assert.AreEqual(nameof(EntityBehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Enable), stub.invokationList[1]);

            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
            
            GameObject.Destroy(sceneEntityController);

            Assert.IsTrue(stub.disabled);
            
            yield return null;
            Assert.IsTrue(stub.disposed);
            
            Assert.AreEqual(nameof(EntityBehaviourStub.Disable), stub.invokationList[^2]);
            Assert.AreEqual(nameof(EntityBehaviourStub.Dispose), stub.invokationList[^1]);
        }
    }
}