using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities.Tests
{
    public sealed class SceneEntityRunnerTests
    {
        [UnityTest]
        public IEnumerator AddBehaviourBeforeAwake()
        {
            //Arrange:
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            var runner = entityGO.AddComponent<SceneEntityUpdater>();
            runner.Add(entity);

            BehaviourStub stub = new BehaviourStub();
            entity.AddBehaviour(stub);
            
            Assert.IsTrue(entity.HasBehaviour(stub));
            Assert.IsFalse(stub.initialized);
            Assert.IsFalse(stub.enabled);
        
            yield return null;
            
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(BehaviourStub.Enable), stub.invokationList[1]);
        
            yield return null;
            
            Assert.IsTrue(stub.updated);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
        
            //Finalize:
            GameObject.Destroy(runner);
            
            yield return null;
            Assert.IsTrue(stub.disabled);
            Assert.IsTrue(stub.disposed);
            
            Assert.AreEqual(nameof(BehaviourStub.Disable), stub.invokationList[^2]);
            Assert.AreEqual(nameof(BehaviourStub.Dispose), stub.invokationList[^1]);
        }
        
        [UnityTest]
        public IEnumerator AddBehaviourWhenAlreadyPlaying()
        {
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            var runner = entityGO.AddComponent<SceneEntityUpdater>();
            runner.Add(entity);

            yield return new WaitForSeconds(0.25f);


            BehaviourStub stub = new BehaviourStub();
            entity.AddBehaviour(stub);
            
            Assert.IsTrue(entity.HasBehaviour(stub));
            
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.AreEqual(nameof(BehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(BehaviourStub.Enable), stub.invokationList[1]);
        
            yield return null;
            
            Assert.IsTrue(stub.updated);
            
            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
        
            //Finalize:
            GameObject.Destroy(entityGO);
        }
        
        [UnityTest]
        public IEnumerator AddAndRemoveRunnerInRuntime()
        {
            var entityGO = new GameObject();
            var entity = entityGO.AddComponent<SceneEntity>();
            BehaviourStub stub = new BehaviourStub();
            entity.AddBehaviour(stub);
            
            yield return new WaitForSeconds(0.25f);
            
            Assert.IsFalse(stub.initialized);
            Assert.IsFalse(stub.enabled);
            Assert.IsFalse(stub.updated);
            
            var runner = entityGO.AddComponent<SceneEntityUpdater>();
            runner.Add(entity);
            
            //Arrange:
            yield return null;
            
            //Assert:
            Assert.IsTrue(stub.initialized);
            Assert.IsTrue(stub.enabled);
            Assert.IsTrue(stub.updated);
            
            Assert.AreEqual(nameof(BehaviourStub.Init), stub.invokationList[0]);
            Assert.AreEqual(nameof(BehaviourStub.Enable), stub.invokationList[1]);

            yield return new WaitForFixedUpdate();
            
            Assert.IsTrue(stub.fixedUpdated);
            Assert.IsTrue(stub.lateUpdated);
            
            GameObject.Destroy(runner);

            Assert.IsTrue(stub.disabled);
            
            yield return null;
            Assert.IsTrue(stub.disposed);
            
            Assert.AreEqual(nameof(BehaviourStub.Disable), stub.invokationList[^2]);
            Assert.AreEqual(nameof(BehaviourStub.Dispose), stub.invokationList[^1]);
        }
    }
}