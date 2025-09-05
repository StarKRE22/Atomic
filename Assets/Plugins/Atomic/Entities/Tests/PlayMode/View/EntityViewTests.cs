using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class EntityViewTests
    {
        [UnityTest]
        public IEnumerator Show_InstallsOnce_AndAddsBehaviours()
        {
            var installer = new GameObject("Installer").AddComponent<TestInstaller>();

            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView",
                installers = new List<EntityViewInstaller> {installer}
            });

            var entity = new Entity();
            view.Show(entity);
            yield return null;

            Assert.IsTrue(installer.Installed);
            Assert.AreEqual(1, entity.BehaviourCount);
            Assert.AreSame(view.GetBehaviourAt(0), entity.GetBehaviourAt(0));

            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [UnityTest]
        public IEnumerator Hide_RemovesBehaviours_AndDisablesGameObject()
        {
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView"
            });

            var entity = new Entity();
            view.Show(entity);
            yield return null;

            var behaviour = new MockBehaviour();
            view.AddBehaviour(behaviour);

            view.Hide();
            yield return null;

            Assert.IsFalse(view.IsVisible);
            Assert.IsFalse(view.gameObject.activeSelf);
            Assert.IsFalse(entity.HasBehaviour(behaviour));

            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [UnityTest]
        public IEnumerator AddBehaviour_AfterShow_ImmediatelyAddedToEntity()
        {
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView"
            });
            
            var entity = new Entity();
            view.Show(entity);
            yield return null;

            var behaviour = new MockBehaviour();
            view.AddBehaviour(behaviour);

            Assert.IsTrue(entity.HasBehaviour(behaviour));
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [UnityTest]
        public IEnumerator DelBehaviour_AfterShow_ImmediatelyRemovedFromEntity()
        {
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView"
            });

            var behaviour = new MockBehaviour();
            view.AddBehaviour(behaviour);

            var entity = new Entity();
            view.Show(entity);
            yield return null;

            view.DelBehaviour(behaviour);
            Assert.IsFalse(entity.HasBehaviour(behaviour));

            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [UnityTest]
        public IEnumerator Install_CalledOnlyOnce()
        {
            var installer = new GameObject("Installer").AddComponent<TestInstaller>();
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView",
                installers = new List<EntityViewInstaller> {installer}
            });

            var entity = new Entity();
            view.Show(entity);
            view.Hide();
            view.Show(entity);
            yield return null;

            Assert.AreEqual(1, installer.CallCount);
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [UnityTest]
        public IEnumerator EntityView_NonGeneric_WorksAsExpected()
        {
            var view = BehaviourEntityView.Create();
            
            var entity = new Entity();
            view.Show(entity);
            yield return null;

            Assert.AreEqual(entity, view.Entity);
            Assert.IsTrue(view.IsVisible);
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void AddBehaviour_BeforeShow_IsAppliedAfterShow()
        {
            var view = BehaviourEntityView.Create();
            var behaviour = new MockBehaviour();
            view.AddBehaviour(behaviour);

            var entity = new Entity();
            view.Show(entity);

            Assert.IsTrue(entity.HasBehaviour(behaviour));
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void DelBehaviour_BeforeShow_RemovesFromInternalList()
        {
            var view = BehaviourEntityView.Create();
            var behaviour = new MockBehaviour();
            view.AddBehaviour(behaviour);
            view.DelBehaviour(behaviour);

            Assert.IsFalse(view.HasBehaviour(behaviour));

            var entity = new Entity();
            entity.OnBehaviourAdded += (entity1, entityBehaviour) =>
                Debug.Log($"BEHAVIOUR ADDED {entityBehaviour.GetType().Name}");
            
            view.Show(entity);

            
            Assert.IsFalse(entity.HasBehaviour(behaviour));
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void AddBehaviour_Twice_DoesNotDuplicateInEntity()
        {
            var view = BehaviourEntityView.Create();
            var behaviour = new MockBehaviour();

            view.AddBehaviour(behaviour);
            view.AddBehaviour(behaviour);

            var entity = new Entity();
            view.Show(entity);

            int count = entity.GetBehaviours().Count(b => b == behaviour);
            Assert.AreEqual(1, count); // хотя в _behaviours может быть 2, но в Entity — должно быть 1
          
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void Install_WithNullInstallers_DoesNotThrow()
        {
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView",
                installers = new List<EntityViewInstaller>
                {
                    null,
                    new GameObject().AddComponent<TestInstaller>(),
                    null
                }
            });

            var entity = new Entity();
            Assert.DoesNotThrow(() => view.Show(entity));
            
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void Install_CalledOnlyOnceEvenAfterMultipleShowHide()
        {
            var installer = new GameObject("Installer").AddComponent<TestInstaller>();
            var view = BehaviourEntityView.Create(new BehaviourEntityView.CreateArgs
            {
                name = "EntityView",
                installers = new List<EntityViewInstaller> {installer}
            });

            var entity = new Entity();
            view.Show(entity);
            view.Hide();
            view.Show(entity);
            view.Hide();
            view.Show(entity);

            Assert.AreEqual(1, installer.CallCount);
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void OnDrawGizmosSelected_CallsGizmosDraw()
        {
            var view = BehaviourEntityView.Create();

            var gizmo = new MockGizmoBehaviour();
            view.AddBehaviour(gizmo);

            var entity = new Entity();
            view.Show(entity);

            view.OnGizmosDraw();

            Assert.IsTrue(gizmo.Called);
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        [Test]
        public void OnDrawGizmosSelected_WhenExceptionInBehaviour_LogsWarning()
        {
            var view = BehaviourEntityView.Create();
            var badGizmo = new ThrowingGizmoBehaviour();
            view.AddBehaviour(badGizmo);
            
            var entity = new Entity();
            view.Show(entity);

            LogAssert.Expect(LogType.Warning,
                new System.Text.RegularExpressions.Regex("Ops: detected exception in gizmos"));

            view.OnGizmosDraw();
            
            BehaviourEntityView.Destroy(view);
            entity.Dispose();
        }

        public class MockBehaviour : IEntityBehaviour
        {
        }

        public class TestInstaller : EntityViewInstaller
        {
            public bool Installed { get; private set; }
            public int CallCount { get; private set; }

            public override void Install(BehaviourEntityView view)
            {
                Installed = true;
                CallCount++;
                view.AddBehaviour(new MockBehaviour());
            }
        }

        private sealed class MockGizmoBehaviour : IEntityGizmos
        {
            public bool Called { get; private set; }
            public void DrawGizmos(IEntity entity) => Called = true;
        }

        private sealed class ThrowingGizmoBehaviour : IEntityGizmos
        {
            public void DrawGizmos(IEntity entity) => throw new Exception("oops");
        }
    }
}