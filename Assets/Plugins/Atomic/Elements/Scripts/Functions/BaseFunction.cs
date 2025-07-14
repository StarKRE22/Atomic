using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless function returning a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    [Serializable]
    public class BaseFunction<T> : IValue<T>
    {
        private Func<T> func;

        /// <summary>
        /// Default constructor. Does not initialize the function.
        /// </summary>
        public BaseFunction()
        {
        }

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BaseFunction(Func<T> func) => this.func = func;

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T}"/> to <see cref="BaseFunction{T}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BaseFunction<T>(Func<T> value) => new(value);

        /// <summary>
        /// Gets the result of the function.
        /// </summary>
        public T Value => this.func != null ? this.func.Invoke() : default;

        /// <summary>
        /// Replaces the internal function with the given one.
        /// </summary>
        /// <param name="func">The function delegate to assign.</param>
        public void Construct(Func<T> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the function and returns its result.
        /// </summary>
        public T Invoke() => this.func != null ? this.func.Invoke() : default;
    }

    /// <summary>
    /// Represents a function that takes a single argument and returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
    [Serializable]
    public sealed class BaseFunction<T, R> : IFunction<T, R>
    {
        private Func<T, R> func;

        /// <summary>
        /// Default constructor. Does not initialize the function.
        /// </summary>
        public BaseFunction()
        {
        }

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BaseFunction(Func<T, R> func) => this.func = func;

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T, R}"/> to <see cref="BaseFunction{T, R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BaseFunction<T, R>(Func<T, R> value) => new(value);

        /// <summary>
        /// Sets or replaces the function implementation.
        /// </summary>
        /// <param name="func">The function delegate to assign.</param>
        public void Compose(Func<T, R> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the function with the provided argument.
        /// </summary>
        /// <param name="args">The argument passed to the function.</param>
        /// <returns>The result of the function call.</returns>
        public R Invoke(T args) => this.func.Invoke(args);
    }

    /// <summary>
    /// Represents a function that takes two arguments and returns a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
    [Serializable]
    public sealed class BaseFunction<T1, T2, R> : IFunction<T1, T2, R>
    {
        private Func<T1, T2, R> func;

        /// <summary>
        /// Default constructor. Does not initialize the function.
        /// </summary>
        public BaseFunction()
        {
        }

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BaseFunction(Func<T1, T2, R> func) => this.func = func;

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T1, T2, R}"/> to <see cref="BaseFunction{T1, T2, R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BaseFunction<T1, T2, R>(Func<T1, T2, R> value) => new(value);

        /// <summary>
        /// Sets or replaces the function implementation.
        /// </summary>
        /// <param name="func">The function delegate to assign.</param>
        public void Construct(Func<T1, T2, R> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the function with the provided arguments.
        /// </summary>
        /// <param name="arg1">The first input parameter.</param>
        /// <param name="arg2">The second input parameter.</param>
        /// <returns>The result of the function call.</returns>
        public R Invoke(T1 arg1, T2 arg2) => this.func.Invoke(arg1, arg2);
    }
}