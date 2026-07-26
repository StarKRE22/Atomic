using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Atomic.Elements
{
    [TestFixture]
    public sealed class ExpressionExtensionsTests
    {
        #region Test Helpers

        private sealed class FunctionStub : IFunction<float>
        {
            private readonly float _value;

            public FunctionStub(float value)
            {
                _value = value;
            }

            public float Invoke() => _value;
        }

        private sealed class FunctionStub<T, R> : IFunction<T, R>
        {
            private readonly Func<T, R> _func;

            public FunctionStub(Func<T, R> func)
            {
                _func = func;
            }

            public R Invoke(T arg) => _func(arg);
        }

        private sealed class FunctionStub<T1, T2, R> : IFunction<T1, T2, R>
        {
            private readonly Func<T1, T2, R> _func;

            public FunctionStub(Func<T1, T2, R> func)
            {
                _func = func;
            }

            public R Invoke(T1 arg1, T2 arg2) => _func(arg1, arg2);
        }

        #endregion

        #region Sum Extensions

        [TestFixture]
        public sealed class SumFloatTests
        {
            [Test]
            public void Sum_NullFloatCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<float> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.Sum());
            }

            [Test]
            public void Sum_EmptyFloatCollection_ReturnsZero()
            {
                //Arrange:
                var list = new List<float>();

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(0f, result);
            }

            [Test]
            public void Sum_SingleFloatElement_ReturnsThatElement()
            {
                //Arrange:
                var list = new List<float> { 7.5f };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(7.5f, result);
            }

            [Test]
            public void Sum_MultiplePositiveFloatElements_ReturnsCorrectSum()
            {
                //Arrange:
                var list = new List<float> { 1.5f, 2.5f, 3.0f, 4.0f };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(11.0f, result);
            }

            [Test]
            public void Sum_MixedSignFloatElements_ReturnsCorrectSum()
            {
                //Arrange:
                var list = new List<float> { 10.0f, -3.5f, 2.0f, -5.5f };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(3.0f, result);
            }

            [Test]
            public void Sum_NegativeOnlyFloatElements_ReturnsNegativeSum()
            {
                //Arrange:
                var list = new List<float> { -1.0f, -2.0f, -3.0f };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(-6.0f, result);
            }
        }

        [TestFixture]
        public sealed class SumIntTests
        {
            [Test]
            public void Sum_NullIntCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<int> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.Sum());
            }

            [Test]
            public void Sum_EmptyIntCollection_ReturnsZero()
            {
                //Arrange:
                var list = new List<int>();

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(0f, result);
            }

            [Test]
            public void Sum_SingleIntElement_ReturnsThatElementAsFloat()
            {
                //Arrange:
                var list = new List<int> { 42 };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(42f, result);
            }

            [Test]
            public void Sum_MultipleIntElements_ReturnsCorrectSum()
            {
                //Arrange:
                var list = new List<int> { 1, 2, 3, 4, 5 };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(15f, result);
            }

            [Test]
            public void Sum_MixedSignIntElements_ReturnsCorrectSum()
            {
                //Arrange:
                var list = new List<int> { 10, -4, 3, -5 };

                //Act:
                float result = list.Sum();

                //Assert:
                Assert.AreEqual(4f, result);
            }
        }

        #endregion

        #region Multiply Extensions

        [TestFixture]
        public sealed class MultiplyFloatTests
        {
            [Test]
            public void Multiply_NullFloatCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<float> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.Multiply());
            }

            [Test]
            public void Multiply_EmptyFloatCollection_ReturnsOne()
            {
                //Arrange:
                var list = new List<float>();

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(1f, result);
            }

            [Test]
            public void Multiply_SingleFloatElement_ReturnsThatElement()
            {
                //Arrange:
                var list = new List<float> { 5.5f };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(5.5f, result);
            }

            [Test]
            public void Multiply_MultipleFloatElements_ReturnsCorrectProduct()
            {
                //Arrange:
                var list = new List<float> { 2.0f, 3.0f, 4.0f };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(24.0f, result);
            }

            [Test]
            public void Multiply_FloatWithZero_ReturnsZero()
            {
                //Arrange:
                var list = new List<float> { 5.0f, 0.0f, 3.0f };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(0f, result);
            }

            [Test]
            public void Multiply_FloatWithNegativeValues_ReturnsCorrectProduct()
            {
                //Arrange:
                var list = new List<float> { -2.0f, 3.0f };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(-6.0f, result);
            }
        }

        [TestFixture]
        public sealed class MultiplyIntTests
        {
            [Test]
            public void Multiply_NullIntCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<int> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.Multiply());
            }

            [Test]
            public void Multiply_EmptyIntCollection_ReturnsOne()
            {
                //Arrange:
                var list = new List<int>();

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(1f, result);
            }

            [Test]
            public void Multiply_SingleIntElement_ReturnsThatElementAsFloat()
            {
                //Arrange:
                var list = new List<int> { 7 };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(7f, result);
            }

            [Test]
            public void Multiply_MultipleIntElements_ReturnsCorrectProduct()
            {
                //Arrange:
                var list = new List<int> { 2, 3, 4 };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(24f, result);
            }

            [Test]
            public void Multiply_IntWithNegativeValues_ReturnsCorrectProduct()
            {
                //Arrange:
                var list = new List<int> { -2, 5 };

                //Act:
                float result = list.Multiply();

                //Assert:
                Assert.AreEqual(-10f, result);
            }
        }

        #endregion

        #region And Extension

        [TestFixture]
        public sealed class AndTests
        {
            [Test]
            public void And_NullCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<bool> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.And());
            }

            [Test]
            public void And_EmptyCollection_ReturnsTrue()
            {
                //Arrange:
                var list = new List<bool>();

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void And_AllTrue_ReturnsTrue()
            {
                //Arrange:
                var list = new List<bool> { true, true, true };

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void And_SingleTrue_ReturnsTrue()
            {
                //Arrange:
                var list = new List<bool> { true };

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void And_AnyFalse_ReturnsFalse()
            {
                //Arrange:
                var list = new List<bool> { true, false, true };

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsFalse(result);
            }

            [Test]
            public void And_AllFalse_ReturnsFalse()
            {
                //Arrange:
                var list = new List<bool> { false, false, false };

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsFalse(result);
            }

            [Test]
            public void And_SingleFalse_ReturnsFalse()
            {
                //Arrange:
                var list = new List<bool> { false };

                //Act:
                bool result = list.And();

                //Assert:
                Assert.IsFalse(result);
            }
        }

        #endregion

        #region Or Extension

        [TestFixture]
        public sealed class OrTests
        {
            [Test]
            public void Or_NullCollection_ThrowsArgumentNullException()
            {
                //Arrange:
                IEnumerable<bool> list = null;

                //Assert:
                Assert.Throws<ArgumentNullException>(() => list.Or());
            }

            [Test]
            public void Or_EmptyCollection_ReturnsFalse()
            {
                //Arrange:
                var list = new List<bool>();

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsFalse(result);
            }

            [Test]
            public void Or_AllTrue_ReturnsFalse()
            {
                //Arrange: All true returns false (no false triggers the branch).
                var list = new List<bool> { true, true, true };

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsFalse(result);
            }

            [Test]
            public void Or_SingleFalse_ReturnsTrue()
            {
                //Arrange: A single false triggers the negation branch.
                var list = new List<bool> { false };

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void Or_MixedWithFalse_ReturnsTrue()
            {
                //Arrange: First false encountered triggers early return.
                var list = new List<bool> { true, false, true };

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void Or_AllFalse_ReturnsTrue()
            {
                //Arrange: First false triggers the branch.
                var list = new List<bool> { false, false, false };

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsTrue(result);
            }

            [Test]
            public void Or_SingleTrue_ReturnsFalse()
            {
                //Arrange: Single true doesn't trigger the branch.
                var list = new List<bool> { true };

                //Act:
                bool result = list.Or();

                //Assert:
                Assert.IsFalse(result);
            }
        }

        #endregion

        #region Add Extensions — IExpression<R>

        [TestFixture]
        public sealed class AddExpressionRTests
        {
            [Test]
            public void Add_WithSourceAndFunc_AddsMemberAndReturnsSelf()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                var source = new object();
                Func<float> func = () => 5f;

                //Act:
                IExpression<float> result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(5f, expression.Value);
            }

            [Test]
            public void Add_WithSourceAndIFunction_AddsMemberViaInvokeAndReturnsSelf()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                var source = new object();
                IFunction<float> func = new FunctionStub(3f);

                //Act:
                IExpression<float> result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(3f, expression.Value);
            }

            [Test]
            public void Add_WithIFunctionOnly_AddsMemberWithNullSource()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                IFunction<float> func = new FunctionStub(7f);

                //Act:
                IExpression<float> result = expression.Add(func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(7f, expression.Value);
            }

            [Test]
            public void Add_WithKVP_AddsMemberFromKeyValuePair()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                var source = new object();
                IFunction<float> func = new FunctionStub(4f);
                var kvp = new KeyValuePair<object, IFunction<float>>(source, func);

                //Act:
                IExpression<float> result = expression.Add(kvp);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(4f, expression.Value);
            }

            [Test]
            public void Add_WithFuncOnly_AddsMemberWithNullSourceAndReturnsSelf()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                Func<float> func = () => 6f;

                //Act:
                IExpression<float> result = expression.Add(func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(6f, expression.Value);
            }

            [Test]
            public void Add_MultipleOverloads_CanBeChainedFluently()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                var source = new object();

                //Act:
                expression
                    .Add(source, (Func<float>)(() => 1f))
                    .Add(new FunctionStub(2f))
                    .Add(() => 3f);

                //Assert:
                Assert.AreEqual(3, expression.Count);
                Assert.AreEqual(6f, expression.Value);
            }
        }

        #endregion

        #region Add Extensions — IExpression<T, R>

        [TestFixture]
        public sealed class AddExpressionT1RTests
        {
            [Test]
            public void Add_WithSourceAndFunc_AddsMemberAndReturnsSelf()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                var source = new object();
                Func<string, float> func = s => s == "hello" ? 10f : 0f;

                //Act:
                var result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(10f, expression.Invoke("hello"));
                Assert.AreEqual(0f, expression.Invoke("other"));
            }

            [Test]
            public void Add_WithSourceAndIFunction_AddsMemberViaInvoke()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                var source = new object();
                IFunction<string, float> func = new FunctionStub<string, float>(s => s.Length);

                //Act:
                var result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(5f, expression.Invoke("hello"));
            }

            [Test]
            public void Add_WithKVP_AddsMemberFromKeyValuePair()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                var source = new object();
                IFunction<string, float> func = new FunctionStub<string, float>(s => 42f);
                var kvp = new KeyValuePair<object, IFunction<string, float>>(source, func);

                //Act:
                var result = expression.Add(kvp);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(42f, expression.Invoke("any"));
            }

            [Test]
            public void Add_WithFuncOnly_AddsMemberWithNullSource()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                Func<string, float> func = s => s == "test" ? 99f : 0f;

                //Act:
                var result = expression.Add(func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(99f, expression.Invoke("test"));
            }

            [Test]
            public void Add_MultipleOverloads_CanBeChainedFluently()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();

                //Act:
                expression
                    .Add((Func<string, float>)(s => s.Length))
                    .Add(null, new FunctionStub<string, float>(s => 10f));

                //Assert:
                Assert.AreEqual(2, expression.Count);
                Assert.AreEqual(15f, expression.Invoke("hello"));
            }
        }

        #endregion

        #region Add Extensions — IExpression<T1, T2, R>

        [TestFixture]
        public sealed class AddExpressionT1T2RTests
        {
            [Test]
            public void Add_WithSourceAndFunc_AddsMemberAndReturnsSelf()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                var source = new object();
                Func<string, int, float> func = (s, i) => i;

                //Act:
                var result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(7f, expression.Invoke("test", 7));
            }

            [Test]
            public void Add_WithSourceAndIFunction_AddsMemberViaInvoke()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                var source = new object();
                IFunction<string, int, float> func = new FunctionStub<string, int, float>((s, i) => i * 2f);

                //Act:
                var result = expression.Add(source, func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(10f, expression.Invoke("test", 5));
            }

            [Test]
            public void Add_WithKVP_AddsMemberFromKeyValuePair()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                var source = new object();
                IFunction<string, int, float> func = new FunctionStub<string, int, float>((s, i) => 3f);
                var kvp = new KeyValuePair<object, IFunction<string, int, float>>(source, func);

                //Act:
                var result = expression.Add(kvp);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(3f, expression.Invoke("any", 99));
            }

            [Test]
            public void Add_WithFuncOnly_AddsMemberWithNullSource()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                Func<string, int, float> func = (s, i) => s.Length + i;

                //Act:
                var result = expression.Add(func);

                //Assert:
                Assert.AreSame(expression, result);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(8f, expression.Invoke("hello", 3));
            }

            [Test]
            public void Add_MultipleOverloads_CanBeChainedFluently()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();

                //Act:
                expression
                    .Add((Func<string, int, float>)((s, i) => i))
                    .Add(null, new FunctionStub<string, int, float>((s, i) => 1f));

                //Assert:
                Assert.AreEqual(2, expression.Count);
                Assert.AreEqual(11f, expression.Invoke("test", 10));
            }
        }

        #endregion

        #region Remove Extensions — IExpression<R>

        [TestFixture]
        public sealed class RemoveExpressionRTests
        {
            [Test]
            public void RemoveBySource_ExistingSource_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var source1 = new object();
                var source2 = new object();
                var expression = new FloatSumExpression();
                expression.Add(source1, () => 5f);
                expression.Add(source2, () => 10f);

                //Act:
                bool removed = expression.Remove(source1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(10f, expression.Value);
            }

            [Test]
            public void RemoveBySource_NonExistingSource_ReturnsFalseAndDoesNotModify()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                expression.Add(new object(), () => 5f);

                //Act:
                bool removed = expression.Remove(new object());

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(5f, expression.Value);
            }

            [Test]
            public void RemoveByFunc_ExistingFunc_ReturnsTrueAndRemoves()
            {
                //Arrange:
                Func<float> func1 = () => 5f;
                Func<float> func2 = () => 10f;
                var expression = new FloatSumExpression();
                expression.Add(func1);
                expression.Add(func2);

                //Act:
                bool removed = expression.Remove(func1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(10f, expression.Value);
            }

            [Test]
            public void RemoveByFunc_NonExistingFunc_ReturnsFalseAndDoesNotModify()
            {
                //Arrange:
                Func<float> func1 = () => 5f;
                var expression = new FloatSumExpression();
                expression.Add(func1);

                //Act:
                bool removed = expression.Remove((Func<float>)(() => 10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByIFunction_ExistingIFunction_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var stub = new FunctionStub(5f);
                var expression = new FloatSumExpression();
                expression.Add(stub);
                expression.Add(new FunctionStub(10f));

                //Act:
                bool removed = expression.Remove(stub);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
                Assert.AreEqual(10f, expression.Value);
            }

            [Test]
            public void RemoveByIFunction_NonExistingIFunction_ReturnsFalse()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                expression.Add(new FunctionStub(5f));

                //Act:
                bool removed = expression.Remove(new FunctionStub(10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }
        }

        #endregion

        #region Remove Extensions — IExpression<T, R>

        [TestFixture]
        public sealed class RemoveExpressionT1RTests
        {
            [Test]
            public void RemoveBySource_ExistingSource_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var source1 = new object();
                var source2 = new object();
                var expression = new FloatSumExpression<string>();
                expression.Add(source1, (Func<string, float>)(s => 5f));
                expression.Add(source2, (Func<string, float>)(s => 10f));

                //Act:
                bool removed = expression.Remove(source1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveBySource_NonExistingSource_ReturnsFalse()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                expression.Add(new object(), (Func<string, float>)(s => 5f));

                //Act:
                bool removed = expression.Remove(new object());

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByFunc_ExistingFunc_ReturnsTrueAndRemoves()
            {
                //Arrange:
                Func<string, float> func1 = s => 5f;
                Func<string, float> func2 = s => 10f;
                var expression = new FloatSumExpression<string>();
                expression.Add(func1);
                expression.Add(func2);

                //Act:
                bool removed = expression.Remove(func1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByFunc_NonExistingFunc_ReturnsFalse()
            {
                //Arrange:
                Func<string, float> func1 = s => 5f;
                var expression = new FloatSumExpression<string>();
                expression.Add(func1);

                //Act:
                bool removed = expression.Remove((Func<string, float>)(s => 10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByIFunction_ExistingIFunction_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var stub = new FunctionStub<string, float>(s => 5f);
                var expression = new FloatSumExpression<string>();
                expression.Add(null, stub);
                expression.Add(null, new FunctionStub<string, float>(s => 10f));

                //Act:
                bool removed = expression.Remove(stub);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByIFunction_NonExistingIFunction_ReturnsFalse()
            {
                //Arrange:
                var expression = new FloatSumExpression<string>();
                expression.Add(null, new FunctionStub<string, float>(s => 5f));

                //Act:
                bool removed = expression.Remove(new FunctionStub<string, float>(s => 10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }
        }

        #endregion

        #region Remove Extensions — IExpression<T1, T2, R>

        [TestFixture]
        public sealed class RemoveExpressionT1T2RTests
        {
            [Test]
            public void RemoveBySource_ExistingSource_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var source1 = new object();
                var source2 = new object();
                var expression = new FloatSumExpression<string, int>();
                expression.Add(source1, (Func<string, int, float>)((s, i) => 5f));
                expression.Add(source2, (Func<string, int, float>)((s, i) => 10f));

                //Act:
                bool removed = expression.Remove(source1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveBySource_NonExistingSource_ReturnsFalse()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                expression.Add(new object(), (Func<string, int, float>)((s, i) => 5f));

                //Act:
                bool removed = expression.Remove(new object());

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByFunc_ExistingFunc_ReturnsTrueAndRemoves()
            {
                //Arrange:
                Func<string, int, float> func1 = (s, i) => 5f;
                Func<string, int, float> func2 = (s, i) => 10f;
                var expression = new FloatSumExpression<string, int>();
                expression.Add(func1);
                expression.Add(func2);

                //Act:
                bool removed = expression.Remove(func1);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByFunc_NonExistingFunc_ReturnsFalse()
            {
                //Arrange:
                Func<string, int, float> func1 = (s, i) => 5f;
                var expression = new FloatSumExpression<string, int>();
                expression.Add(func1);

                //Act:
                bool removed = expression.Remove((Func<string, int, float>)((s, i) => 10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByIFunction_ExistingIFunction_ReturnsTrueAndRemoves()
            {
                //Arrange:
                var stub = new FunctionStub<string, int, float>((s, i) => 5f);
                var expression = new FloatSumExpression<string, int>();
                expression.Add(null, stub);
                expression.Add(null, new FunctionStub<string, int, float>((s, i) => 10f));

                //Act:
                bool removed = expression.Remove(stub);

                //Assert:
                Assert.IsTrue(removed);
                Assert.AreEqual(1, expression.Count);
            }

            [Test]
            public void RemoveByIFunction_NonExistingIFunction_ReturnsFalse()
            {
                //Arrange:
                var expression = new FloatSumExpression<string, int>();
                expression.Add(null, new FunctionStub<string, int, float>((s, i) => 5f));

                //Act:
                bool removed = expression.Remove(new FunctionStub<string, int, float>((s, i) => 10f));

                //Assert:
                Assert.IsFalse(removed);
                Assert.AreEqual(1, expression.Count);
            }
        }

        #endregion

        #region SubscribeState

        [TestFixture]
        public sealed class SubscribeStateTests
        {
            [Test]
            public void SubscribeState_FiresOnStateChangeWithCurrentValue()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                float capturedValue = -1f;
                int fireCount = 0;

                var subscription = expression.SubscribeState(v =>
                {
                    capturedValue = v;
                    fireCount++;
                });

                try
                {
                    //Act:
                    expression.Add(() => 10f);

                    //Assert:
                    Assert.AreEqual(1, fireCount);
                    Assert.AreEqual(10f, capturedValue);
                }
                finally
                {
                    subscription.Dispose();
                }
            }

            [Test]
            public void SubscribeState_FiresOnEachAddMember()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                float lastValue = -1f;

                var subscription = expression.SubscribeState(v => lastValue = v);

                try
                {
                    //Act:
                    expression.Add(() => 1f);
                    Assert.AreEqual(1f, lastValue);

                    expression.Add(() => 2f);

                    //Assert:
                    Assert.AreEqual(3f, lastValue);
                }
                finally
                {
                    subscription.Dispose();
                }
            }

            [Test]
            public void SubscribeState_FiresOnClear()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                expression.Add(() => 5f);
                float lastValue = -1f;

                var subscription = expression.SubscribeState(v => lastValue = v);

                try
                {
                    //Act:
                    expression.Clear();

                    //Assert:
                    Assert.AreEqual(0f, lastValue);
                }
                finally
                {
                    subscription.Dispose();
                }
            }

            [Test]
            public void SubscribeState_DoesNotFireAfterDispose()
            {
                //Arrange:
                var expression = new FloatSumExpression();
                float capturedValue = -1f;
                int fireCount = 0;

                var subscription = expression.SubscribeState(v =>
                {
                    capturedValue = v;
                    fireCount++;
                });

                //Act: First add fires callback
                expression.Add(() => 10f);
                Assert.AreEqual(1, fireCount);
                Assert.AreEqual(10f, capturedValue);

                //Act: Dispose subscription
                subscription.Dispose();

                //Act: Second add should NOT fire callback
                expression.Add(() => 20f);

                //Assert:
                Assert.AreEqual(1, fireCount);
                Assert.AreEqual(10f, capturedValue);
            }

            [Test]
            public void SubscribeState_ReturnsCorrectSubscriptionType()
            {
                //Arrange:
                var expression = new FloatSumExpression();

                //Act:
                var subscription = expression.SubscribeState(v => { });

                //Assert:
                Assert.IsInstanceOf<IReadOnlyReactiveList<ExpressionMember<float>>.StateChangedSubscription>(subscription);
                subscription.Dispose();
            }
        }

        #endregion
    }
}
