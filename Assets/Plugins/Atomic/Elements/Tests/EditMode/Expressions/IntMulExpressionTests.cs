using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class IntMulExpressionTests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntMulExpression();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Value);
        }

        [Test]
        public void Multiplication()
        {
            var exp = new IntMulExpression(
                () => -2,
                () => 3,
                () => 5
            );

            //Assert:
            Assert.AreEqual(3, exp.Count);
            Assert.AreEqual(-30, exp.Value);
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntMulExpression(
                () => 2,
                () => 3
            );

            //Pre-assert:
            Assert.AreEqual(6, exp.Value);
            Assert.AreEqual(2, exp.Count);

            exp.Add(new Const<int>(-3));

            //Assert:
            Assert.AreEqual(3, exp.Count);
            Assert.AreEqual(-18, exp.Value);
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<int> member1 = () => 10;
            Func<int> member2 = () => 20;

            var exp = new IntMulExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(200, exp.Value);
            Assert.AreEqual(2, exp.Count);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member2);

            //Assert:
            Assert.AreEqual(10, exp.Value);
            Assert.AreEqual(1, exp.Count);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<int> member1 = () => -1;
            Func<int> member2 = () => 8;

            var exp = new IntMulExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Value);
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class IntMulExpressionT1Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntMulExpression<string>();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Invoke("Vasya"));
        }

        [Test]
        public void Multiplication()
        {
            var exp = new IntMulExpression<string>(
                s => -2,
                s => s == "Vasya" ? 5 : 0
            );

            //Assert:
            Assert.AreEqual(2, exp.Count);
            Assert.AreEqual(-10, exp.Invoke("Vasya"));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntMulExpression<string>(
                s => 2,
                s => 3
            );

            //Pre-assert:
            Assert.AreEqual(6, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.Count);

            exp.Add(s => s == "Vasya" ? 5 : 0);

            //Assert:
            Assert.AreEqual(3, exp.Count);
            Assert.AreEqual(30, exp.Invoke("Vasya"));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, int> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, int> member2 = s => 20;

            var exp = new IntMulExpression<string>(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(100, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.Count);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member2);

            //Assert:
            Assert.AreEqual(3, exp.Invoke("Petya"));
            Assert.AreEqual(1, exp.Count);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, int> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, int> member2 = s => 20;

            var exp = new IntMulExpression<string>(
                member1, member2
            );

            Assert.AreEqual(100, exp.Invoke("Vasya"));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Invoke("Vasya"));
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }
    
     [TestFixture]
    public sealed class IntMulExpressionT2Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntMulExpression<string, int>();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Invoke("Vasya", 5));
        }

        [Test]
        public void Multiplication()
        {
            var exp = new IntMulExpression<string, int>(
                (s, i) => -2 * i,
                (s, i) => s == "Vasya" ? 5 : 0
            );

            //Assert:
            Assert.AreEqual(2, exp.Count);
            Assert.AreEqual(-100, exp.Invoke("Vasya", 10));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntMulExpression<string, int>(
                (s, i) => i,
                (s, i) => s == "Vasya" ? 5 : 0
            );

            //Pre-assert:
            Assert.AreEqual(10, exp.Invoke("Vasya", 2));
            Assert.AreEqual(2, exp.Count);

            exp.Add((s, i) => s == "Vasya" ? 0 : 1);

            //Assert:
            Assert.AreEqual(3, exp.Count);
            Assert.AreEqual(0, exp.Invoke("Vasya", 1000));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, int, int> member1 = (s, i) => s == "Vasya" ? i : 1;
            Func<string, int, int> member2 = (s, i) => 20;

            var exp = new IntMulExpression<string, int>(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(200, exp.Invoke("Vasya", 10));
            Assert.AreEqual(2, exp.Count);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member1);

            //Assert:
            Assert.AreEqual(20, exp.Invoke("Vasya", 3));
            Assert.AreEqual(1, exp.Count);

            Assert.IsFalse(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, int, int> member1 = (s, i) => s == "Vasya" ? i : 3;
            Func<string, int, int> member2 = (s, i) => 20;

            var exp = new IntMulExpression<string, int>(
                member1, member2
            );

            Assert.AreEqual(200, exp.Invoke("Vasya", 10));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.Count);
            Assert.AreEqual(1, exp.Invoke("Vasya", 10));
            
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }
}