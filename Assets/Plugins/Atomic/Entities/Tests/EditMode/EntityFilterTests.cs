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
            IReadOnlyCollection<IEntity> filteredEntities = filter.GetAll();

            //Assert:
            Assert.AreEqual(2, filteredEntities.Count);
            Assert.IsTrue(filter.Has(alex));
            Assert.IsTrue(filter.Has(mike));

            Assert.IsFalse(filter.Has(ivan));
            Assert.IsFalse(filter.Has(george));
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

            filter.OnAdded += e => wasAddEvent = e;

            //Act:
            world.Add(ivan);
            Assert.IsNull(wasAddEvent);

            world.Add(mike);
            Assert.AreEqual(mike, wasAddEvent);

            IReadOnlyCollection<IEntity> filteredEntities = filter.GetAll();

            //Assert:
            Assert.AreEqual(3, filteredEntities.Count);
            Assert.IsTrue(filter.Has(alex));
            Assert.IsTrue(filter.Has(george));
            Assert.IsTrue(filter.Has(mike));

            Assert.IsFalse(filter.Has(ivan));
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
            filter.OnDeleted += e => removedEntity = e;

            //Pre-assert:
            Assert.AreEqual(3, filter.GetAll().Length);
            Assert.IsTrue(filter.Has(mike));

            //Act:
            world.Del(ivan);
            Assert.IsNull(removedEntity);

            world.Del(mike);
            Assert.AreEqual(mike, removedEntity);

            //Assert:
            Assert.AreEqual(2, filter.GetAll().Length);
            Assert.IsTrue(filter.Has(alex));
            Assert.IsTrue(filter.Has(george));
            Assert.IsFalse(filter.Has(mike));
            Assert.IsFalse(filter.Has(ivan));
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

            filter.OnAdded += e => addedEntity = e;
            filter.OnDeleted += e => removedEntity = e;

            //Act:
            Assert.IsFalse(filter.Has(george));
            george.SetValue(ValueAPI.TEAM, TeamType.BLUE);
            Assert.IsTrue(filter.Has(george));
            Assert.AreEqual(george, addedEntity);

            Assert.IsTrue(filter.Has(mike));
            mike.SetValue(ValueAPI.TEAM, TeamType.RED);
            Assert.IsFalse(filter.Has(mike));
            Assert.AreEqual(mike, removedEntity);

            Assert.IsTrue(filter.Has(alex));
            alex.DelValue(ValueAPI.TEAM);
            Assert.IsFalse(filter.Has(alex));
            Assert.AreEqual(alex, removedEntity);
            
            Assert.IsFalse(filter.Has(ivan));
            ivan.AddValue(ValueAPI.TEAM, TeamType.BLUE);
            Assert.IsTrue(filter.Has(ivan));
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

            filter.OnAdded += e => addedEntity = e;
            filter.OnDeleted += e => removedEntity = e;

            //Act:
            Assert.IsFalse(filter.Has(mike));
            mike.AddTag(TagAPI.WARRIOR);
            Assert.IsTrue(filter.Has(mike));
            Assert.AreEqual(mike, addedEntity);

            Assert.IsTrue(filter.Has(alex));
            alex.DelTag(TagAPI.WARRIOR);
            Assert.IsFalse(filter.Has(alex));
            Assert.AreEqual(alex, removedEntity);
        }
    }
}