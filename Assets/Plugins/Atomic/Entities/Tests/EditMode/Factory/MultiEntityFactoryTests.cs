using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public class MultiEntityFactory_Tests
    {
        [Test]
        public void Add_And_Create_Should_Return_CorrectEntity()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();
            var dummyFactory = new DummyEntityFactory();

            factory.Add("enemy", dummyFactory);

            // Act
            var result = factory.Create("enemy");

            // Assert
            Assert.NotNull(result);
            Assert.AreSame(dummyFactory.Created, result);
        }

        [Test]
        public void Create_Should_Throw_When_Key_Not_Found()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();

            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => factory.Create("nonexistent"));
            StringAssert.Contains("Entity Factory with key", ex.Message);
        }

        [Test]
        public void Remove_Should_Delete_Factory()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();
            factory.Add("npc", new DummyEntityFactory());

            // Act
            factory.Remove("npc");

            // Assert
            Assert.Throws<KeyNotFoundException>(() => factory.Create("npc"));
        }

        [Test]
        public void Constructor_With_KeyValuePairArray_Should_Initialize_Factories()
        {
            // Arrange
            var kvp = new KeyValuePair<string, IEntityFactory<DummyEntity>>[]
            {
                new("enemy", new DummyEntityFactory()),
                new("boss", new DummyEntityFactory())
            };

            var factory = new MultiEntityFactory<string, DummyEntity>(kvp);

            // Act & Assert
            Assert.DoesNotThrow(() => factory.Create("enemy"));
            Assert.DoesNotThrow(() => factory.Create("boss"));
        }

        [Test]
        public void Constructor_With_IEnumerable_Should_Initialize_Factories()
        {
            // Arrange
            var dict = new Dictionary<string, IEntityFactory<DummyEntity>>
            {
                ["orc"] = new DummyEntityFactory(),
                ["goblin"] = new DummyEntityFactory()
            };

            var factory = new MultiEntityFactory<string, DummyEntity>(dict);

            // Act
            Assert.DoesNotThrow(() => factory.Create("orc"));
            Assert.DoesNotThrow(() => factory.Create("goblin"));
        }

        [Test]
        public void Constructor_With_IReadOnlyDictionary_Should_Initialize_Factories()
        {
            // Arrange
            IReadOnlyDictionary<string, IEntityFactory<DummyEntity>> readonlyDict =
                new Dictionary<string, IEntityFactory<DummyEntity>>
                {
                    ["dragon"] = new DummyEntityFactory()
                };

            var factory = new MultiEntityFactory<string, DummyEntity>(readonlyDict);

            // Act
            var entity = factory.Create("dragon");

            // Assert
            Assert.NotNull(entity);
        }
        
        [Test]
        public void Add_Should_Throw_When_Key_AlreadyExists()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();
            factory.Add("enemy", new DummyEntityFactory());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => factory.Add("enemy", new DummyEntityFactory()));
        }
        
        [Test]
        public void Remove_Should_NotThrow_When_Key_NotExists()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                factory.Remove("missing_key");
            });
        }
        
        [Test]
        public void Add_WithNullKey_ThrowsException()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, DummyEntity>();
            var dummyFactory = new DummyEntityFactory();

            // Assert
            Assert.Throws<ArgumentNullException>(() => factory.Add(null, dummyFactory));
        }
        
        [Test]
        public void EmptyFactory_Should_Throw_OnAnyKey()
        {
            var factory = new MultiEntityFactory<string, DummyEntity>();
            Assert.Throws<KeyNotFoundException>(() => factory.Create("anything"));
        }
    }
}