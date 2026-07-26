using System;
using NUnit.Framework;

namespace Atomic.Entities
{
    [TestFixture]
    public sealed class ValueKeyTests
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
            const string name = "MaxHealth";

            //Act:
            var key = new ValueKey<int>(name);
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
            Assert.Throws<ArgumentNullException>(() => new ValueKey<int>((string) null));
        }

        [Test]
        public void Constructor_FromName_DifferentNamesGetDifferentIds()
        {
            //Arrange:
            //Act:
            var keyA = new ValueKey<int>("Health");
            var keyB = new ValueKey<int>("Mana");

            //Assert:
            Assert.AreNotEqual(keyA.Id, keyB.Id);
        }

        [Test]
        public void Constructor_FromName_DifferentGenericTypes_SameNameShareId()
        {
            //Arrange:
            const string name = "Shared";

            //Act:
            var intKey = new ValueKey<int>(name);
            var stringKey = new ValueKey<string>(name);

            //Assert:
            Assert.AreEqual(intKey.Id, stringKey.Id);
        }

        [Test]
        public void Constructor_FromName_CreatedBeforeRegistration_SameId()
        {
            //Arrange:
            int storeId = EntityKeyStore.NameToId("Experience");

            //Act:
            var key = new ValueKey<int>("Experience");

            //Assert:
            Assert.AreEqual(storeId, key.Id);
        }

        // ── Constructor(int) ─────────────────────────────────────

        [Test]
        public void Constructor_FromId_SetsIdDirectly()
        {
            //Arrange:
            const int expectedId = 55;

            //Act:
            var key = new ValueKey<int>(expectedId);

            //Assert:
            Assert.AreEqual(expectedId, key.Id);
        }

        [Test]
        public void Constructor_FromId_ZeroId_SetsIdToZero()
        {
            //Arrange:
            //Act:
            var key = new ValueKey<string>(0);

            //Assert:
            Assert.AreEqual(0, key.Id);
        }

        [Test]
        public void Constructor_FromId_NegativeId_SetsIdToNegative()
        {
            //Arrange:
            //Act:
            var key = new ValueKey<float>(-3);

            //Assert:
            Assert.AreEqual(-3, key.Id);
        }

        [Test]
        public void Constructor_FromId_DifferentGenericTypes_SameIdIndependent()
        {
            //Arrange:
            const int id = 10;

            //Act:
            var intKey = new ValueKey<int>(id);
            var floatKey = new ValueKey<float>(id);

            //Assert:
            Assert.AreEqual(intKey.Id, floatKey.Id);
        }

        // ── Equals ───────────────────────────────────────────────

        [Test]
        public void Equals_SameId_ReturnsTrue()
        {
            //Arrange:
            var keyA = new ValueKey<int>(7);
            var keyB = new ValueKey<int>(7);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_DifferentId_ReturnsFalse()
        {
            //Arrange:
            var keyA = new ValueKey<int>(1);
            var keyB = new ValueKey<int>(2);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_SameName_ReturnsTrue()
        {
            //Arrange:
            var keyA = new ValueKey<string>("Config");
            var keyB = new ValueKey<string>("Config");

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_DifferentGenericType_SameId_ReturnsFalse()
        {
            //Arrange:
            var intKey = new ValueKey<int>(5);
            var floatKey = new ValueKey<float>(5);

            //Act:
            bool result = intKey.Equals(floatKey);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_Object_SameId_ReturnsTrue()
        {
            //Arrange:
            var keyA = new ValueKey<int>(10);
            object keyB = new ValueKey<int>(10);

            //Act:
            bool result = keyA.Equals(keyB);

            //Assert:
            Assert.IsTrue(result);
        }

        [Test]
        public void Equals_Object_Null_ReturnsFalse()
        {
            //Arrange:
            var key = new ValueKey<int>(1);

            //Act:
            bool result = key.Equals(null);

            //Assert:
            Assert.IsFalse(result);
        }

        [Test]
        public void Equals_Object_DifferentType_ReturnsFalse()
        {
            //Arrange:
            var key = new ValueKey<int>(1);

            //Act:
            bool result = key.Equals("not a value key");

            //Assert:
            Assert.IsFalse(result);
        }

        // ── GetHashCode ──────────────────────────────────────────

        [Test]
        public void GetHashCode_SameId_ReturnsSameHash()
        {
            //Arrange:
            var keyA = new ValueKey<int>(5);
            var keyB = new ValueKey<int>(5);

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
            const int id = 77;
            var key = new ValueKey<int>(id);

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
            const string name = "SpeedMultiplier";
            var key = new ValueKey<float>(name);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual(name, result);
        }

        [Test]
        public void ToString_FromId_RegisteredId_ReturnsName()
        {
            //Arrange:
            const string name = "DisplayName";
            int id = EntityKeyStore.NameToId(name);
            var key = new ValueKey<string>(id);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual(name, result);
        }

        [Test]
        public void ToString_FromId_UnknownId_ReturnsUnknownPlaceholder()
        {
            //Arrange:
            var key = new ValueKey<int>(9999);

            //Act:
            string result = key.ToString();

            //Assert:
            Assert.AreEqual("#Unknown:9999", result);
        }

        // ── Interop / Consistency ────────────────────────────────

        [Test]
        public void TwoKeysSameName_AreEqual()
        {
            //Arrange:
            //Act:
            var keyA = new ValueKey<int>("Gold");
            var keyB = new ValueKey<int>("Gold");

            //Assert:
            Assert.IsTrue(keyA.Equals(keyB));
            Assert.AreEqual(keyA.GetHashCode(), keyB.GetHashCode());
        }

        [Test]
        public void ValueKey_DifferentGenericTypes_SameName_DifferentEquals()
        {
            //Arrange:
            const string name = "Data";

            //Act:
            var intKey = new ValueKey<int>(name);
            var stringKey = new ValueKey<string>(name);

            //Assert:
            Assert.AreEqual(intKey.Id, stringKey.Id);
            Assert.IsFalse(intKey.Equals(stringKey));
        }
    }
}
