using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class MonoEntityWorldTests_UseUnityLifecycle
    {
        [UnityTest]
        public IEnumerator UseUnityLifecycle()
        {
            //Arrange:
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            MonoEntity entity = MonoEntity.Create(behaviours: new IEntityBehaviour[]
            {
                spy
            });

            MonoEntityWorld world = MonoEntityWorld.Create("MonoEntityWorld", useUnityLifecycle: true);
            world.Add(entity);

            //Wait Awake, Start
            yield return null;
            Assert.IsTrue(world.Enabled);
            Assert.IsTrue(entity.Initialized);
            Assert.IsTrue(entity.Enabled);
            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);

            //Wait for update
            yield return new WaitForEndOfFrame();
            Assert.IsTrue(spy.Updated);
            
            yield return new WaitForFixedUpdate();
            Assert.IsTrue(spy.FixedUpdated);
            Assert.IsTrue(spy.LateUpdated);

            //Disable world
            world.enabled = false;
            Assert.IsFalse(world.Enabled);
            Assert.IsFalse(entity.Enabled);
            Assert.IsTrue(spy.Disabled);

            //Destroy world
            Assert.IsTrue(spy.Enabled);
            MonoEntityWorld.Destroy(world);

            //Wait OnDestroy
            yield return null;

            Assert.IsTrue(entity.Initialized);
            Assert.IsFalse(spy.Disposed);
        }
        
        [UnityTest]
        public IEnumerator Enable_And_Disable()
        {
            //Arrange:
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            MonoEntity entity = MonoEntity.Create(behaviours: new IEntityBehaviour[]
            {
                spy
            });

            MonoEntityWorld world = MonoEntityWorld.Create("MonoEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            yield return new WaitForSeconds(0.1f);

            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);
            Assert.IsTrue(spy.Updated);
            
            //Act:
            world.enabled = false;

            Assert.IsTrue(spy.Disabled);

            //Act:
            world.enabled = true;
            
            Assert.AreEqual(nameof(IEntityEnable.Enable), spy.InvocationList[^1]);
        }
        
        [UnityTest]
        public IEnumerator RemoveBehaviour()
        {
            //Arrange:
            EntityBehaviourSpy spy = new EntityBehaviourSpy();

            MonoEntity entity = MonoEntity.Create(behaviours: new IEntityBehaviour[]
            {
                spy
            });

            MonoEntityWorld world = MonoEntityWorld.Create("MonoEntityWorld", useUnityLifecycle: true);
            world.Add(entity);
            
            //Arrange:
            yield return new WaitForSeconds(0.1f);
            
            //Pre-Assert:
            Assert.IsTrue(spy.Initialized);
            Assert.IsTrue(spy.Enabled);
            Assert.IsTrue(spy.Updated);
            
            //Act:
            bool success = entity.DelBehaviour(spy);
            
            //Assert:
            Assert.IsTrue(success);
            Assert.IsTrue(spy.Disabled);
            Assert.IsTrue(spy.Disposed);
            Assert.AreEqual(nameof(IEntityDisable.Disable), spy.InvocationList[^2]);
            Assert.AreEqual(nameof(IEntityDispose.Dispose), spy.InvocationList[^1]);
        }
    }
}