using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    public sealed class OrExpressionTests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var orExpression = new OrExpression();

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Value);
        }

        [Test]
        public void TrueExpression()
        {
            var orExpression = new OrExpression(
                () => true,
                () => false
            );

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Value);
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var orExpression = new OrExpression(
                () => false,
                () => false
            );

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsFalse(orExpression.Value);
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var orExpression = new OrExpression(
                () => false
            );

            //Pre-assert:
            Assert.IsFalse(orExpression.Value);

            orExpression.Add(new Const<bool>(true));

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Value);
        }

        [Test]
        public void Remove()
        {
            //Arrange:

            bool Member1() => true;
            bool Member2() => false;

            var orExpression = new OrExpression(
                Member1, Member2
            );

            //Pre-assert:
            Assert.IsTrue(orExpression.Value);
            Assert.IsTrue(orExpression.Contains(Member1));
            Assert.IsTrue(orExpression.Contains(Member2));

            //Act:
            orExpression.Remove(Member1);

            //Assert:
            Assert.AreEqual(1, orExpression.Count);
            Assert.IsFalse(orExpression.Value);
            Assert.IsFalse(orExpression.Contains(Member1));
        }

        [Test]
        public void Clear()
        {
            //Arrange:

            bool Member1() => true;
            bool Member2() => false;

            var orExpression = new OrExpression(
                Member1, Member2
            );

            //Pre-assert:
            Assert.IsTrue(orExpression.Value);
            Assert.IsTrue(orExpression.Contains(Member1));
            Assert.IsTrue(orExpression.Contains(Member2));

            //Act:
            orExpression.Clear();

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Value);

            Assert.IsFalse(orExpression.Contains(Member1));
            Assert.IsFalse(orExpression.Contains(Member2));
        }
    }

    [TestFixture]
    public sealed class OrExpressionT1Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var orExpression = new OrExpression<string>();

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya"));
        }

        [Test]
        public void TrueExpression()
        {
            var orExpression = new OrExpression<string>(
                s => s == "Vasya",
                _ => false
            );

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Invoke("Vasya"));
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var orExpression = new OrExpression<string>(
                _ => false,
                s => s != "Vasya",
                s => s == "Petya"
            );

            //Assert:
            Assert.AreEqual(3, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya"));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var orExpression = new OrExpression<string>(
                _ => false
            );

            //Act:
            orExpression.Add(s => s == "Vasya");

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Invoke("Vasya"));
        }

        [Test]
        public void Remove()
        {
            //Arrange:

            bool Member1(string s) => s == "Vasya";
            bool Member2(string s) => s == "Petya";

            var orExpression = new OrExpression<string>(
                Member1, Member2
            );

            //Pre-assert:
            Assert.IsFalse(orExpression.Invoke("Ivan"));
            Assert.IsTrue(orExpression.Contains(Member1));
            Assert.IsTrue(orExpression.Contains(Member2));

            //Act:
            orExpression.Remove(Member1);
            orExpression.Remove(Member2);

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya"));
            Assert.IsFalse(orExpression.Contains(Member1));
            Assert.IsFalse(orExpression.Contains(Member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            bool Member1(string s) => s == "Vasya";
            bool Member2(string s) => s == "Petya";

            var orExpression = new OrExpression<string>(
                Member1, Member2
            );

            //Pre-assert:
            Assert.IsTrue(orExpression.Invoke("Vasya"));
            Assert.IsTrue(orExpression.Contains(Member1));
            Assert.IsTrue(orExpression.Contains(Member2));

            //Act:
            orExpression.Clear();

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya"));
            Assert.IsFalse(orExpression.Contains(Member1));
            Assert.IsFalse(orExpression.Contains(Member2));
        }
    }

    [TestFixture]
    public sealed class OrExpressionT2Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var orExpression = new OrExpression<string, int>();

            //Assert:
            Assert.AreEqual(0, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void TrueExpression()
        {
            var orExpression = new OrExpression<string, int>(
                (s, i) => s == "Vasya",
                (s, i) => false
            );

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void FalseExpression()
        {
            //Arrange:
            var orExpression = new OrExpression<string, int>(
                (s, i) => false,
                (s, i) => s != "Vasya",
                (s, i) => s == "Petya"
            );

            //Assert:
            Assert.AreEqual(3, orExpression.Count);
            Assert.IsFalse(orExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var orExpression = new OrExpression<string, int>(
                (s, i) => false
            );

            Assert.IsFalse(orExpression.Invoke("Vawsya", 10));

            //Act:
            orExpression.Add((s, i) => s == "Vasya");

            //Assert:
            Assert.AreEqual(2, orExpression.Count);
            Assert.IsTrue(orExpression.Invoke("Vasya", 10));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, int, bool> member1 = (s, i) => s == "Vasya";
            Func<string, int, bool> member2 = (s, i) => s == "Petya";

            var orExpression = new OrExpression<string, int>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(orExpression.Invoke("Vasya", 10));
            Assert.IsTrue(orExpression.Contains(member1));
            Assert.IsTrue(orExpression.Contains(member2));

            //Act:
            orExpression.Remove(member1);

            //Assert:
            Assert.IsFalse(orExpression.Invoke("Vasya", 10));
            Assert.IsFalse(orExpression.Contains(member1));
        }
        
        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, int, bool> member1 = (s, i) => s == "Vasya";
            Func<string, int, bool> member2 = (s, i) => s == "Petya";

            var orExpression = new OrExpression<string, int>(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(orExpression.Invoke("Vasya", 10));
            Assert.IsTrue(orExpression.Contains(member1));
            Assert.IsTrue(orExpression.Contains(member2));

            //Act:
            orExpression.Clear();

            //Assert:
            Assert.IsFalse(orExpression.Invoke("Vasya", 10));
            Assert.IsFalse(orExpression.Contains(member1));
            Assert.IsFalse(orExpression.Contains(member2));
        }
    }
}