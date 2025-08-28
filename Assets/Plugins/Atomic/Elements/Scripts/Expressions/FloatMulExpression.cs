// using System;
// using System.Collections.Generic;
//
// namespace Atomic.Elements
// {
//     /// <summary>
//     /// Represents an expression that computes the product of multiple parameterless float-returning functions.
//     /// </summary>
//     [Serializable]
//     public class FloatMulExpression : ExpressionBase<float>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="FloatMulExpression"/> class.
//         /// </summary>
//         public FloatMulExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of float-returning functions.</param>
//         public FloatMulExpression(params Func<float>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with the specified collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of float-returning functions.</param>
//         public FloatMulExpression(IEnumerable<Func<float>> members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by multiplying the results of all function members.
//         /// </summary>
//         /// <param name="members">The list of function members.</param>
//         /// <returns>The product of all evaluated functions, or 1 if none exist.</returns>
//         protected override float Invoke(IReadOnlyList<Func<float>> members)
//         {
//             float result = 1;
//             int count = members.Count;
//             if (count == 0)
//                 return result;
//
//             for (int i = 0; i < count; i++) 
//                 result *= members[i].Invoke();
//
//             return result;
//         }
//     }
//
//     /// <summary>
//     /// Represents an expression that computes the product of float values returned from functions with a single input parameter.
//     /// </summary>
//     /// <typeparam name="T">The input parameter type.</typeparam>
//     [Serializable]
//     public class FloatMulExpression<T> : ExpressionBase<T, float>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="FloatMulExpression{T}"/> class.
//         /// </summary>
//         public FloatMulExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of functions that take a <typeparamref name="T"/> and return a float.</param>
//         public FloatMulExpression(params Func<T, float>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with the specified collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that take a <typeparamref name="T"/> and return a float.</param>
//         public FloatMulExpression(IEnumerable<Func<T, float>> members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by multiplying the results of all function members for a given input.
//         /// </summary>
//         /// <param name="members">The list of function members.</param>
//         /// <param name="arg">The input value passed to each function.</param>
//         /// <returns>The product of all evaluated results, or 1 if none exist.</returns>
//         protected override float Invoke(IReadOnlyList<Func<T, float>> members, T arg)
//         {
//             float result = 1;
//             int count = members.Count;
//             if (count == 0)
//                 return result;
//
//             for (int i = 0; i < count; i++) 
//                 result *= members[i].Invoke(arg);
//
//             return result;
//         }
//     }
//
//     /// <summary>
//     /// Represents an expression that computes the product of float values returned from functions with two input parameters.
//     /// </summary>
//     /// <typeparam name="T1">The first input parameter type.</typeparam>
//     /// <typeparam name="T2">The second input parameter type.</typeparam>
//     [Serializable]
//     public class FloatMulExpression<T1, T2> : ExpressionBase<T1, T2, float>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="FloatMulExpression{T1, T2}"/> class.
//         /// </summary>
//         public FloatMulExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a float.</param>
//         public FloatMulExpression(params Func<T1, T2, float>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with the specified collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a float.</param>
//         public FloatMulExpression(IEnumerable<Func<T1, T2, float>> members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by multiplying the results of all function members for the given input values.
//         /// </summary>
//         /// <param name="members">The list of function members.</param>
//         /// <param name="arg1">The first input value.</param>
//         /// <param name="arg2">The second input value.</param>
//         /// <returns>The product of all evaluated results, or 1 if none exist.</returns>
//         protected override float Invoke(IReadOnlyList<Func<T1, T2, float>> members, T1 arg1, T2 arg2)
//         {
//             float result = 1;
//             
//             int count = members.Count;
//             if (count == 0)
//                 return result;
//
//             for (int i = 0; i < count; i++) 
//                 result *= members[i].Invoke(arg1, arg2);
//
//             return result;
//         }
//     }
// }