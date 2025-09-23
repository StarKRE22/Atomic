using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Atomic.Entities
{
    public partial class EntityViewTests
    {
        
        [Test]
        public void Show_ThrowsOnNullEntity()
        {
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();

            Assert.Throws<ArgumentNullException>(() => view.Show(null));

            Object.DestroyImmediate(go);
        }
        
        [Test]
        public void Show_AssignsEntity()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.AreEqual(entity, view.Entity);

            //Dispose:
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Show_IsVisible_True()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();
            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.IsTrue(view.IsVisible);

            //Dispose:
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Show_ActivatesGameObjects_ByDefault()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();

            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.IsTrue(view.gameObject.activeSelf);

            //Dispose:
            Object.DestroyImmediate(go);
        }

        [Test]
        public void Show_ActivatesGameObjects_WhenControlGameObject_True()
        {
            //Arrange:
            var go = new GameObject();
            go.SetActive(false);

            var view = go.AddComponent<EntityView>();
            view.controlGameObject = true;

            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.IsTrue(view.gameObject.activeSelf);

            //Dispose:
            Object.DestroyImmediate(go);
        }


        [Test]
        public void Show_NotActivatesGameObjects_WhenControlGameObject_False()
        {
            //Arrange:
            var go = new GameObject();
            go.SetActive(false);

            var view = go.AddComponent<EntityView>();
            view.controlGameObject = false;

            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.IsFalse(view.gameObject.activeSelf);

            //Dispose:
            Object.DestroyImmediate(go);
        }


        [Test]
        public void Show_AppliesAspects()
        {
            //Arrange:
            var go = new GameObject();
            var view = go.AddComponent<EntityView>();

            var aspect = go.AddComponent<SceneEntityInstallerStub>();
            view.installers = new List<SceneEntityInstaller> {aspect};

            var entity = new Entity();

            //Act:
            view.Show(entity);

            //Assert:
            Assert.IsTrue(aspect.Installed);
            
            //Dispose:
            Object.DestroyImmediate(go);
        }
    }
}