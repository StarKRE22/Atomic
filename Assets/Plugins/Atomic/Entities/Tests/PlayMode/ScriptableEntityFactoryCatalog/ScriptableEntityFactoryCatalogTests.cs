using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class ScriptableEntityFactoryCatalogTests
    {
        private ScriptableEntityFactoryCatalog factoryCatalog;
        private ScriptableEntityFactoryDummy _factoryA;
        private ScriptableEntityFactoryDummy _factoryB;

        [SetUp]
        public void Setup()
        {
            _factoryA = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryA.name = "Enemy";

            _factoryB = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryB.name = "Ally";

            factoryCatalog = ScriptableObject.CreateInstance<ScriptableEntityFactoryCatalog>();
            factoryCatalog._factories = new ScriptableEntityFactory[] {_factoryA, _factoryB};
        }

        [Test]
        public void GetKey_Should_UseAssetName()
        {
            // Act
            var key = factoryCatalog.Keys;

            // Assert
            CollectionAssert.Contains(key, "Enemy");
            CollectionAssert.Contains(key, "Ally");
        }

        [Test]
        public void Count_Should_MatchFactoryArray()
        {
            Assert.AreEqual(2, factoryCatalog.Count);
        }

        [Test]
        public void Indexer_Should_ReturnCorrectFactory()
        {
            var enemyFactory = factoryCatalog["Enemy"];
            Assert.IsNotNull(enemyFactory);
            Assert.AreSame(_factoryA, enemyFactory);
        }

        [Test]
        public void ContainsKey_Should_ReturnTrue_WhenKeyExists()
        {
            Assert.IsTrue(factoryCatalog.ContainsKey("Ally"));
            Assert.IsFalse(factoryCatalog.ContainsKey("Unknown"));
        }

        [Test]
        public void TryGetValue_Should_ReturnFactoryIfExists()
        {
            bool found = factoryCatalog.TryGetValue("Enemy", out var factory);
            Assert.IsTrue(found);
            Assert.AreSame(_factoryA, factory);
        }

        [Test]
        public void Values_Should_ContainAllFactories()
        {
            CollectionAssert.Contains(factoryCatalog.Values, _factoryA);
            CollectionAssert.Contains(factoryCatalog.Values, _factoryB);
        }

        [Test]
        public void Keys_Should_ContainCorrectKeys()
        {
            CollectionAssert.AreEquivalent(new[] {"Enemy", "Ally"}, factoryCatalog.Keys);
        }

        [Test]
        public void GetEnumerator_Should_IterateAll()
        {
            var dict = new Dictionary<string, IEntityFactory<IEntity>>();
            foreach (var kvp in factoryCatalog)
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

            var localCatalog = ScriptableObject.CreateInstance<ScriptableEntityFactoryCatalog>();
            localCatalog._factories = new ScriptableEntityFactory[] {_factoryA, duplicate};

            // force init
            _ = localCatalog.Count;

            // only 1 should be registered
            Assert.AreEqual(1, localCatalog.Count);
            Assert.AreSame(_factoryA, localCatalog["Enemy"]);
        }
    }
}