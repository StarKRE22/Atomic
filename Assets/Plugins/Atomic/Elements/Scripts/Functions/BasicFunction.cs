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
    public class BasicFunction<T> : IValue<T>
    {
        private readonly Func<T> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BasicFunction(Func<T> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T}"/> to <see cref="BasicFunction{T}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BasicFunction<T>(Func<T> value) => new(value);

        /// <summary>
        /// Gets the result of the function.
        /// </summary>
        public T Value => this.func.Invoke();

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the function and returns its result.
        /// </summary>
        public T Invoke() => this.func.Invoke();
    }

    /// <summary>
    /// Represents a function that takes a single argument and returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
    [Serializable]
    public class BasicFunction<T, R> : IFunction<T, R>
    {
        private readonly Func<T, R> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BasicFunction(Func<T, R> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T, R}"/> to <see cref="BasicFunction{T,R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BasicFunction<T, R>(Func<T, R> value) => new(value);

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
    public class BasicFunction<T1, T2, R> : IFunction<T1, T2, R>
    {
        private readonly Func<T1, T2, R> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public BasicFunction(Func<T1, T2, R> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T1, T2, R}"/> to <see cref="BasicFunction{T1,T2,R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator BasicFunction<T1, T2, R>(Func<T1, T2, R> value) => new(value);

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