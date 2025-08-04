using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class PrefabEntityPoolTests
    {
        private GameObject _root;
        private TestPrefabEntityPool _pool;
        private SceneEntity _prefab;

        [SetUp]
        public void SetUp()
        {
            _root = new GameObject("PoolRoot");
            _pool = _root.AddComponent<TestPrefabEntityPool>();

            _prefab = SceneEntity.Create<SceneEntity>();
            _prefab.name = "TestEntity";
            _prefab.gameObject.SetActive(false);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_pool?.gameObject);
            Object.DestroyImmediate(_prefab?.gameObject);
        }

        [UnityTest]
        public IEnumerator Init_CreatesInactiveEntitiesInNamedPool()
        {
            _pool.Init(_prefab, 3);
            yield return null;

            Assert.AreEqual(3, _pool.GetStackCount("TestEntity"));

            foreach (var entity in _pool.GetStack("TestEntity"))
                Assert.IsFalse(entity.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator Rent_ReturnsEntity_ActivatesIt()
        {
            _pool.Init(_prefab, 1);
            yield return null;

            var entity = _pool.Rent(_prefab);
            Assert.IsNotNull(entity);
            Assert.IsTrue(entity.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator Rent_WithPositionAndRotation_SetsTransform()
        {
            var position = new Vector3(1, 2, 3);
            var rotation = Quaternion.Euler(0, 90, 0);

            var entity = _pool.Rent(_prefab, position, rotation);
            yield return null;

            Assert.AreEqual(position, entity.transform.position);
            Assert.AreEqual(rotation.eulerAngles, entity.transform.rotation.eulerAngles);
        }

        [UnityTest]
        public IEnumerator Return_DeactivatesAndReparents()
        {
            var entity = _pool.Rent(_prefab);
            _pool.Return(entity);
            yield return null;

            Assert.IsFalse(entity.gameObject.activeSelf);
            Assert.AreEqual(_pool.GetContainer("TestEntity"), entity.transform.parent);
            Assert.AreEqual(1, _pool.GetStackCount("TestEntity"));
        }

        [UnityTest]
        public IEnumerator Return_Twice_SecondIgnored()
        {
            var entity = _pool.Rent(_prefab);
            _pool.Return(entity);
            _pool.Return(entity);
            yield return null;

            Assert.AreEqual(1, _pool.GetStackCount("TestEntity"));
        }

        [UnityTest]
        public IEnumerator Dispose_RemovesAllEntitiesAndContainers()
        {
            _pool.Init(_prefab, 2);
            _pool.Dispose(_prefab);
            yield return null;

            Assert.AreEqual(0, _pool.GetAllPoolNames().Count);
        }

        [UnityTest]
        public IEnumerator Dispose_All_RemovesEverything()
        {
            _pool.Init(_prefab, 2);
            var otherPrefab = SceneEntity.Create<SceneEntity>();
            otherPrefab.name = "Other";
            _pool.Init(otherPrefab, 1);

            _pool.Dispose();
            yield return null;

            Assert.AreEqual(0, _pool.GetAllPoolNames().Count);
            Object.DestroyImmediate(otherPrefab.gameObject);
        }

        [UnityTest]
        public IEnumerator Rent_WithoutInit_CreatesNewPool()
        {
            var entity = _pool.Rent(_prefab);
            yield return null;

            Assert.IsNotNull(entity);
            Assert.AreEqual("TestEntity", entity.name);
            Assert.IsTrue(_pool.HasPool("TestEntity"));
        }

        public class TestPrefabEntityPool : PrefabEntityPool<SceneEntity>
        {
            public Stack<SceneEntity> GetStack(string name)
            {
                var field = typeof(PrefabEntityPool<SceneEntity>)
                    .GetField("_pools",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                Dictionary<string, Pool> pools = (Dictionary<string, Pool>) field!.GetValue(this);

                Pool pool = pools[name];
                return pool.stack;
            }

            public int GetStackCount(string name) => GetStack(name).Count;

            public Transform GetContainer(string name)
            {
                var field = typeof(PrefabEntityPool<SceneEntity>)
                    .GetField("_pools",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var pools = (Dictionary<string, Pool>) field!.GetValue(this);

                Pool pool = pools[name];
                return pool.container;
            }

            public List<string> GetAllPoolNames()
            {
                var field = typeof(PrefabEntityPool<SceneEntity>)
                    .GetField("_pools",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var pools = (Dictionary<string, Pool>) field!.GetValue(this);
                return new List<string>(pools.Keys);
            }

            public bool HasPool(string name)
            {
                var field = typeof(PrefabEntityPool<SceneEntity>)
                    .GetField("_pools",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var pools = (Dictionary<string, Pool>) field!.GetValue(this);
                return pools.ContainsKey(name);
            }
        }
    }
}