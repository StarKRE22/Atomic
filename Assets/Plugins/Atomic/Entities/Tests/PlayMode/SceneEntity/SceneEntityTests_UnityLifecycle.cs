using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_UnityLifecycle
    {
        [UnityTest]
        public IEnumerator EntityLifecycle_ByUnityLifecycle()
        {
            //Arrange:
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            //Act:
            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            //Wait awake:
            yield return null;

            //Assert:
            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(entity.IsActive);

            Assert.IsTrue(entity.HasBehaviour(stub));
            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);

            Assert.AreEqual(nameof(IEntitySpawned.OnSpawn), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityActive.OnActive), stub.InvocationList[1]);

            //Wait update:
            yield return null;
            Assert.IsTrue(stub.Updated);

            //Wait fixed & late update
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(stub.FixedUpdated);
            Assert.IsTrue(stub.LateUpdated);

            //Finalize:
            SceneEntity.Destroy(entity);
            Assert.IsFalse(entity.IsActive);
            Assert.IsTrue(stub.Deactivated);

            //Wait for OnDestroy
            yield return null;
            Assert.IsFalse(entity.IsSpawned);
            Assert.IsTrue(stub.Despawned);

            Assert.AreEqual(nameof(IEntityInactive.OnInactive), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDespawned.OnDespawn), stub.InvocationList[^1]);
        }

        [UnityTest]
        public IEnumerator AddBehaviour_EntityIsActive_ByUnityLifecycle()
        {
            //Arrange:
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            //Act:
            SceneEntity entity = SceneEntity.Create();

            //Wait unity callbacks
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(entity.IsSpawned);
            Assert.IsTrue(entity.IsActive);

            entity.AddBehaviour(stub);

            Assert.IsTrue(entity.HasBehaviour(stub));
            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);
            Assert.AreEqual(nameof(IEntitySpawned.OnSpawn), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityActive.OnActive), stub.InvocationList[1]);

            //Wait update
            yield return null;
            Assert.IsTrue(stub.Updated);

            //Wait fixed update
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(stub.FixedUpdated);
            Assert.IsTrue(stub.LateUpdated);

            //Finalize:
            SceneEntity.Destroy(entity);
        }


        [UnityTest]
        public IEnumerator Add_And_Remove_EntityBehaviour_EntityIsActive()
        {
            //Arrange:
            DummyEntityBehaviour stub = new DummyEntityBehaviour();

            //Act:
            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            }, useUnityLifecycle: true);

            //Wait Awake, Start
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(stub.Spawned);
            Assert.IsTrue(stub.Activated);
            
            Assert.AreEqual(nameof(IEntitySpawned.OnSpawn), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityActive.OnActive), stub.InvocationList[1]);

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(stub.FixedUpdated);

            yield return new WaitForEndOfFrame();
            Assert.IsTrue(stub.Updated);
            Assert.IsTrue(stub.LateUpdated);

            entity.DelBehaviour(stub);
            
            Assert.IsTrue(stub.Deactivated);
            Assert.IsTrue(stub.Despawned);

            Assert.AreEqual(nameof(IEntityInactive.OnInactive), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDespawned.OnDespawn), stub.InvocationList[^1]);
        }
    }
}