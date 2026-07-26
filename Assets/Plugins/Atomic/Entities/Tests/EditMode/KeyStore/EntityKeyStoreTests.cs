using System;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class EntityKeyStoreTests
    {
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

        // ── NameToId ─────────────────────────────────────────────

        [Test]
        public void NameToId_SameName_ReturnsConsistentId()
        {
            //Arrange:
            const string name = "Health";

            //Act:
            int first = EntityKeyStore.NameToId(name);
            int second = EntityKeyStore.NameToId(name);

            //Assert:
            Assert.AreEqual(first, second);
        }

        [Test]
        public void NameToId_DifferentNames_ReturnsDifferentIds()
        {
            //Arrange:
            //Act:
            int idA = EntityKeyStore.NameToId("Health");
            int idB = EntityKeyStore.NameToId("Mana");

            //Assert:
            Assert.AreNotEqual(idA, idB);
        }

        [Test]
        public void NameToId_NullName_ThrowsArgumentNullException()
        {
            //Arrange:
            //Act:
            //Assert:
            Assert.Throws<ArgumentNullException>(() => EntityKeyStore.NameToId(null));
        }

        [Test]
        public void NameToId_FirstName_ReturnsIdOne()
        {
            //Arrange:
            //Act:
            int id = EntityKeyStore.NameToId("First");

            //Assert:
            Assert.AreEqual(1, id);
        }

        [Test]
        public void NameToId_SequentialNames_ReturnsSequentialIds()
        {
            //Arrange:
            //Act:
            int idA = EntityKeyStore.NameToId("A");
            int idB = EntityKeyStore.NameToId("B");
            int idC = EntityKeyStore.NameToId("C");

            //Assert:
            Assert.AreEqual(1, idA);
            Assert.AreEqual(2, idB);
            Assert.AreEqual(3, idC);
        }

        // ── IdToName ─────────────────────────────────────────────

        [Test]
        public void IdToName_RegisteredId_ReturnsCorrectName()
        {
            //Arrange:
            const string name = "Damage";
            int id = EntityKeyStore.NameToId(name);

            //Act:
            string result = EntityKeyStore.IdToName(id);

            //Assert:
            Assert.AreEqual(name, result);
        }

        [Test]
        public void IdToName_UnknownId_ReturnsUnknownPlaceholder()
        {
            //Arrange:
            const int unknownId = 9999;

            //Act:
            string result = EntityKeyStore.IdToName(unknownId);

            //Assert:
            Assert.AreEqual("#Unknown:9999", result);
        }

        [Test]
        public void IdToName_AfterReset_UnknownIdReturnsPlaceholder()
        {
            //Arrange:
            int id = EntityKeyStore.NameToId("Temp");

            //Act:
            EntityKeyStore.Reset();
            string result = EntityKeyStore.IdToName(id);

            //Assert:
            Assert.AreEqual($"#Unknown:{id}", result);
        }

        // ── Reset ────────────────────────────────────────────────

        [Test]
        public void Reset_ClearsAllMappings()
        {
            //Arrange:
            int idBefore = EntityKeyStore.NameToId("Key");

            //Act:
            EntityKeyStore.Reset();
            string nameAfterReset = EntityKeyStore.IdToName(idBefore);

            //Assert:
            Assert.AreEqual($"#Unknown:{idBefore}", nameAfterReset);
        }

        [Test]
        public void Reset_NameToIdStartsFresh()
        {
            //Arrange:
            int idBeforeReset = EntityKeyStore.NameToId("Same");

            //Act:
            EntityKeyStore.Reset();
            int idAfterReset = EntityKeyStore.NameToId("Same");

            //Assert: Reset clears caches, so "Same" gets a fresh ID starting from 1
            Assert.AreEqual(1, idAfterReset);
            Assert.AreEqual(idBeforeReset, idAfterReset);
        }

        [Test]
        public void Reset_DoesNotThrow()
        {
            //Arrange:
            //Act:
            //Assert:
            Assert.DoesNotThrow(() => EntityKeyStore.Reset());
        }

        // ── SetAlgorithm ─────────────────────────────────────────

        [Test]
        public void SetAlgorithm_NullAlgorithm_ThrowsArgumentNullException()
        {
            //Arrange:
            //Act:
            //Assert:
            Assert.Throws<ArgumentNullException>(() => EntityKeyStore.SetAlgorithm(null));
        }

        [Test]
        public void SetAlgorithm_ReplacingAlgorithm_ResetsMappings()
        {
            //Arrange:
            int idBefore = EntityKeyStore.NameToId("Old");

            //Act:
            EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm(100));
            int idAfter = EntityKeyStore.NameToId("New");

            //Assert:
            Assert.AreEqual(100, idAfter);
            Assert.AreEqual($"#Unknown:{idBefore}", EntityKeyStore.IdToName(idBefore));
        }

        [Test]
        public void SetAlgorithm_NewAlgorithm_UsesNewAlgorithmForIdGeneration()
        {
            //Arrange:
            //Act:
            EntityKeyStore.SetAlgorithm(new SequentialEntityKeyAlgorithm(42));
            int id = EntityKeyStore.NameToId("Anything");

            //Assert:
            Assert.AreEqual(42, id);
        }

        // ── Caching ──────────────────────────────────────────────

        [Test]
        public void Caching_SecondCallToNameToId_ReturnsSameId()
        {
            //Arrange:
            const string name = "CachedKey";

            //Act:
            int first = EntityKeyStore.NameToId(name);
            int second = EntityKeyStore.NameToId(name);
            int third = EntityKeyStore.NameToId(name);

            //Assert:
            Assert.AreEqual(first, second);
            Assert.AreEqual(second, third);
        }

        [Test]
        public void Caching_MultipleDifferentNames_AllReturnUniqueIds()
        {
            //Arrange:
            //Act:
            int id1 = EntityKeyStore.NameToId("X");
            int id2 = EntityKeyStore.NameToId("Y");
            int id3 = EntityKeyStore.NameToId("Z");

            //Assert:
            Assert.AreEqual(1, id1);
            Assert.AreEqual(2, id2);
            Assert.AreEqual(3, id3);
        }
    }
}
