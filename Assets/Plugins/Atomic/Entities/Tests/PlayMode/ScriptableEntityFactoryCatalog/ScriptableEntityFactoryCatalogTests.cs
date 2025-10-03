using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class ScriptableMultiEntityFactoryTests
    {
        private ScriptableMultiEntityFactory factory;
        private ScriptableEntityFactoryDummy _factoryA;
        private ScriptableEntityFactoryDummy _factoryB;

        [SetUp]
        public void Setup()
        {
            _factoryA = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryA.name = "Enemy";

            _factoryB = ScriptableObject.CreateInstance<ScriptableEntityFactoryDummy>();
            _factoryB.name = "Ally";

            factory = ScriptableObject.CreateInstance<ScriptableMultiEntityFactory>();
            factory._factories = new ScriptableEntityFactory[] {_factoryA, _factoryB};
        }
        

        [Test]
        public void ContainsKey_Should_ReturnTrue_WhenKeyExists()
        {
            Assert.IsTrue(factory.Contains("Ally"));
            Assert.IsFalse(factory.Contains("Unknown"));
        }
    }
}