using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class FloatMulExpressionTests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new FloatMulExpression();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Value);
        }

        [Test]
        public void Multiplication()
        {
            var exp = new FloatMulExpression(
                () => -2,
                () => 3.5f,
                () => 5
            );

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(-35, exp.Value);
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new FloatMulExpression(
                () => 2,
                () => 3
            );

            //Pre-assert:
            Assert.AreEqual(6, exp.Value);
            Assert.AreEqual(2, exp.MemberCount);

            exp.Append(new Const<float>(-3));

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(-18, exp.Value);
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<float> member1 = () => 10;
            Func<float> member2 = () => 20;

            var exp = new FloatMulExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(200, exp.Value);
            Assert.AreEqual(2, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member2);

            //Assert:
            Assert.AreEqual(10, exp.Value);
            Assert.AreEqual(1, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<float> member1 = () => -1;
            Func<float> member2 = () => 8;

            var exp = new FloatMulExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Value);
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class FloatMulExpressionT1Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new FloatMulExpression<string>();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Invoke("Vasya"));
        }

        [Test]
        public void Multiplication()
        {
            var exp = new FloatMulExpression<string>(
                s => -2,
                s => s == "Vasya" ? 5 : 0
            );

            //Assert:
            Assert.AreEqual(2, exp.MemberCount);
            Assert.AreEqual(-10, exp.Invoke("Vasya"));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new FloatMulExpression<string>(
                s => 2,
                s => 3
            );

            //Pre-assert:
            Assert.AreEqual(6, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.MemberCount);

            exp.Append(s => s == "Vasya" ? 5 : 0);

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(30, exp.Invoke("Vasya"));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, float> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, float> member2 = s => 20;

            var exp = new FloatMulExpression<string>(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(100, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member2);

            //Assert:
            Assert.AreEqual(3, exp.Invoke("Petya"));
            Assert.AreEqual(1, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, float> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, float> member2 = s => 20;

            var exp = new FloatMulExpression<string>(
                member1, member2
            );

            Assert.AreEqual(100, exp.Invoke("Vasya"));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Invoke("Vasya"));
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }
    
    [TestFixture]
    public sealed class FloatMulExpressionT2Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new FloatMulExpression<string, int>();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Invoke("Vasya", 5));
        }

        [Test]
        public void Multiplication()
        {
            var exp = new FloatMulExpression<string, int>(
                (s, i) => -2 * i,
                (s, i) => s == "Vasya" ? 5 : 0
            );

            //Assert:
            Assert.AreEqual(2, exp.MemberCount);
            Assert.AreEqual(-100, exp.Invoke("Vasya", 10));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new FloatMulExpression<string, float>(
                (s, i) => i,
                (s, i) => s == "Vasya" ? 5 : 0
            );

            //Pre-assert:
            Assert.AreEqual(2.5f, exp.Invoke("Vasya", 0.5f));
            Assert.AreEqual(2, exp.MemberCount);

            exp.Append((s, i) => s == "Vasya" ? 0 : 1);

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(0, exp.Invoke("Vasya", 1000));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, float, float> member1 = (s, i) => s == "Vasya" ? i : 1;
            Func<string, float, float> member2 = (s, i) => 20;

            var exp = new FloatMulExpression<string, float>(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(200, exp.Invoke("Vasya", 10));
            Assert.AreEqual(2, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member1);

            //Assert:
            Assert.AreEqual(20, exp.Invoke("Vasya", 3));
            Assert.AreEqual(1, exp.MemberCount);

            Assert.IsFalse(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, float, float> member1 = (s, i) => s == "Vasya" ? i : 3;
            Func<string, float, float> member2 = (s, i) => 20;

            var exp = new FloatMulExpression<string, float>(
                member1, member2
            );

            Assert.AreEqual(200, exp.Invoke("Vasya", 10));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(1, exp.Invoke("Vasya", 10));
            
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }
}