// using System;
// using System.Collections.Generic;
//
// namespace Atomic.Elements
// {
//     /// <summary>
//     /// Represents an expression that computes the sum of multiple parameterless integer-returning functions.
//     /// </summary>
//     [Serializable]
//     public class IntSumExpression : ExpressionBase<int>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="IntSumExpression"/> class.
//         /// </summary>
//         public IntSumExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with the specified function members.
//         /// </summary>
//         /// <param name="members">An array of functions that return an integer value.</param>
//         public IntSumExpression(params Func<int>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that return an integer value.</param>
//         public IntSumExpression(IEnumerable<Func<int>> members) : base(members) { }
//
//         protected int InitialValue() => 0;
//         
//         
//         /// <summary>
//         /// Evaluates the expression by summing the results of all function members.
//         /// </summary>
//         /// <param name="members">The list of function members to evaluate.</param>
//         /// <returns>The sum of all evaluated function results, or 0 if none exist.</returns>
//         protected override bool Reduce(Func<int> member, ref int current)
//         {
//             current += member.Invoke();
//             return true;
//         }
//     
//     
//     //     int count = members.Count;
//     //     if (count == 0)
//     //         return 0;
//     //
//     //     int result = 0;
//     //
//     //     for (int i = 0; i < count; i++)
//     //         result += members[i].Invoke();
//     //
//     //     return result;
//     }
//
//     /// <summary>
//     /// Represents an expression that computes the sum of integer values returned from functions with a single input parameter.
//     /// </summary>
//     /// <typeparam name="T">The input parameter type.</typeparam>
//     [Serializable]
//     public class IntSumExpression<T> : ExpressionBase<T, int>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="IntSumExpression{T}"/> class.
//         /// </summary>
//         public IntSumExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that take <typeparamref name="T"/> and return an integer value.</param>
//         public IntSumExpression(IEnumerable<Func<T, int>> members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with an array of function members.
//         /// </summary>
//         /// <param name="members">An array of functions that take <typeparamref name="T"/> and return an integer value.</param>
//         public IntSumExpression(params Func<T, int>[] members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by summing the results of all function members for a given input.
//         /// </summary>
//         /// <param name="members">The list of function members to evaluate.</param>
//         /// <param name="arg">The input argument passed to each function.</param>
//         /// <returns>The sum of all evaluated results, or 0 if none exist.</returns>
//         protected override int Reduce(IReadOnlyList members, T arg)
//         {
//         
//         }
//
//         
//         // int count = members.Count;
//         // if (count == 0)
//         //     return 0;
//         //
//         // int result = 0;
//         //
//         // for (int i = 0; i < count; i++)
//         //     result += members[i].Invoke(arg);
//         //
//         // return result;
//         
//         protected int InitialValue() => 0;
//     }
//
//     /// <summary>
//     /// Represents an expression that computes the sum of integer values returned from functions with two input parameters.
//     /// </summary>
//     /// <typeparam name="T1">The first input parameter type.</typeparam>
//     /// <typeparam name="T2">The second input parameter type.</typeparam>
//     [Serializable]
//     public class IntSumExpression<T1, T2> : ExpressionBase<T1, T2, int>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="IntSumExpression{T1, T2}"/> class.
//         /// </summary>
//         public IntSumExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
//         public IntSumExpression(IEnumerable<Func<T1, T2, int>> members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with an array of function members.
//         /// </summary>
//         /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
//         public IntSumExpression(params Func<T1, T2, int>[] members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by summing the results of all function members for the given input arguments.
//         /// </summary>
//         /// <param name="members">The list of function members to evaluate.</param>
//         /// <param name="arg1">The first input argument.</param>
//         /// <param name="arg2">The second input argument.</param>
//         /// <returns>The sum of all evaluated results, or 0 if none exist.</returns>
//         protected override int Invoke(IReadOnlyList<Func<T1, T2, int>> members, T1 arg1, T2 arg2)
//         {
//             int count = members.Count;
//             if (count == 0)
//                 return 0;
//
//             int result = 0;
//
//             for (int i = 0; i < count; i++)
//                 result += members[i].Invoke(arg1, arg2);
//
//             return result;
//         }
//     }
// }