using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public class EntityViewCatalogTests
    {
        private EntityViewCatalog _catalog;
        private EntityView _viewA;
        private EntityView _viewB;

        [OneTimeSetUp]
        public void SetUp()
        {
            _catalog = ScriptableObject.CreateInstance<EntityViewCatalog>();

            _viewA = EntityView.Create(new EntityView.CreateArgs
            {
                name = "ViewA"
            });
            _viewB = EntityView.Create(new EntityView.CreateArgs
            {
                name = "ViewB"
            });

            _catalog.prefabs = new List<EntityView> {_viewA, _viewB};
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_catalog);
            Object.DestroyImmediate(_viewA.gameObject);
            Object.DestroyImmediate(_viewB.gameObject);
        }

        [Test]
        public void Count_ReturnsCorrectValue()
        {
            Assert.AreEqual(2, _catalog.Count);
        }

        [Test]
        public void GetPrefab_ByIndex_ReturnsCorrectPrefab()
        {
            var result = _catalog.GetPrefab(1);
            Assert.AreEqual("ViewB", result.Key);
            Assert.AreEqual(_viewB, result.Value);
        }

        [Test]
        public void GetPrefab_ByName_ReturnsCorrectInstance()
        {
            var result = _catalog.GetPrefab("ViewA");
            Assert.AreSame(_viewA, result);
        }

        [Test]
        public void GetPrefab_ByName_ThrowsIfNotFound()
        {
            var ex = Assert.Throws<System.Exception>(() => _catalog.GetPrefab("Missing"));
            StringAssert.Contains("Prefab with name Missing is not found!", ex.Message);
        }

        [Test]
        public void GetName_ReturnsViewName()
        {
            string name = _catalog.GetName(_viewA);
            Assert.AreEqual("ViewA", name);
        }
    }
}