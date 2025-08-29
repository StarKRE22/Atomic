using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public class EntityWorld_Spawn_Tests
    {
        [Test]
        public void Spawn_SetsSpawnedFlag()
        {
            var world = new EntityWorld<Entity>();
            world.Spawn();
            Assert.IsTrue(world.IsSpawned);
        }

        [Test]
        public void Spawn_CallsEntitySpawn_OnAllEntities()
        {
            var e1 = new Entity();
            var e2 = new Entity();
            var world = new EntityWorld<Entity>(e1, e2);

            world.Spawn();

            Assert.IsTrue(e1.Initialized);
            Assert.IsTrue(e2.Initialized);
        }

        [Test]
        public void Spawn_RaisesOnSpawnedEvent()
        {
            var world = new EntityWorld<Entity>();
            bool called = false;
            world.OnSpawned += () => called = true;

            world.Spawn();

            Assert.IsTrue(called);
        }

        [Test]
        public void Spawn_CalledTwice_DoesNotCallAgain()
        {
            var entity = new Entity();
            var world = new EntityWorld<Entity>(entity);

            int eventCallCount = 0;
            world.OnSpawned += () => eventCallCount++;

            world.Spawn();
            world.Spawn(); // repeated

            Assert.AreEqual(1, eventCallCount);
            Assert.IsTrue(entity.Initialized); // still true
        }


        [Test]
        public void Spawn_Calls_EntityBehaviourSpawn()
        {
            //Arrange:
            var initBehaviour = new DummyEntityBehaviour();
            var entity = new Entity("E");
            entity.AddBehaviour(initBehaviour);

            var entityWorld = new EntityWorld("Test", entity);
            var wasSpawn = false;

            //Act:
            entity.OnInitialized += () => wasSpawn = true;
            entityWorld.Spawn();

            //Assert:
            Assert.IsTrue(wasSpawn);
            Assert.IsTrue(initBehaviour.Initialized);
        }
    }
}