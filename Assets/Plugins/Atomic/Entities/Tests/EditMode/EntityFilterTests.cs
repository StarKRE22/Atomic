using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed class EntityFilterTests
    {
        private static class TagAPI
        {
            public const int WARRIOR = 1;
        }

        private static class ValueAPI
        {
            public const int TEAM = 1;
        }

        private enum TeamType
        {
            BLUE = 0,
            RED = 1
        }

        [Test]
        public void CreateFilterOfBlueWarriors()
        {
            //Arrange:
            var alex = new Entity("Alex");
            alex.AddTag(TagAPI.WARRIOR);
            alex.AddValue(ValueAPI.TEAM, TeamType.BLUE);

            var george = new Entity("George",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.RED}
                }
            );

            var ivan = new Entity("Ivan",
                new[] {TagAPI.WARRIOR}
            );

            var mike = new Entity("Mike",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var world = new EntityWorld("Entity World", alex, george, ivan, mike);
            var filter = new EntityFilter(world, e => e.HasTag(TagAPI.WARRIOR) &&
                                                      e.TryGetValue(ValueAPI.TEAM, out TeamType teamType) &&
                                                      teamType == TeamType.BLUE);
            //Act:
            IReadOnlyCollection<IEntity> filteredEntities = filter.Entities;

            //Assert:
            Assert.AreEqual(2, filteredEntities.Count);
            Assert.IsTrue(filter.HasEntity(alex));
            Assert.IsTrue(filter.HasEntity(mike));

            Assert.IsFalse(filter.HasEntity(ivan));
            Assert.IsFalse(filter.HasEntity(george));
        }

        [Test]
        public void AddEntitiesInWorld()
        {
            //Arrange:
            var alex = new Entity("Alex",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var george = new Entity("George",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var mike = new Entity("Mike",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var ivan = new Entity("Ivan",
                new[] {TagAPI.WARRIOR}
            );

            IEntity wasAddEvent = null;

            var world = new EntityWorld("Entity World", alex, george);
            var filter = new EntityFilter(world, e => e.HasTag(TagAPI.WARRIOR) &&
                                                      e.TryGetValue(ValueAPI.TEAM, out TeamType teamType) &&
                                                      teamType == TeamType.BLUE);

            filter.OnEntityAdded += e => wasAddEvent = e;

            //Act:
            world.AddEntity(ivan);
            Assert.IsNull(wasAddEvent);

            world.AddEntity(mike);
            Assert.AreEqual(mike, wasAddEvent);

            IReadOnlyCollection<IEntity> filteredEntities = filter.Entities;

            //Assert:
            Assert.AreEqual(3, filteredEntities.Count);
            Assert.IsTrue(filter.HasEntity(alex));
            Assert.IsTrue(filter.HasEntity(george));
            Assert.IsTrue(filter.HasEntity(mike));

            Assert.IsFalse(filter.HasEntity(ivan));
        }


        [Test]
        public void DelEntitiesInWorld()
        {
            //Arrange:
            var alex = new Entity("Alex",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var george = new Entity("George",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var mike = new Entity("Mike",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var ivan = new Entity("Ivan",
                new[] {TagAPI.WARRIOR}
            );

            var world = new EntityWorld("Entity World", alex, george, mike, ivan);
            var filter = new EntityFilter(world, e => e.HasTag(TagAPI.WARRIOR) &&
                                                      e.TryGetValue(ValueAPI.TEAM, out TeamType teamType) &&
                                                      teamType == TeamType.BLUE);

            IEntity removedEntity = null;
            filter.OnEntityDeleted += e => removedEntity = e;

            //Pre-assert:
            Assert.AreEqual(3, filter.Entities.Count);
            Assert.IsTrue(filter.HasEntity(mike));

            //Act:
            world.DelEntity(ivan);
            Assert.IsNull(removedEntity);

            world.DelEntity(mike);
            Assert.AreEqual(mike, removedEntity);

            //Assert:
            Assert.AreEqual(2, filter.Entities.Count);
            Assert.IsTrue(filter.HasEntity(alex));
            Assert.IsTrue(filter.HasEntity(george));
            Assert.IsFalse(filter.HasEntity(mike));
            Assert.IsFalse(filter.HasEntity(ivan));
        }

        [Test]
        public void WhenWarriorChangeTeamThenFilterChanged()
        {
            //Arrange:
            var alex = new Entity("Alex",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var george = new Entity("George",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.RED}
                }
            );

            var mike = new Entity("Mike",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

            var ivan = new Entity("Ivan",
                new[] {TagAPI.WARRIOR}
            );
            
            var world = new EntityWorld("Entity World", alex, george, mike, ivan);
            var filter = new EntityFilter(world, e => e.HasTag(TagAPI.WARRIOR) &&
                                                      e.TryGetValue(ValueAPI.TEAM, out TeamType teamType) &&
                                                      teamType == TeamType.BLUE);

            IEntity addedEntity = null;
            IEntity removedEntity = null;

            filter.OnEntityAdded += e => addedEntity = e;
            filter.OnEntityDeleted += e => removedEntity = e;

            //Act:
            Assert.IsFalse(filter.HasEntity(george));
            george.SetValue(ValueAPI.TEAM, TeamType.BLUE);
            Assert.IsTrue(filter.HasEntity(george));
            Assert.AreEqual(george, addedEntity);

            Assert.IsTrue(filter.HasEntity(mike));
            mike.SetValue(ValueAPI.TEAM, TeamType.RED);
            Assert.IsFalse(filter.HasEntity(mike));
            Assert.AreEqual(mike, removedEntity);

            Assert.IsTrue(filter.HasEntity(alex));
            alex.DelValue(ValueAPI.TEAM);
            Assert.IsFalse(filter.HasEntity(alex));
            Assert.AreEqual(alex, removedEntity);
            
            Assert.IsFalse(filter.HasEntity(ivan));
            ivan.AddValue(ValueAPI.TEAM, TeamType.BLUE);
            Assert.IsTrue(filter.HasEntity(ivan));
            Assert.AreEqual(ivan, addedEntity);
        }

        [Test]
        public void WhenTagWarriorChangedThenFilterChanged()
        {
            //Arrange:
            var mike = new Entity("Mike", values:
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );
            
             var alex = new Entity("Alex",
                new[] {TagAPI.WARRIOR},
                new Dictionary<int, object>
                {
                    {ValueAPI.TEAM, TeamType.BLUE}
                }
            );

             var world = new EntityWorld("Entity World", alex, mike);
            var filter = new EntityFilter(world, e => e.HasTag(TagAPI.WARRIOR) &&
                                                      e.TryGetValue(ValueAPI.TEAM, out TeamType teamType) &&
                                                      teamType == TeamType.BLUE);

            IEntity addedEntity = null;
            IEntity removedEntity = null;

            filter.OnEntityAdded += e => addedEntity = e;
            filter.OnEntityDeleted += e => removedEntity = e;

            //Act:
            Assert.IsFalse(filter.HasEntity(mike));
            mike.AddTag(TagAPI.WARRIOR);
            Assert.IsTrue(filter.HasEntity(mike));
            Assert.AreEqual(mike, addedEntity);

            Assert.IsTrue(filter.HasEntity(alex));
            alex.DelTag(TagAPI.WARRIOR);
            Assert.IsFalse(filter.HasEntity(alex));
            Assert.AreEqual(alex, removedEntity);
        }
    }
}