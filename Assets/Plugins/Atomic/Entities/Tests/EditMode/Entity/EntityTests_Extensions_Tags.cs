using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Entities
{
    public sealed partial class EntityTests
    {
        // ──────────────────────────────────────────────────────────────
        //  Setup / Teardown — isolate EntityKeyStore between tests
        // ──────────────────────────────────────────────────────────────

        [SetUp]
        public void SetUp()
        {
            EntityKeyStore.Reset();
        }

        [TearDown]
        public void TearDown()
        {
            EntityKeyStore.Reset();
        }

        // ══════════════════════════════════════════════════════════════
        //  AddTag extensions
        // ══════════════════════════════════════════════════════════════

        #region AddTag (string)

        [Test]
        public void AddTag_ByString_ReturnsTrue_WhenNew()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.AddTag("enemy");

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag("enemy"));
        }

        [Test]
        public void AddTag_ByString_ReturnsFalse_WhenDuplicate()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("enemy");

            //Act:
            bool result = entity.AddTag("enemy");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void AddTag_ByString_MapsSameId_EveryCall()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            entity.AddTag("health");
            int expectedId = EntityKeyStore.NameToId("health");

            //Assert:
            Assert.IsTrue(entity.HasTag(expectedId));
        }

        [Test]
        public void AddTag_ByString_DifferentNamesDifferentTags()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            entity.AddTag("alpha");
            entity.AddTag("beta");

            //Assert:
            Assert.IsTrue(entity.HasTag("alpha"));
            Assert.IsTrue(entity.HasTag("beta"));
            Assert.AreEqual(2, entity.TagCount);
        }

        #endregion

        #region AddTag (TagKey)

        [Test]
        public void AddTag_ByTagKey_ReturnsTrue_WhenNew()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("fire");

            //Act:
            bool result = entity.AddTag(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag("fire"));
        }

        [Test]
        public void AddTag_ByTagKey_ReturnsFalse_WhenDuplicate()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("fire");
            entity.AddTag(key);

            //Act:
            bool result = entity.AddTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void AddTag_ByTagKey_MatchesStringEquivalent()
        {
            //Arrange:
            var entity = new Entity();
            int idFromString = EntityKeyStore.NameToId("poison");
            var key = new TagKey(idFromString);

            //Act:
            entity.AddTag(key);

            //Assert:
            Assert.IsTrue(entity.HasTag("poison"));
            Assert.IsTrue(entity.HasTag(idFromString));
        }

        #endregion

        #region AddTag<E> (TagKey<E>)

        [Test]
        public void AddTag_GenericTagKey_ReturnsTrue_WhenNew()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("magic");

            //Act:
            bool result = entity.AddTag(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag("magic"));
        }

        [Test]
        public void AddTag_GenericTagKey_ReturnsFalse_WhenDuplicate()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("magic");
            entity.AddTag(key);

            //Act:
            bool result = entity.AddTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void AddTag_GenericTagKey_MatchesStringEquivalent()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("speed");

            //Act:
            entity.AddTag(key);

            //Assert:
            int expectedId = EntityKeyStore.NameToId("speed");
            Assert.IsTrue(entity.HasTag(expectedId));
        }

        #endregion

        #region AddTag (string, out int id)

        [Test]
        public void AddTag_WithOutId_ReturnsTrue_WhenNew()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.AddTag("stealth", out int id);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag(id));
            Assert.AreEqual(EntityKeyStore.NameToId("stealth"), id);
        }

        [Test]
        public void AddTag_WithOutId_ReturnsFalse_WhenDuplicate()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("stealth");

            //Act:
            bool result = entity.AddTag("stealth", out int id);

            //Assert:
            Assert.IsFalse(result);
            Assert.AreEqual(EntityKeyStore.NameToId("stealth"), id);
        }

        [Test]
        public void AddTag_WithOutId_OutputIdMatchesNameToId()
        {
            //Arrange:
            var entity = new Entity();
            string tagName = "crit";

            //Act:
            entity.AddTag(tagName, out int id);

            //Assert:
            Assert.AreEqual(EntityKeyStore.NameToId(tagName), id);
            Assert.IsTrue(entity.HasTag(id));
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  AddTags extensions
        // ══════════════════════════════════════════════════════════════

        #region AddTags (IEnumerable<string>)

        [Test]
        public void AddTags_ByStrings_AddsAllTags()
        {
            //Arrange:
            var entity = new Entity();
            var tags = new[] { "alpha", "beta", "gamma" };

            //Act:
            entity.AddTags(tags);

            //Assert:
            Assert.IsTrue(entity.HasTag("alpha"));
            Assert.IsTrue(entity.HasTag("beta"));
            Assert.IsTrue(entity.HasTag("gamma"));
            Assert.AreEqual(3, entity.TagCount);
        }

        [Test]
        public void AddTags_ByStrings_HandlesEmptyCollection()
        {
            //Arrange:
            var entity = new Entity();
            var tags = new string[0];

            //Act:
            entity.AddTags(tags);

            //Assert:
            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void AddTags_ByStrings_IgnoresNullCollection()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            entity.AddTags((IEnumerable<string>)null);

            //Assert:
            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void AddTags_ByStrings_DeduplicatesIdenticalNames()
        {
            //Arrange:
            var entity = new Entity();
            var tags = new[] { "same", "same", "same" };

            //Act:
            entity.AddTags(tags);

            //Assert:
            Assert.AreEqual(1, entity.TagCount);
        }

        #endregion

        #region AddTags (IEnumerable<TagKey>)

        [Test]
        public void AddTags_ByTagKeys_AddsAllTags()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey("fire"),
                new TagKey("water"),
                new TagKey("earth")
            };

            //Act:
            entity.AddTags(keys);

            //Assert:
            Assert.IsTrue(entity.HasTag("fire"));
            Assert.IsTrue(entity.HasTag("water"));
            Assert.IsTrue(entity.HasTag("earth"));
            Assert.AreEqual(3, entity.TagCount);
        }

        [Test]
        public void AddTags_ByTagKeys_IgnoresNullCollection()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            entity.AddTags((IEnumerable<TagKey>)null);

            //Assert:
            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void AddTags_ByTagKeys_DeduplicatesEquivalentKeys()
        {
            //Arrange:
            var entity = new Entity();
            int sharedId = EntityKeyStore.NameToId("shared");
            var keys = new[]
            {
                new TagKey(sharedId),
                new TagKey(sharedId)
            };

            //Act:
            entity.AddTags(keys);

            //Assert:
            Assert.AreEqual(1, entity.TagCount);
        }

        #endregion

        #region AddTags<E> (IEnumerable<TagKey<E>>)

        [Test]
        public void AddTags_GenericTagKeys_AddsAllTags()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("one"),
                new TagKey<Entity>("two"),
                new TagKey<Entity>("three")
            };

            //Act:
            entity.AddTags(keys);

            //Assert:
            Assert.IsTrue(entity.HasTag("one"));
            Assert.IsTrue(entity.HasTag("two"));
            Assert.IsTrue(entity.HasTag("three"));
            Assert.AreEqual(3, entity.TagCount);
        }

        [Test]
        public void AddTags_GenericTagKeys_IgnoresNullCollection()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            entity.AddTags((IEnumerable<TagKey<Entity>>)null);

            //Assert:
            Assert.AreEqual(0, entity.TagCount);
        }

        [Test]
        public void AddTags_GenericTagKeys_DeduplicatesEquivalentKeys()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("dup"),
                new TagKey<Entity>("dup")
            };

            //Act:
            entity.AddTags(keys);

            //Assert:
            Assert.AreEqual(1, entity.TagCount);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  DelTag extensions
        // ══════════════════════════════════════════════════════════════

        #region DelTag (string)

        [Test]
        public void DelTag_ByString_ReturnsTrue_WhenExists()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("enemy");

            //Act:
            bool result = entity.DelTag("enemy");

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasTag("enemy"));
        }

        [Test]
        public void DelTag_ByString_ReturnsFalse_WhenNotPresent()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.DelTag("nonexistent");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DelTag_ByString_RemovesOnlyTargetTag()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("alpha");
            entity.AddTag("beta");

            //Act:
            entity.DelTag("alpha");

            //Assert:
            Assert.IsFalse(entity.HasTag("alpha"));
            Assert.IsTrue(entity.HasTag("beta"));
        }

        [Test]
        public void DelTag_ByString_CanReAddAfterDeletion()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("temp");
            entity.DelTag("temp");

            //Act:
            bool result = entity.AddTag("temp");

            //Assert:
            Assert.IsTrue(result);
            Assert.IsTrue(entity.HasTag("temp"));
        }

        #endregion

        #region DelTag (TagKey)

        [Test]
        public void DelTag_ByTagKey_ReturnsTrue_WhenExists()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("poison");
            var key = new TagKey("poison");

            //Act:
            bool result = entity.DelTag(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasTag("poison"));
        }

        [Test]
        public void DelTag_ByTagKey_ReturnsFalse_WhenNotPresent()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("poison");

            //Act:
            bool result = entity.DelTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void DelTag_ByTagKey_RemovesByMatchingId()
        {
            //Arrange:
            var entity = new Entity();
            int id = EntityKeyStore.NameToId("item");
            entity.AddTag(id);
            var key = new TagKey(id);

            //Act:
            entity.DelTag(key);

            //Assert:
            Assert.IsFalse(entity.HasTag(id));
        }

        #endregion

        #region DelTag<E> (TagKey<E>)

        [Test]
        public void DelTag_GenericTagKey_ReturnsTrue_WhenExists()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("shield");
            entity.AddTag(key);

            //Act:
            bool result = entity.DelTag(key);

            //Assert:
            Assert.IsTrue(result);
            Assert.IsFalse(entity.HasTag("shield"));
        }

        [Test]
        public void DelTag_GenericTagKey_ReturnsFalse_WhenNotPresent()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("shield");

            //Act:
            bool result = entity.DelTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  HasTag extensions
        // ══════════════════════════════════════════════════════════════

        #region HasTag (string)

        [Test]
        public void HasTag_ByString_ReturnsTrue_WhenTagExists()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("target");

            //Act:
            bool result = entity.HasTag("target");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasTag_ByString_ReturnsFalse_WhenNoTags()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasTag("target");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasTag_ByString_ReturnsFalse_WhenDifferentTagAdded()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("other");

            //Act:
            bool result = entity.HasTag("target");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasTag_ByString_ReturnsFalse_AfterDeletion()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("target");
            entity.DelTag("target");

            //Act:
            bool result = entity.HasTag("target");

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        #region HasTag (TagKey)

        [Test]
        public void HasTag_ByTagKey_ReturnsTrue_WhenTagExists()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("node");
            var key = new TagKey("node");

            //Act:
            bool result = entity.HasTag(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasTag_ByTagKey_ReturnsFalse_WhenMissing()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("node");

            //Act:
            bool result = entity.HasTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasTag_ByTagKey_ReturnsFalse_AfterDeletion()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("node");
            entity.AddTag(key);
            entity.DelTag(key);

            //Act:
            bool result = entity.HasTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        #region HasTag<E> (TagKey<E>)

        [Test]
        public void HasTag_GenericTagKey_ReturnsTrue_WhenTagExists()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("buff");
            entity.AddTag(key);

            //Act:
            bool result = entity.HasTag(key);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasTag_GenericTagKey_ReturnsFalse_WhenMissing()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("buff");

            //Act:
            bool result = entity.HasTag(key);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  HasAllTags extensions
        // ══════════════════════════════════════════════════════════════

        #region HasAllTags (params int[])

        [Test]
        public void HasAllTags_ByInts_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);
            entity.AddTag(3);

            //Act:
            bool result = entity.HasAllTags(1, 2, 3);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAllTags_ByInts_ReturnsFalse_WhenOneMissing()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);

            //Act:
            bool result = entity.HasAllTags(1, 2, 99);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByInts_ReturnsFalse_WhenAllMissing()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags(10, 20, 30);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByInts_ReturnsTrue_WhenEmptyArray()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags(new int[0]);

            //Assert:
            Assert.IsTrue(result);
        }

        #endregion

        #region HasAllTags (params string[])

        [Test]
        public void HasAllTags_ByStrings_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("fire");
            entity.AddTag("ice");
            entity.AddTag("wind");

            //Act:
            bool result = entity.HasAllTags("fire", "ice", "wind");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAllTags_ByStrings_ReturnsFalse_WhenOneMissing()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("fire");
            entity.AddTag("ice");

            //Act:
            bool result = entity.HasAllTags("fire", "ice", "wind");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByStrings_ReturnsFalse_WhenAllMissing()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags("a", "b", "c");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByStrings_ReturnsTrue_WhenEmptyArray()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags(new string[0]);

            //Assert:
            Assert.IsTrue(result);
        }

        #endregion

        #region HasAllTags (params TagKey[])

        [Test]
        public void HasAllTags_ByTagKeys_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("tank");
            entity.AddTag("healer");
            var keyTank = new TagKey("tank");
            var keyHealer = new TagKey("healer");

            //Act:
            bool result = entity.HasAllTags(keyTank, keyHealer);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAllTags_ByTagKeys_ReturnsFalse_WhenOneMissing()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("tank");
            var keyTank = new TagKey("tank");
            var keyDps = new TagKey("dps");

            //Act:
            bool result = entity.HasAllTags(keyTank, keyDps);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByTagKeys_ReturnsFalse_WhenAllMissing()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey("x"),
                new TagKey("y")
            };

            //Act:
            bool result = entity.HasAllTags(keys);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_ByTagKeys_ReturnsTrue_WhenEmptyParams()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags(new TagKey[0]);

            //Assert:
            Assert.IsTrue(result);
        }

        #endregion

        #region HasAllTags<E> (params TagKey<E>[])

        [Test]
        public void HasAllTags_GenericTagKeys_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("atk"),
                new TagKey<Entity>("def")
            };
            entity.AddTag(keys[0]);
            entity.AddTag(keys[1]);

            //Act:
            bool result = entity.HasAllTags(keys);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAllTags_GenericTagKeys_ReturnsFalse_WhenOneMissing()
        {
            //Arrange:
            var entity = new Entity();
            var keyPresent = new TagKey<Entity>("atk");
            var keyMissing = new TagKey<Entity>("spd");
            entity.AddTag(keyPresent);

            //Act:
            bool result = entity.HasAllTags(keyPresent, keyMissing);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_GenericTagKeys_ReturnsFalse_WhenAllMissing()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("a"),
                new TagKey<Entity>("b")
            };

            //Act:
            bool result = entity.HasAllTags(keys);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAllTags_GenericTagKeys_ReturnsTrue_WhenEmptyParams()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAllTags(new TagKey<Entity>[0]);

            //Assert:
            Assert.IsTrue(result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  HasAnyTag extensions
        // ══════════════════════════════════════════════════════════════

        #region HasAnyTag (params int[])

        [Test]
        public void HasAnyTag_ByInts_ReturnsTrue_WhenOnePresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag(5);

            //Act:
            bool result = entity.HasAnyTag(5, 6, 7);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByInts_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag(1);
            entity.AddTag(2);

            //Act:
            bool result = entity.HasAnyTag(1, 2);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByInts_ReturnsFalse_WhenNonePresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag(1);

            //Act:
            bool result = entity.HasAnyTag(2, 3, 4);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAnyTag_ByInts_ReturnsFalse_WhenEmptyArray()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAnyTag(new int[0]);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        #region HasAnyTag (params string[])

        [Test]
        public void HasAnyTag_ByStrings_ReturnsTrue_WhenOnePresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("cold");

            //Act:
            bool result = entity.HasAnyTag("cold", "hot", "warm");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByStrings_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("x");
            entity.AddTag("y");

            //Act:
            bool result = entity.HasAnyTag("x", "y");

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByStrings_ReturnsFalse_WhenNonePresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("a");

            //Act:
            bool result = entity.HasAnyTag("b", "c");

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAnyTag_ByStrings_ReturnsFalse_WhenEmptyArray()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAnyTag(new string[0]);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        #region HasAnyTag (params TagKey[])

        [Test]
        public void HasAnyTag_ByTagKeys_ReturnsTrue_WhenOnePresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("present");
            var keys = new[]
            {
                new TagKey("present"),
                new TagKey("absent")
            };

            //Act:
            bool result = entity.HasAnyTag(keys);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByTagKeys_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("a");
            entity.AddTag("b");
            var keys = new[]
            {
                new TagKey("a"),
                new TagKey("b")
            };

            //Act:
            bool result = entity.HasAnyTag(keys);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_ByTagKeys_ReturnsFalse_WhenNonePresent()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey("missing1"),
                new TagKey("missing2")
            };

            //Act:
            bool result = entity.HasAnyTag(keys);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAnyTag_ByTagKeys_ReturnsFalse_WhenEmptyParams()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAnyTag(new TagKey[0]);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        #region HasAnyTag<E> (params TagKey<E>[])

        [Test]
        public void HasAnyTag_GenericTagKeys_ReturnsTrue_WhenOnePresent()
        {
            //Arrange:
            var entity = new Entity();
            var keyPresent = new TagKey<Entity>("found");
            var keyMissing = new TagKey<Entity>("gone");
            entity.AddTag(keyPresent);

            //Act:
            bool result = entity.HasAnyTag(keyPresent, keyMissing);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_GenericTagKeys_ReturnsTrue_WhenAllPresent()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("a"),
                new TagKey<Entity>("b")
            };
            entity.AddTag(keys[0]);
            entity.AddTag(keys[1]);

            //Act:
            bool result = entity.HasAnyTag(keys);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void HasAnyTag_GenericTagKeys_ReturnsFalse_WhenNonePresent()
        {
            //Arrange:
            var entity = new Entity();
            var keys = new[]
            {
                new TagKey<Entity>("x"),
                new TagKey<Entity>("y")
            };

            //Act:
            bool result = entity.HasAnyTag(keys);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void HasAnyTag_GenericTagKeys_ReturnsFalse_WhenEmptyParams()
        {
            //Arrange:
            var entity = new Entity();

            //Act:
            bool result = entity.HasAnyTag(new TagKey<Entity>[0]);

            //Assert:
            Assert.IsFalse(result);
        }

        #endregion

        // ══════════════════════════════════════════════════════════════
        //  Cross-extension integration
        // ══════════════════════════════════════════════════════════════

        #region Cross-Extension Integration

        [Test]
        public void AddTag_ByString_IsVisibleThrough_HasTag_ByTagKey()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("bridge");
            var key = new TagKey("bridge");

            //Act & Assert:
            Assert.IsTrue(entity.HasTag(key));
        }

        [Test]
        public void DelTag_ByTagKey_RemovesTagAdded_ByString()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("anchor");
            var key = new TagKey("anchor");

            //Act:
            entity.DelTag(key);

            //Assert:
            Assert.IsFalse(entity.HasTag("anchor"));
        }

        [Test]
        public void DelTag_ByString_RemovesTagAdded_ByTagKey()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey("link");
            entity.AddTag(key);

            //Act:
            entity.DelTag("link");

            //Assert:
            Assert.IsFalse(entity.HasTag(key));
        }

        [Test]
        public void AddTag_Generic_AddsTagsVisibleThrough_HasTag_ByString()
        {
            //Arrange:
            var entity = new Entity();
            var key = new TagKey<Entity>("glyph");
            entity.AddTag(key);

            //Act & Assert:
            Assert.IsTrue(entity.HasTag("glyph"));
        }

        [Test]
        public void HasAllTags_MixedSources_ReturnsCorrectResult()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("a");
            entity.AddTag("b");
            var keyC = new TagKey("c");

            //Act:
            bool withAllInts = entity.HasAllTags(0, 0);           // never assigned
            bool withAllStrings = entity.HasAllTags("a", "b");    // both present
            bool withKeyMissing = entity.HasAllTags(keyC);        // not present

            //Assert:
            Assert.IsFalse(withAllInts);
            Assert.IsTrue(withAllStrings);
            Assert.IsFalse(withKeyMissing);
        }

        [Test]
        public void HasAnyTag_MixedSources_ReturnsCorrectResult()
        {
            //Arrange:
            var entity = new Entity();
            entity.AddTag("hit");
            var keyPresent = new TagKey("hit");
            var keyMissing = new TagKey("miss");

            //Act & Assert:
            Assert.IsTrue(entity.HasAnyTag("hit", "nope"));
            Assert.IsTrue(entity.HasAnyTag(keyPresent, keyMissing));
        }

        #endregion
    }
}
