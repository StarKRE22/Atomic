using System;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class TagKeyTests
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

        // ── Constructor(string) ──────────────────────────────────

        [Test]
        public void Constructor_FromName_IdMatchesEntityKeyStoreNameToId()
        {
            //Arrange:
            const string name = "Player";

            //Act:
            var key = new TagKey(name);
            int expected = EntityKeyStore.NameToId(name);

            //Assert:
            Assert.AreEqual(expected, key.Id);
        }

        [Test]
        public void Constructor_FromName_NullName_ThrowsArgumentNullException()
        {
            //Arrange:
            //Act:
            //Assert:
            Assert.Throws<ArgumentNullException>(() => new TagKey((string) null));
        }

        [Test]
        public void Constructor_FromName_DifferentNamesGetDifferentIds()
        {
            //Arrange:
            //Act:
            var keyA = new TagKey("Health");
            var keyB = new TagKey("Mana");

            //Assert:
            Assert.AreNotEqual(keyA.Id, keyB.Id);
        }

        [Test]
        public void Constructor_FromName_CreatedBeforeRegistration_SameId()
        {
            //Arrange:
            // Pre-register in EntityKeyStore
            int storeId = EntityKeyStore.NameToId("Enemy");

            //Act:
            var key = new TagKey("Enemy");

            //Assert:
            Assert.AreEqual(storeId, key.Id);
        }

        // ── Constructor(int) ─────────────────────────────────────

        [Test]
        public void Constructor_FromId_SetsIdDirectly()
        {
            //Arrange:
            const int expectedId = 42;

            //Act:
            var key = new TagKey(expectedId);

            //Assert:
            Assert.AreEqual(expectedId, key.Id);
        }

        [Test]
        public void Constructor_FromId_ZeroId_SetsIdToZero()
        {
            //Arrange:
            //Act:
            var key = new TagKey(0);

            //Assert:
            Assert.AreEqual(0, key.Id);
        }

        [Test]
        public void Constructor_FromId_NegativeId_SetsIdToNegative()
        {
            //Arrange:
            //Act:
            var key = new TagKey(-5);

            //Assert:
            Assert.AreEqual(-5, key.Id);
        }

        // ── Equals ───────────────────────────────────────────────

        [Test]
        public void Equals_SameId_ReturnsTrue()
        {
            //Arrange:
            var keyA = new TagKey(7);
            var keyB = new TagKey(7);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_DifferentId_ReturnsFalse()
        {
            //Arrange:
            var keyA = new TagKey(1);
            var keyB = new TagKey(2);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_SameName_ReturnsTrue()
        {
            //Arrange:
            var keyA = new TagKey("Fire");
            var keyB = new TagKey("Fire");

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_Object_SameId_ReturnsTrue()
        {
            //Arrange:
            var keyA = new TagKey(10);
            object keyB = new TagKey(10);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_Object_Null_ReturnsFalse()
        {
            //Arrange:
            var key = new TagKey(1);

            //Act:
            bool result = key.Equals(null);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_Object_DifferentType_ReturnsFalse()
        {
            //Arrange:
            var key = new TagKey(1);

            //Act:
            bool result = key.Equals("not a tag key");

            //Assert:
            Assert.IsFalse(result);
        }

        // ── GetHashCode ──────────────────────────────────────────

        [Test]
        public void GetHashCode_SameId_ReturnsSameHash()
        {
            //Arrange:
            var keyA = new TagKey(5);
            var keyB = new TagKey(5);

            //Act:
            int hashA = keyA.GetHashCode();
            int hashB = keyB.GetHashCode();

            //Assert:
            Assert.AreEqual(hashA, hashB);
        }

        [Test]
        public void GetHashCode_ReturnsId()
        {
            //Arrange:
            const int id = 99;
            var key = new TagKey(id);

            //Act:
            int hash = key.GetHashCode();

            //Assert:
            Assert.AreEqual(id, hash);
        }

        // ── ToString ─────────────────────────────────────────────

        [Test]
        public void ToString_FromName_ReturnsNameFromEntityKeyStore()
        {
            //Arrange:
            const string name = "Speed";
            var key = new TagKey(name);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual(name, result);
        }

        [Test]
        public void ToString_FromId_RegisteredId_ReturnsName()
        {
            //Arrange:
            const string name = "Strength";
            int id = EntityKeyStore.NameToId(name);
            var key = new TagKey(id);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual(name, result);
        }

        [Test]
        public void ToString_FromId_UnknownId_ReturnsUnknownPlaceholder()
        {
            //Arrange:
            var key = new TagKey(9999);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual("#Unknown:9999", result);
        }

        // ── Operator / Interop ───────────────────────────────────

        [Test]
        public void TagKey_TwoKeysSameName_AreEqual()
        {
            //Arrange:
            //Act:
            var keyA = new TagKey("Gold");
            var keyB = new TagKey("Gold");

            //Assert:
            Assert.IsTrue(keyA.Equals(keyB));
            Assert.AreEqual(keyA.GetHashCode(), keyB.GetHashCode());
        }
    }
}
