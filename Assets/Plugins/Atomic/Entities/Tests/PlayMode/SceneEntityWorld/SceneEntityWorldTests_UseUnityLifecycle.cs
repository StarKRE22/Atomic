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
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);

            //Wait Awake, Start
            yield return null;
            Assert.IsTrue(world.IsSpawned);
            Assert.IsTrue(world.IsActive);
            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(entity.IsActive);
            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);

            //Wait for update
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(stub.Updated);
            Assert.IsTrue(stub.FixedUpdated);
            Assert.IsTrue(stub.LateUpdated);

            //Disable world
            world.enabled = false;
            Assert.IsFalse(world.IsActive);
            Assert.IsFalse(entity.IsActive);
            Assert.IsTrue(stub.Deactivated);

            //Destroy world
            Assert.IsTrue(stub.Activated);
            SceneEntityWorld.Destroy(world);

            //Wait OnDestroy
            yield return null;

            Assert.IsFalse(world.IsSpawned);
            Assert.IsFalse(entity.IsSpawned);
            Assert.IsTrue(stub.Despawned);
        }
        
        [UnityTest]
        public IEnumerator Enable_And_Disable()
        {
            //Arrange:
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);
            Assert.IsTrue(stub.Updated);
            
            //Act:
            world.enabled = false;

            Assert.IsTrue(stub.Deactivated);

            //Act:
            world.enabled = true;
            
            Assert.AreEqual(nameof(IEntityActive.OnActive), stub.InvocationList[^1]);
        }
        
        [UnityTest]
        public IEnumerator RemoveBehaviour()
        {
            //Arrange:
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            SceneEntityWorld world = SceneEntityWorld.Create("SceneEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            //Arrange:
            yield return new WaitForSeconds(0.1f);
            
            //Pre-Assert:
            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);
            Assert.IsTrue(stub.Updated);
            
            //Act:
            bool success = entity.DelBehaviour(stub);
            
            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(stub.Deactivated);
            Assert.IsTrue(stub.Despawned);
            Assert.AreEqual(nameof(IEntityInactive.OnInactive), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDespawned.OnDespawn), stub.InvocationList[^1]);
        }
    }
}