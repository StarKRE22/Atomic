using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class EntityViewTests
    {
        [Test]
        public void Hide_DoesNothing_WhenEntityIsNull()
        {
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();

            // Should not throw
            Assert.DoesNotThrow(() => view.Hide());

            Object.DestroyImmediate(go);
        }
        
        [Test]
        public void Hide_DiscardAspects()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();

            var installer = go.AddComponent<SceneEntityInstallerStub>();
            view.installers = new List<SceneEntityInstaller> {installer};

            var entity = new Entity();
            view.Show(entity);

            //Act:
            view.Hide();
            
            //Assert:
            Assert.IsTrue(installer.Uninstalled);

            //Dispose:
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Hide_RemovesEntity()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            var entity = new Entity();

            view.Show(entity);

            //Act:
            view.Hide();

            //Assert:
            Assert.IsNull(view.Entity);

            //Dispose:
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Hide_IsNotVisible()
        {
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            var entity = new Entity();

            view.Show(entity);
            view.Hide();

            Assert.IsFalse(view.IsVisible);

            Object.DestroyImmediate(go);
        }

        [Test]
        public void Hide_DeactivatesGameObject_ByDefault()
        {
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            var entity = new Entity();

            view.Show(entity);
            view.Hide();

            Assert.IsFalse(view.IsVisible);
            Assert.IsFalse(view.gameObject.activeSelf);

            Object.DestroyImmediate(go);
        }

        [Test]
        public void Hide_NotDeactivatesGameObject_WhenControlGameObjectFalse()
        {
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            view.controlGameObject = false;
            
            var entity = new Entity();

            view.Show(entity);
            view.Hide();

            Assert.IsTrue(view.gameObject.activeSelf);

            Object.DestroyImmediate(go);
        }
    }
}