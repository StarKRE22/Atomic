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
            var factory = new MultiEntityFactory<string, EntityDummy>();
            var dummyFactory = new EntityFactoryDummy();

            factory.Register("enemy", dummyFactory);

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
            var factory = new MultiEntityFactory<string, EntityDummy>();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => factory.Create("nonexistent"));
        }

        [Test]
        public void Remove_Should_Delete_Factory()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, EntityDummy>();
            factory.Register("npc", new EntityFactoryDummy());

            // Act
            factory.Unregister("npc");

            // Assert
            Assert.Throws<KeyNotFoundException>(() => factory.Create("npc"));
        }

        [Test]
        public void Constructor_With_KeyValuePairArray_Should_Initialize_Factories()
        {
            // Arrange
            var kvp = new KeyValuePair<string, IEntityFactory<EntityDummy>>[]
            {
                new("enemy", new EntityFactoryDummy()),
                new("boss", new EntityFactoryDummy())
            };

            var factory = new MultiEntityFactory<string, EntityDummy>(kvp);

            // Act & Assert
            Assert.DoesNotThrow(() => factory.Create("enemy"));
            Assert.DoesNotThrow(() => factory.Create("boss"));
        }

        [Test]
        public void Constructor_With_IEnumerable_Should_Initialize_Factories()
        {
            // Arrange
            var dict = new Dictionary<string, IEntityFactory<EntityDummy>>
            {
                ["orc"] = new EntityFactoryDummy(),
                ["goblin"] = new EntityFactoryDummy()
            };

            var factory = new MultiEntityFactory<string, EntityDummy>(dict);

            // Act
            Assert.DoesNotThrow(() => factory.Create("orc"));
            Assert.DoesNotThrow(() => factory.Create("goblin"));
        }

        [Test]
        public void Constructor_With_IReadOnlyDictionary_Should_Initialize_Factories()
        {
            // Arrange
            IReadOnlyDictionary<string, IEntityFactory<EntityDummy>> readonlyDict =
                new Dictionary<string, IEntityFactory<EntityDummy>>
                {
                    ["dragon"] = new EntityFactoryDummy()
                };

            var factory = new MultiEntityFactory<string, EntityDummy>(readonlyDict);

            // Act
            var entity = factory.Create("dragon");

            // Assert
            Assert.NotNull(entity);
        }
        
        [Test]
        public void Add_Should_Throw_When_Key_AlreadyExists()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, EntityDummy>();
            factory.Register("enemy", new EntityFactoryDummy());

            // Act & Assert
            Assert.Throws<ArgumentException>(() => factory.Register("enemy", new EntityFactoryDummy()));
        }
        
        [Test]
        public void Remove_Should_NotThrow_When_Key_NotExists()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, EntityDummy>();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                factory.Unregister("missing_key");
            });
        }
        
        [Test]
        public void Add_WithNullKey_ThrowsException()
        {
            // Arrange
            var factory = new MultiEntityFactory<string, EntityDummy>();
            var dummyFactory = new EntityFactoryDummy();

            // Assert
            Assert.Throws<ArgumentNullException>(() => factory.Register(null, dummyFactory));
        }
        
        [Test]
        public void EmptyFactory_Should_Throw_OnAnyKey()
        {
            var factory = new MultiEntityFactory<string, EntityDummy>();
            Assert.Throws<KeyNotFoundException>(() => factory.Create("anything"));
        }
    }
}