using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class SceneEntityFactoryCatalogTests
    {
        private ScriptableEntityCatalog _catalog;
        private ScriptableEntityFactoryDummy _factoryA;
        private ScriptableEntityFactoryDummy _factoryB;

        [SetUp]
        public void Setup()
        {
            _factoryA = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryA.name = "Enemy";

            _factoryB = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryB.name = "Ally";

            _catalog = ScriptableObject.CreateInstance<ScriptableEntityCatalog>();
            _catalog._factories = new ScriptableEntityFactory[] {_factoryA, _factoryB};
        }

        [Test]
        public void GetKey_Should_UseAssetName()
        {
            // Act
            var key = _catalog.Keys;

            // Assert
            CollectionAssert.Contains(key, "Enemy");
            CollectionAssert.Contains(key, "Ally");
        }

        [Test]
        public void Count_Should_MatchFactoryArray()
        {
            Assert.AreEqual(2, _catalog.Count);
        }

        [Test]
        public void Indexer_Should_ReturnCorrectFactory()
        {
            var enemyFactory = _catalog["Enemy"];
            Assert.IsNotNull(enemyFactory);
            Assert.AreSame(_factoryA, enemyFactory);
        }

        [Test]
        public void ContainsKey_Should_ReturnTrue_WhenKeyExists()
        {
            Assert.IsTrue(_catalog.ContainsKey("Ally"));
            Assert.IsFalse(_catalog.ContainsKey("Unknown"));
        }

        [Test]
        public void TryGetValue_Should_ReturnFactoryIfExists()
        {
            bool found = _catalog.TryGetValue("Enemy", out var factory);
            Assert.IsTrue(found);
            Assert.AreSame(_factoryA, factory);
        }

        [Test]
        public void Values_Should_ContainAllFactories()
        {
            CollectionAssert.Contains(_catalog.Values, _factoryA);
            CollectionAssert.Contains(_catalog.Values, _factoryB);
        }

        [Test]
        public void Keys_Should_ContainCorrectKeys()
        {
            CollectionAssert.AreEquivalent(new[] {"Enemy", "Ally"}, _catalog.Keys);
        }

        [Test]
        public void GetEnumerator_Should_IterateAll()
        {
            var dict = new Dictionary<string, IEntityFactory<IEntity>>();
            foreach (var kvp in _catalog)
                dict[kvp.Key] = kvp.Value;

            Assert.AreEqual(2, dict.Count);
            Assert.AreSame(_factoryA, dict["Enemy"]);
            Assert.AreSame(_factoryB, dict["Ally"]);
        }

        [Test]
        public void DuplicateKeys_Should_WarnAndIgnoreSecond()
        {
            var duplicate = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            duplicate.name = "Enemy"; // same as _factoryA

            var localCatalog = ScriptableObject.CreateInstance<ScriptableEntityCatalog>();
            localCatalog._factories = new ScriptableEntityFactory[] {_factoryA, duplicate};

            // force init
            _ = localCatalog.Count;

            // only 1 should be registered
            Assert.AreEqual(1, localCatalog.Count);
            Assert.AreSame(_factoryA, localCatalog["Enemy"]);
        }
    }
}