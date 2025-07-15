using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression composed of parameterless function members returning values of type <typeparamref name="T"/>.
    /// Supports dynamic addition, removal, and evaluation of function members.
    /// </summary>
    /// <typeparam name="T">The return type of the functions.</typeparam>
    public interface IExpression<T> : IValue<T>
    {
        /// <summary>
        /// Gets the number of function members in the expression.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a function to the expression.
        /// </summary>
        /// <param name="member">A function that returns a value of type <typeparamref name="T"/>.</param>
        void Add(Func<T> member);

        /// <summary>
        /// Removes a function from the expression.
        /// </summary>
        /// <param name="member">The function to remove.</param>
        void Remove(Func<T> member);

        /// <summary>
        /// Determines whether the specified function exists in the expression.
        /// </summary>
        /// <param name="member">The function to check.</param>
        /// <returns>True if the function is present; otherwise, false.</returns>
        bool Contains(Func<T> member);

        /// <summary>
        /// Removes all function members from the expression.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Represents an expression composed of function members that accept a parameter of type <typeparamref name="T"/>
    /// and return a value of type <typeparamref name="R"/>.
    /// Supports dynamic addition, removal, and evaluation of function members.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    /// <typeparam name="R">The return type of the functions.</typeparam>
    public interface IExpression<T, R> : IFunction<T, R>
    {
        /// <summary>
        /// Gets the number of function members in the expression.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a function to the expression.
        /// </summary>
        /// <param name="member">A function that takes a <typeparamref name="T"/> and returns <typeparamref name="R"/>.</param>
        void Add(Func<T, R> member);

        /// <summary>
        /// Removes a function from the expression.
        /// </summary>
        /// <param name="member">The function to remove.</param>
        void Remove(Func<T, R> member);

        /// <summary>
        /// Determines whether the specified function exists in the expression.
        /// </summary>
        /// <param name="member">The function to check.</param>
        /// <returns>True if the function is present; otherwise, false.</returns>
        bool Contains(Func<T, R> member);

        /// <summary>
        /// Removes all function members from the expression.
        /// </summary>
        void Clear();
    }

    /// <summary>
    /// Represents an expression composed of binary functions that take two input parameters of types <typeparamref name="T1"/> and <typeparamref name="T2"/>,
    /// and return a value of type <typeparamref name="R"/>.
    /// Supports dynamic management of multiple function members.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    /// <typeparam name="R">The return type of the functions.</typeparam>
    public interface IExpression<T1, T2, R> : IFunction<T1, T2, R>
    {
        /// <summary>
        /// Gets the number of function members in the expression.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a function to the expression.
        /// </summary>
        /// <param name="member">A function that takes <typeparamref name="T1"/> and <typeparamref name="T2"/>, and returns <typeparamref name="R"/>.</param>
        void Add(Func<T1, T2, R> member);

        /// <summary>
        /// Removes a function from the expression.
        /// </summary>
        /// <param name="member">The function to remove.</param>
        void Remove(Func<T1, T2, R> member);

        /// <summary>
        /// Determines whether the specified function exists in the expression.
        /// </summary>
        /// <param name="member">The function to check.</param>
        /// <returns>True if the function is present; otherwise, false.</returns>
        bool Contains(Func<T1, T2, R> member);

        /// <summary>
        /// Removes all function members from the expression.
        /// </summary>
        void Clear();
    }
}