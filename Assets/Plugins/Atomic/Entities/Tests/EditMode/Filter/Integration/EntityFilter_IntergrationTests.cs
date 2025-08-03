using NUnit.Framework;

namespace Atomic.Entities
{
    public class EntityFilter_IntegrationTests
    {
        private enum TeamType
        {
            BLUE = 0,
            RED = 1
        }

        [Test]
        public void Constructor_FilterOfBlueWarriors()
        {
            //Arrange:
            var alex = new Entity("Alex");
            alex.AddTag("Warrior");
            alex.AddValue("Team", TeamType.BLUE);

            var george = new Entity("George");
            george.AddTag("Warrior");
            george.AddValue("Team", TeamType.RED);

            var ivan = new Entity("Ivan");
            ivan.AddTag("Warrior");

            var mike = new Entity("Mike");
            mike.AddTag("Warrior");
            mike.AddValue("Team", TeamType.BLUE);

            var world = new EntityWorld("Entity World", alex, george, ivan, mike);
            var filter = new EntityFilter(world, this.IsBlueWarrior);

            //Assert:
            Assert.AreEqual(2, filter.Count);
            Assert.IsTrue(filter.Contains(alex));
            Assert.IsTrue(filter.Contains(mike));

            Assert.IsFalse(filter.Contains(ivan));
            Assert.IsFalse(filter.Contains(george));
        }

        [Test]
        public void EntityWorld_AddEntities_FilterChanged()
        {
            //Arrange:
            var alex = new Entity("Alex");
            alex.AddTag("Warrior");
            alex.AddValue("Team", TeamType.BLUE);

            var george = new Entity("George");
            george.AddTag("Warrior");
            george.AddValue("Team", TeamType.BLUE);

            var mike = new Entity("Mike");
            mike.AddTag("Warrior");
            mike.AddValue("Team", TeamType.BLUE);

            var ivan = new Entity("Ivan");
            ivan.AddTag("Warrior");

            IEntity wasAddEvent = null;

            var world = new EntityWorld("Entity World", alex, george);
            var filter = new EntityFilter(world, this.IsBlueWarrior);

            filter.OnAdded += e => wasAddEvent = e;

            //Act:
            world.Add(ivan);
            Assert.IsNull(wasAddEvent);

            world.Add(mike);
            Assert.AreEqual(mike, wasAddEvent);

            //Assert:
            Assert.AreEqual(3, filter.Count);
            Assert.IsTrue(filter.Contains(alex));
            Assert.IsTrue(filter.Contains(george));
            Assert.IsTrue(filter.Contains(mike));

            Assert.IsFalse(filter.Contains(ivan));
        }

        [Test]
        public void EntityWorld_RemoveEntities_FilterChanged()
        {
            //Arrange:
            var alex = new Entity("Alex");
            alex.AddTag("Warrior");
            alex.AddValue("Team", TeamType.BLUE);

            var george = new Entity("George");
            george.AddTag("Warrior");
            george.AddValue("Team", TeamType.BLUE);

            var mike = new Entity("Mike");
            mike.AddTag("Warrior");
            mike.AddValue("Team", TeamType.BLUE);

            var ivan = new Entity("Ivan");
            ivan.AddTag("Warrior");

            var world = new EntityWorld("Entity World", alex, george, mike, ivan);
            var filter = new EntityFilter(world, this.IsBlueWarrior);

            IEntity removedEntity = null;
            filter.OnRemoved += e => removedEntity = e;

            //Pre-assert:
            Assert.AreEqual(3, filter.Count);
            Assert.IsTrue(filter.Contains(mike));

            //Act:
            world.Remove(ivan);
            Assert.IsNull(removedEntity);

            world.Remove(mike);
            Assert.AreEqual(mike, removedEntity);

            //Assert:
            Assert.AreEqual(2, filter.Count);
            Assert.IsTrue(filter.Contains(alex));
            Assert.IsTrue(filter.Contains(george));
            Assert.IsFalse(filter.Contains(mike));
            Assert.IsFalse(filter.Contains(ivan));
        }


        [Test]
        public void ChangeWarriorTeam_FilterChanged()
        {
            //Arrange:
            var alex = new Entity("Alex");
            alex.AddTag("Warrior");
            alex.AddValue("Team", TeamType.BLUE);

            var george = new Entity("George");
            george.AddTag("Warrior");
            george.AddValue("Team", TeamType.RED);

            var mike = new Entity("Mike");
            mike.AddTag("Warrior");
            mike.AddValue("Team", TeamType.BLUE);

            var ivan = new Entity("Ivan");
            ivan.AddTag("Warrior");

            var world = new EntityWorld("Entity World", alex, george, mike, ivan);
            var filter = new EntityFilter(world, this.IsBlueWarrior, new ValueEntityTrigger());

            IEntity addedEntity = null;
            IEntity removedEntity = null;

            filter.OnAdded += e => addedEntity = e;
            filter.OnRemoved += e => removedEntity = e;

            //Act:
            Assert.IsFalse(filter.Contains(george));
            george.SetValue("Team", TeamType.BLUE);
            Assert.IsTrue(filter.Contains(george));
            Assert.AreEqual(george, addedEntity);

            Assert.IsTrue(filter.Contains(mike));
            mike.SetValue("Team", TeamType.RED);
            Assert.IsFalse(filter.Contains(mike));
            Assert.AreEqual(mike, removedEntity);

            Assert.IsTrue(filter.Contains(alex));
            alex.DelValue("Team");
            Assert.IsFalse(filter.Contains(alex));
            Assert.AreEqual(alex, removedEntity);

            Assert.IsFalse(filter.Contains(ivan));
            ivan.AddValue("Team", TeamType.BLUE);
            Assert.IsTrue(filter.Contains(ivan));
            Assert.AreEqual(ivan, addedEntity);
        }


        [Test]
        public void ChangeWarriorTag_FilterChanged()
        {
            //Arrange:
            var mike = new Entity("Mike");
            mike.AddValue("Team", TeamType.BLUE);

            var alex = new Entity("Alex");
            alex.AddTag("Warrior");
            alex.AddValue("Team", TeamType.BLUE);

            var world = new EntityWorld("Entity World", alex, mike);
            var filter = new EntityFilter(world, this.IsBlueWarrior, new TagEntityTrigger());

            IEntity addedEntity = null;
            IEntity removedEntity = null;

            filter.OnAdded += e => addedEntity = e;
            filter.OnRemoved += e => removedEntity = e;

            //Assert precondition
            Assert.IsTrue(filter.Contains(alex));
            Assert.IsFalse(filter.Contains(mike));

            //Add tag to Mike:
            mike.AddTag("Warrior");
            Assert.IsTrue(filter.Contains(mike));
            Assert.AreEqual(mike, addedEntity);

            //Remove tag from Alex:
            Assert.IsTrue(filter.Contains(alex));
            alex.DelTag("Warrior");
            Assert.IsFalse(filter.Contains(alex));
            Assert.AreEqual(alex, removedEntity);
        }

        private bool IsBlueWarrior(IEntity e) =>
            e.HasTag("Warrior") &&
            e.TryGetValue("Team", out TeamType teamType) &&
            teamType == TeamType.BLUE;
    }
}