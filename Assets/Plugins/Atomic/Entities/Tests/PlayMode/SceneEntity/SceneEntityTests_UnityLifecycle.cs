using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public sealed class SceneEntityTests_UseUnityLifecycle
    {
        [UnityTest]
        public IEnumerator EntityLifecycle_ByUnityLifecycle()
        {
            //Arrange:
            EntityBehaviourStub stub = new EntityBehaviourStub();

            //Act:
            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            });

            //Wait awake:
            yield return null;

            //Assert:
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);

            Assert.IsTrue(entity.HasBehaviour(stub));
            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);

            Assert.AreEqual(nameof(IEntityInit.Init), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), stub.InvocationList[1]);

            //Wait update:
            yield return null;
            Assert.IsTrue(stub.Updated);

            //Wait fixed & late update
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(stub.FixedUpdated);
            Assert.IsTrue(stub.LateUpdated);

            //Finalize:
            SceneEntity.Destroy(entity);
            Assert.IsFalse(entity.Enabled);
            Assert.IsTrue(stub.Disabled);

            //Wait for OnDestroy
            yield return null;
            Assert.IsFalse(entity.Initialized);
            Assert.IsTrue(stub.Disposed);

            Assert.AreEqual(nameof(IEntityDisable.Disable), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), stub.InvocationList[^1]);
        }

        [UnityTest]
        public IEnumerator AddBehaviour_EntityIsActive_ByUnityLifecycle()
        {
            //Arrange:
            EntityBehaviourStub stub = new EntityBehaviourStub();

            //Act:
            SceneEntity entity = SceneEntity.Create();

            //Wait unity callbacks
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);

            entity.AddBehaviour(stub);

            Assert.IsTrue(entity.HasBehaviour(stub));
            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);
            Assert.AreEqual(nameof(IEntityInit.Init), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), stub.InvocationList[1]);

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
            EntityBehaviourStub stub = new EntityBehaviourStub();

            //Act:
            SceneEntity entity = SceneEntity.Create(behaviours: new IEntityBehaviour[]
            {
                stub
            }, useUnityLifecycle: true);

            //Wait Awake, Start
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(stub.Initialized);
            Assert.IsTrue(stub.Enabled);
            
            Assert.AreEqual(nameof(IEntityInit.Init), stub.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), stub.InvocationList[1]);

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(stub.FixedUpdated);

            yield return new WaitForEndOfFrame();
            Assert.IsTrue(stub.Updated);
            Assert.IsTrue(stub.LateUpdated);

            entity.DelBehaviour(stub);
            
            Assert.IsTrue(stub.Disabled);
            Assert.IsTrue(stub.Disposed);

            Assert.AreEqual(nameof(IEntityDisable.Disable), stub.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), stub.InvocationList[^1]);
        }
    }
}