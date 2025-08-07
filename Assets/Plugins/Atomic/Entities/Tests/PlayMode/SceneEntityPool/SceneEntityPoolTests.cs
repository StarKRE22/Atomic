using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Atomic.Entities
{
    public class SceneEntityPoolTests
    {
        private SceneEntity _prefab;
        private SceneEntityPool _pool;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _prefab = SceneEntity.Create();
        }
        
        [SetUp]
        public void SetUp()
        {
            _pool = SceneEntityPool.Create(new SceneEntityPool<SceneEntity>.CreateArgs
            {
                prefab = _prefab,
                initOnAwake = false
            });
        }

        [UnityTearDown]
        public IEnumerator TearDown()
        {
            SceneEntityPool.Destroy(_pool);
            yield return null;
        }

        [UnityTest]
        public IEnumerator Init_CreatesEntities_DeactivatesThem()
        {
            _pool.Init(3);
            yield return null;

            Assert.AreEqual(3, _pool._pooledEntities.Count);
            foreach (var e in _pool._pooledEntities)
                Assert.IsFalse(e.gameObject.activeSelf);
        }

        [UnityTest]
        public IEnumerator Rent_FromInitializedPool_ActivatesEntity()
        {
            _pool.Init(1);
            yield return null;

            var entity = _pool.Rent();
            Assert.NotNull(entity);
            Assert.IsTrue(entity.gameObject.activeSelf);
            Assert.AreEqual(0, _pool._pooledEntities.Count);
        }

        [UnityTest]
        public IEnumerator Return_DeactivatesEntityAndParentsIt()
        {
            var entity = _pool.Rent();
            _pool.Return(entity);

            yield return null;

            Assert.IsFalse(entity.gameObject.activeSelf);
            Assert.AreEqual(_pool.transform, entity.transform.parent);
            Assert.AreEqual(1, _pool._pooledEntities.Count);
        }

        [UnityTest]
        public IEnumerator Rent_WhenPoolIsEmpty_CreatesNewEntity()
        {
            var entity = _pool.Rent();
            yield return null;

            Assert.IsNotNull(entity);
            Assert.AreEqual(1, _pool._rentEntities.Count);
        }

        [UnityTest]
        public IEnumerator Dispose_ClearsPoolAndDestroysEntities()
        {
            _pool.Init(2);
            var e1 = _pool.Rent();

            Assert.AreEqual(1, _pool._pooledEntities.Count);
            Assert.AreEqual(1, _pool._rentEntities.Count);
            
            _pool.Dispose();
            yield return null;

            Assert.AreEqual(0, _pool._pooledEntities.Count);
            Assert.AreEqual(0, _pool._rentEntities.Count);
        }

        [UnityTest]
        public IEnumerator Awake_AutoInit_CreatesEntities()
        {
            _pool = SceneEntityPool.Create(new SceneEntityPool<SceneEntity>.CreateArgs
            {
                prefab = _prefab,
                initOnAwake = true,
                initialCount = 2
            });
            
            yield return null;

            Assert.AreEqual(2, _pool._pooledEntities.Count);
        }

        [UnityTest]
        public IEnumerator Return_UntrackedEntity_ShowsWarning()
        {
            var foreign = SceneEntity.Create();
            LogAssert.Expect(LogType.Warning, $"[EntityPool] Attempted to return untracked entity: {foreign}");
            _pool.Return(foreign);
            yield return null;
        }
    }
}