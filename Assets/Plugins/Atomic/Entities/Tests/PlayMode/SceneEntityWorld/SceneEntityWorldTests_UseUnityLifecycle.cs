using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class SceneEntityWorldTests_UseUnityLifecycle
    {
        [UnityTest]
        public IEnumerator UseUnityLifecycle()
        {
            //Arrange:
            EntityBehaviourStub stub = new EntityBehaviourStub();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);

            //Wait Awake, Start
            yield return null;
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);
            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);

            //Wait for update
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(stub.Updated);
            
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(stub.FixedUpdated);
            Assert.IsTrue(stub.LateUpdated);

            //Disable world
            world.enabled = false;
            Assert.IsFalse(world.Enabled);
            Assert.IsFalse(entity.Enabled);
            Assert.IsTrue(stub.Disabled);

            //Destroy world
            Assert.IsTrue(stub.Enabled);
            SceneEntityWorld.Destroy(world);

            //Wait OnDestroy
            yield return null;

            Assert.IsTrue(entity.Initialized);
            Assert.IsFalse(stub.Disposed);
        }
        
        [UnityTest]
        public IEnumerator Enable_And_Disable()
        {
            //Arrange:
            EntityBehaviourStub stub = new EntityBehaviourStub();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);
            Assert.IsTrue(stub.Updated);
            
            //Act:
            world.enabled = false;

            Assert.IsTrue(stub.Disabled);

            //Act:
            world.enabled = true;
            
            Assert.AreEqual(nameof(IEntityEnable.Enable), stub.InvocationList[^1]);
        }
        
        [UnityTest]
        public IEnumerator RemoveBehaviour()
        {
            //Arrange:
            EntityBehaviourStub stub = new EntityBehaviourStub();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            //Arrange:
            yield return new WaitForSeconds(0.1f);
            
            //Pre-Assert:
            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);
            Assert.IsTrue(stub.Updated);
            
            //Act:
            bool success = entity.DelBehaviour(stub);
            
            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(stub.Disabled);
            Assert.IsTrue(stub.Disposed);
            Assert.AreEqual(nameof(IEntityDisable.Disable), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), stub.InvocationList[^1]);
        }
    }
}