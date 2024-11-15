using System;
using NUnit.Framework;

// ReSharper disable ConvertToLocalFunction

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class AndExpressionTests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var andExpression = new AndExpression();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Value);
        }

        [Test]
        public void TrueExpression()
        {
            var andExpression = new AndExpression(
                () => true,
                () => true,
                () => true
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Value);
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var andExpression = new AndExpression(
                () => true,
                () => false,
                () => true
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Value);
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var andExpression = new AndExpression(
                () => true,
                () => true
            );

            //Pre-assert:
            Assert.IsTrue(andExpression.Value);
            Assert.AreEqual(2, andExpression.MemberCount);

            andExpression.Append(new Const<bool>(false));

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Value);
        }

        [Test]
        public void Remove()
        {
            //Arrange:

            Func<bool> member1 = () => true;
            Func<bool> member2 = () => false;

            var andExpression = new AndExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.IsFalse(andExpression.Value);
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));
            Assert.AreEqual(2, andExpression.MemberCount);


            //Act:
            andExpression.Remove(member2);

            //Assert:
            Assert.IsTrue(andExpression.Value);
            Assert.AreEqual(1, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<bool> member1 = () => true;
            Func<bool> member2 = () => false;

            var andExpression = new AndExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));

            //Act:
            andExpression.Clear();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Value);
            Assert.IsFalse(andExpression.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class AndExpressionT1Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var andExpression = new AndExpression<string>();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya"));
        }

        [Test]
        public void TrueExpression()
        {
            var andExpression = new AndExpression<string>(
                s => s != "Petya",
                s => s == "Vasya",
                _ => true
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya"));
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var andExpression = new AndExpression<string>(
                _ => true,
                s => s == "Vasya",
                s => s == "Petya"
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Invoke("Vasya"));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var andExpression = new AndExpression<string>(
                _ => true
            );

            //Act:
            andExpression.Append(s => s == "Petya");

            //Assert:
            Assert.AreEqual(2, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Invoke("Vasya"));
        }

        [Test]
        public void Remove()
        {
            //Arrange:

            Func<string, bool> member1 = s => s == "Vasya";
            Func<string, bool> member2 = s => s == "Petya";

            var andExpression = new AndExpression<string>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsFalse(andExpression.Invoke("Vasya"));
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));

            //Act:
            andExpression.Remove(member2);

            //Assert:
            Assert.AreEqual(1, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya"));
            Assert.IsFalse(andExpression.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, bool> member1 = s => s == "Vasya";
            Func<string, bool> member2 = s => s == "Petya";

            var andExpression = new AndExpression<string>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));

            //Act:
            andExpression.Clear();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke(null));
            Assert.IsFalse(andExpression.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class AndExpressionT2Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var andExpression = new AndExpression<string, int>();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void TrueExpression()
        {
            var andExpression = new AndExpression<string, int>(
                (s, i) => s != "Petya",
                (s, i) => s == "Vasya",
                (s, i) => true
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var andExpression = new AndExpression<string, int>(
                (s, i) => true,
                (s, i) => s == "Vasya",
                (s, i) => s == "Petya"
            );

            //Assert:
            Assert.AreEqual(3, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var andExpression = new AndExpression<string, int>(
                (s, i) => true
            );

            //Act:
            andExpression.Append((s, i) => s == "Petya");

            //Assert:
            Assert.AreEqual(2, andExpression.MemberCount);
            Assert.IsFalse(andExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, int, bool> member1 = (s, i) => s == "Vasya";
            Func<string, int, bool> member2 = (s, i) => s == "Petya";

            var andExpression = new AndExpression<string, int>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsFalse(andExpression.Invoke("Vasya", 10));
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));

            //Act:
            andExpression.Remove(member2);

            //Assert:
            Assert.AreEqual(1, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya", 10));
            Assert.IsFalse(andExpression.Contains(member2));
        }
        
        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, int, bool> member1 = (s, i) => s == "Vasya";
            Func<string, int, bool> member2 = (s, i) => s == "Petya";

            var andExpression = new AndExpression<string, int>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsFalse(andExpression.Invoke("Vasya", 10));
            Assert.IsTrue(andExpression.Contains(member1));
            Assert.IsTrue(andExpression.Contains(member2));

            //Act:
            andExpression.Clear();

            //Assert:
            Assert.AreEqual(0, andExpression.MemberCount);
            Assert.IsTrue(andExpression.Invoke("Vasya", 10));

            Assert.IsFalse(andExpression.Contains(member1));
            Assert.IsFalse(andExpression.Contains(member2));
        }
    }
}