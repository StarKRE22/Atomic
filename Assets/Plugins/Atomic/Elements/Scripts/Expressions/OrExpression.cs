// using System;
// using System.Collections.Generic;
//
// namespace Atomic.Elements
// {
//     /// <summary>
//     /// Represents a logical OR expression composed of parameterless boolean-returning functions.
//     /// The expression evaluates to <c>true</c> if at least one function returns <c>true</c>.
//     /// </summary>
//     [Serializable]
//     public class OrExpression : ExpressionBase<bool>, IPredicate
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="OrExpression"/> class.
//         /// </summary>
//         public OrExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of function members.
//         /// </summary>
//         /// <param name="members">A collection of functions that return <c>true</c> or <c>false</c>.</param>
//         public OrExpression(IEnumerable<Func<bool>> members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with an array of function members.
//         /// </summary>
//         /// <param name="members">An array of functions that return <c>true</c> or <c>false</c>.</param>
//         public OrExpression(params Func<bool>[] members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression by checking if at least one member returns <c>true</c>.
//         /// </summary>
//         /// <param name="members">The list of boolean-returning functions.</param>
//         /// <returns><c>true</c> if any function returns <c>true</c>; otherwise, <c>false</c>.</returns>
//         protected override bool Invoke(IReadOnlyList<Func<bool>> members)
//         {
//             int count = members.Count;
//             if (count == 0)
//                 return false;
//
//             for (int i = 0; i < count; i++)
//                 if (members[i].Invoke())
//                     return true;
//
//             return false;
//         }
//     }
//
//     /// <summary>
//     /// Represents a logical OR expression composed of functions that take a single argument and return a boolean value.
//     /// </summary>
//     /// <typeparam name="T">The input type for the predicate functions.</typeparam>
//     [Serializable]
//     public class OrExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="OrExpression{T}"/> class.
//         /// </summary>
//         public OrExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with an array of predicate functions.
//         /// </summary>
//         /// <param name="members">An array of functions that take <typeparamref name="T"/> and return a boolean.</param>
//         public OrExpression(params Func<T, bool>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of predicate functions.
//         /// </summary>
//         /// <param name="members">A collection of functions that take <typeparamref name="T"/> and return a boolean.</param>
//         public OrExpression(IEnumerable<Func<T, bool>> members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression for the given argument by checking if any predicate returns <c>true</c>.
//         /// </summary>
//         /// <param name="members">The list of predicate functions.</param>
//         /// <param name="args">The input argument.</param>
//         /// <returns><c>true</c> if any predicate returns <c>true</c>; otherwise, <c>false</c>.</returns>
//         protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
//         {
//             int count = members.Count;
//             if (count == 0)
//                 return false;
//
//             for (int i = 0; i < count; i++)
//                 if (members[i].Invoke(args))
//                     return true;
//
//             return false;
//         }
//     }
//
//     /// <summary>
//     /// Represents a logical OR expression composed of functions that take two arguments and return a boolean value.
//     /// </summary>
//     /// <typeparam name="T1">The type of the first input argument.</typeparam>
//     /// <typeparam name="T2">The type of the second input argument.</typeparam>
//     [Serializable]
//     public class OrExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
//     {
//         /// <summary>
//         /// Initializes a new empty instance of the <see cref="OrExpression{T1, T2}"/> class.
//         /// </summary>
//         public OrExpression() { }
//
//         /// <summary>
//         /// Initializes the expression with an array of binary predicate functions.
//         /// </summary>
//         /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a boolean.</param>
//         public OrExpression(params Func<T1, T2, bool>[] members) : base(members) { }
//
//         /// <summary>
//         /// Initializes the expression with a collection of binary predicate functions.
//         /// </summary>
//         /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a boolean.</param>
//         public OrExpression(IEnumerable<Func<T1, T2, bool>> members) : base(members) { }
//
//         /// <summary>
//         /// Evaluates the expression for the given arguments by checking if any predicate returns <c>true</c>.
//         /// </summary>
//         /// <param name="members">The list of predicate functions.</param>
//         /// <param name="arg1">The first input argument.</param>
//         /// <param name="arg2">The second input argument.</param>
//         /// <returns><c>true</c> if any predicate returns <c>true</c>; otherwise, <c>false</c>.</returns>
//         protected override bool Invoke(IReadOnlyList<Func<T1, T2, bool>> members, T1 arg1, T2 arg2)
//         {
//             int count = members.Count;
//             if (count == 0)
//                 return false;
//
//             for (int i = 0; i < count; i++)
//                 if (members[i].Invoke(arg1, arg2))
//                     return true;
//
//             return false;
//         }
//     }
// }
