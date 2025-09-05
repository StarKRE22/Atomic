using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class EntityViewPoolTests
    {
        private GameObject _go;
        private EntityViewPool _pool;
        private Transform _container;
        private BehaviourEntityView _prefab;

        [SetUp]
        public void SetUp()
        {
            _go = new GameObject("Pool");
            _container = new GameObject("Container").transform;

            _pool = _go.AddComponent<EntityViewPool>();
            _pool._container = _container;

            _prefab = BehaviourEntityView.Create();
            _prefab.name = "Soldier";
            _prefab.gameObject.SetActive(false);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_go);
            Object.DestroyImmediate(_container.gameObject);
            Object.DestroyImmediate(_prefab.gameObject);
        }

        [Test]
        public void AddPrefab_ThenRent_CreatesInstance()
        {
            _pool.AddPrefab("Soldier", _prefab);

            var view = _pool.Rent("Soldier");

            Assert.IsNotNull(view);
            Assert.AreEqual("Soldier(Clone)", view.name);
            Assert.AreEqual(_container, view.transform.parent);
        }

        [Test]
        public void Return_ThenRent_ReturnsSameObject()
        {
            _pool.AddPrefab("Soldier", _prefab);

            var view = _pool.Rent("Soldier");
            _pool.Return("Soldier", view);
            var view2 = _pool.Rent("Soldier");

            Assert.AreSame(view, view2);
        }

        [Test]
        public void Rent_MissingPrefab_Throws()
        {
            Assert.Throws<KeyNotFoundException>(() => _pool.Rent("Unknown"));
        }

        [UnityTest]
        public IEnumerator Clear_DestroysAllPooledViews()
        {
            _pool.AddPrefab("Soldier", _prefab);

            var v1 = _pool.Rent("Soldier");
            _pool.Return("Soldier", v1);

            _pool.Clear();

            yield return null;
            Assert.IsTrue(v1 == null || v1.Equals(null));
        }

        [Test]
        public void AddPrefabs_FromCatalog_AddsAll()
        {
            var catalog = ScriptableObject.CreateInstance<EntityViewCatalog>();
            var another = BehaviourEntityView.Create();
            another.name = "Tank";
            catalog._prefabs = new List<EntityView> {another};

            _pool.AddPrefabs(catalog);

            var view = _pool.Rent("Tank");

            Assert.NotNull(view);
            Object.DestroyImmediate(another.gameObject);
            Object.DestroyImmediate(catalog);
        }

        [Test]
        public void RemovePrefab_PreventsFurtherRenting()
        {
            _pool.AddPrefab("Soldier", _prefab);
            _pool.RemovePrefab("Soldier");

            Assert.Throws<KeyNotFoundException>(() => _pool.Rent("Soldier"));
        }

        [Test]
        public void RemovePrefabs_FromCatalog_RemovesAll()
        {
            var catalog = ScriptableObject.CreateInstance<EntityViewCatalog>();
            var v = BehaviourEntityView.Create();
            v.name = "Sniper";
            catalog._prefabs = new List<EntityView> {v};

            _pool.AddPrefabs(catalog);
            _pool.RemovePrefabs(catalog);

            Assert.Throws<KeyNotFoundException>(() => _pool.Rent("Sniper"));

            Object.DestroyImmediate(v.gameObject);
            Object.DestroyImmediate(catalog);
        }
    }
}