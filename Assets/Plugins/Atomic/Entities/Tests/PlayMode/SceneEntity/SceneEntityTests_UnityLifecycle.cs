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
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            //Act:
            MonoEntity entity = MonoEntity.Create(behaviours: new IEntityBehaviour[]
            {
                spy
            });

            //Wait awake:
            yield return null;

            //Assert:
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);

            Assert.IsTrue(entity.HasBehaviour(spy));
            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);

            Assert.AreEqual(nameof(IEntityInit.Init), spy.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), spy.InvocationList[1]);

            //Wait update:
            yield return null;
            Assert.IsTrue(spy.Updated);

            //Wait fixed & late update
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(spy.FixedUpdated);
            Assert.IsTrue(spy.LateUpdated);

            //Finalize:
            MonoEntity.Destroy(entity);
            Assert.IsFalse(entity.Enabled);
            Assert.IsTrue(spy.Disabled);

            //Wait for OnDestroy
            yield return null;
            Assert.IsFalse(entity.Initialized);
            Assert.IsTrue(spy.Disposed);

            Assert.AreEqual(nameof(IEntityDisable.Disable), spy.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), spy.InvocationList[^1]);
        }

        [UnityTest]
        public IEnumerator AddBehaviour_EntityIsActive_ByUnityLifecycle()
        {
            //Arrange:
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            //Act:
            MonoEntity entity = MonoEntity.Create();

            //Wait unity callbacks
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);

            entity.AddBehaviour(spy);

            Assert.IsTrue(entity.HasBehaviour(spy));
            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);
            Assert.AreEqual(nameof(IEntityInit.Init), spy.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), spy.InvocationList[1]);

            //Wait update
            yield return null;
            Assert.IsTrue(spy.Updated);

            //Wait fixed update
            yield return new WaitForFixedUpdate();

            Assert.IsTrue(spy.FixedUpdated);
            Assert.IsTrue(spy.LateUpdated);

            //Finalize:
            MonoEntity.Destroy(entity);
        }


        [UnityTest]
        public IEnumerator Add_And_Remove_EntityBehaviour_EntityIsActive()
        {
            //Arrange:
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            //Act:
            MonoEntity entity = MonoEntity.Create(behaviours: new IEntityBehaviour[]
            {
                spy
            }, useUnityLifecycle: true);

            //Wait Awake, Start
            yield return new WaitForEndOfFrame();

            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);
            
            Assert.AreEqual(nameof(IEntityInit.Init), spy.InvocationList[0]);
            Assert.AreEqual(nameof(IEntityEnable.Enable), spy.InvocationList[1]);

            yield return new WaitForFixedUpdate();
            Assert.IsTrue(spy.FixedUpdated);

            yield return new WaitForEndOfFrame();
            Assert.IsTrue(spy.Updated);
            Assert.IsTrue(spy.LateUpdated);

            entity.DelBehaviour(spy);
            
            Assert.IsTrue(spy.Disabled);
            Assert.IsTrue(spy.Disposed);

            Assert.AreEqual(nameof(IEntityDisable.Disable), spy.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), spy.InvocationList[^1]);
        }
    }
}