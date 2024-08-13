using System;
using NUnit.Framework;

namespace Atomic.Elements
{
    
    [TestFixture]
    public sealed class IntSumExpressionTests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntSumExpression();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Value);
        }

        [Test]
        public void Sum()
        {
            var exp = new IntSumExpression(
                () => -2,
                () => 3,
                () => 5
            );

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(6, exp.Value);
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntSumExpression(
                () => 2,
                () => 3
            );

            //Pre-assert:
            Assert.AreEqual(5, exp.Value);
            Assert.AreEqual(2, exp.MemberCount);

            exp.Append(new Const<int>(-1));

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(4, exp.Value);
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<int> member1 = () => 16;
            Func<int> member2 = () => 26;
            Func<int> member3 = () => 60;

            var exp = new IntSumExpression(
                member1, member2, member3
            );

            //Pre-assert:
            Assert.AreEqual(102, exp.Value);
            Assert.AreEqual(3, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
            Assert.IsTrue(exp.Contains(member3));

            //Act:
            exp.Remove(member2);

            //Assert:
            Assert.AreEqual(76, exp.Value);
            Assert.AreEqual(2, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
            Assert.IsTrue(exp.Contains(member3));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<int> member1 = () => -1;
            Func<int> member2 = () => 8;

            var exp = new IntSumExpression(
                member1, member2
            );

            //Pre-assert:
            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
            Assert.AreEqual(7, exp.Value);

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Value);
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class IntSumExpressionT1Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntSumExpression<string>();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Invoke("Vasya"));
        }

        [Test]
        public void Sum()
        {
            var exp = new IntSumExpression<string>(
                s => -2,
                s => s == "Vasya" ? 5 : 0
            );

            //Assert:
            Assert.AreEqual(2, exp.MemberCount);
            Assert.AreEqual(3, exp.Invoke("Vasya"));
        }

        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntSumExpression<string>(
                s => 2,
                s => 3
            );

            //Pre-assert:
            Assert.AreEqual(5, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.MemberCount);

            exp.Append(s => s == "Vasya" ? 5 : 0);

            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(10, exp.Invoke("Vasya"));
        }

        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string, int> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, int> member2 = s => 20;

            var exp = new IntSumExpression<string>(
                member1, member2
            );

            //Pre-assert:
            Assert.AreEqual(25, exp.Invoke("Vasya"));
            Assert.AreEqual(2, exp.MemberCount);

            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));

            //Act:
            exp.Remove(member1);

            //Assert:
            Assert.AreEqual(20, exp.Invoke("Vasya"));
            Assert.AreEqual(1, exp.MemberCount);

            Assert.IsFalse(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
        }

        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string, int> member1 = s => s == "Vasya" ? 5 : 3;
            Func<string, int> member2 = s => 20;

            var exp = new IntSumExpression<string>(
                member1, member2
            );

            Assert.AreEqual(25, exp.Invoke("Vasya"));

            //Act:
            exp.Clear();

            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Invoke("Vasya"));
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
        }
    }

    [TestFixture]
    public sealed class IntSumExpressionT2Tests
    {
        [Test]
        public void EmptyExpression()
        {
            //Arrange:
            var exp = new IntSumExpression<string, int>();
    
            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Invoke("Vasya", 5));
        }
    
        [Test]
        public void Sum()
        {
            var exp = new IntSumExpression<string, int>(
                (s, i) => -2,
                (s, i) => i,
                (s, i) => s == "Vasya" ? 5 : 0
            );
    
            //Assert:
            Assert.AreEqual(3, exp.MemberCount);
            Assert.AreEqual(13, exp.Invoke("Vasya", 10));
        }
    
        [Test]
        public void Append()
        {
            //Arrange:
            var exp = new IntSumExpression<string, int>(
                (s, i) => 2
            );
    
            //Pre-assert:
            Assert.AreEqual(2, exp.Invoke("Vasya", 10));
            Assert.AreEqual(1, exp.MemberCount);
    
            exp.Append((s, i) => s == "Vasya" ? i : 0);
    
            //Assert:
            Assert.AreEqual(2, exp.MemberCount);
            Assert.AreEqual(12, exp.Invoke("Vasya", 10));
        }
    
        [Test]
        public void Remove()
        {
            //Arrange:
            Func<string,int,int> member1 = (s, i) => -2;
            Func<string,int,int> member2 = (s, i) => i;
            Func<string,int,int> member3 = (s, i) => s == "Vasya" ? 5 : 0;
            
            var exp = new IntSumExpression<string, int>(
                member1,
                member2,
                member3
            );
    
            //Pre-assert:
            Assert.AreEqual(8, exp.Invoke("Vasya", 5));
            Assert.AreEqual(3, exp.MemberCount);
    
            Assert.IsTrue(exp.Contains(member1));
            Assert.IsTrue(exp.Contains(member2));
            Assert.IsTrue(exp.Contains(member3));
    
            //Act:
            exp.Remove(member2);
            exp.Remove(member3);
    
            //Assert:
            Assert.AreEqual(-2, exp.Invoke("Vasya", 10));
            Assert.AreEqual(1, exp.MemberCount);
    
            Assert.IsTrue(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
            Assert.IsFalse(exp.Contains(member3));
        }
    
        [Test]
        public void Clear()
        {
            //Arrange:
            Func<string,int,int> member1 = (s, i) => -2;
            Func<string,int,int> member2 = (s, i) => i;
            Func<string,int,int> member3 = (s, i) => s == "Vasya" ? 5 : 0;
            
            var exp = new IntSumExpression<string, int>(
                member1,
                member2,
                member3
            );
            
            Assert.AreEqual(13, exp.Invoke("Vasya", 10));
    
            //Act:
            exp.Clear();
    
            //Assert:
            Assert.AreEqual(0, exp.MemberCount);
            Assert.AreEqual(0, exp.Invoke("Vasya", 10));
            
            Assert.IsFalse(exp.Contains(member1));
            Assert.IsFalse(exp.Contains(member2));
            Assert.IsFalse(exp.Contains(member3));
        }
    }
}