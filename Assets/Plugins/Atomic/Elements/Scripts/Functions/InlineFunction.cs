using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless function returning a value of type <typeparamref name="R"/>.
    /// </summary>
    /// <typeparam name="R">The return type of the function.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class InlineFunction<R> : IFunction<R>
    {
        private readonly Func<R> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public InlineFunction(Func<R> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T}"/> to <see cref="InlineFunction{T}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlineFunction<R>(Func<R> value) => new(value);

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public R Value => this.func.Invoke();
        
#if ODIN_INSPECTOR
        [Button]
#endif

        /// <summary>
        /// Invokes the function and returns its result.
        /// </summary>
        public R Invoke() => this.func.Invoke();
        
        public override string ToString() => this.func.Method.Name;
    }

    /// <summary>
    /// Represents a function that takes a single argument and returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class InlineFunction<T, R> : IFunction<T, R>
    {
        private readonly Func<T, R> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public InlineFunction(Func<T, R> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T, R}"/> to <see cref="InlineFunction{T,R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlineFunction<T, R>(Func<T, R> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the function with the provided argument.
        /// </summary>
        /// <param name="args">The argument passed to the function.</param>
        /// <returns>The result of the function call.</returns>
        public R Invoke(T args) => this.func.Invoke(args);
        
        public override string ToString() => this.func.Method.Name;
    }

    /// <summary>
    /// Represents a function that takes two arguments and returns a value.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="R">The return type.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class InlineFunction<T1, T2, R> : IFunction<T1, T2, R>
    {
        private readonly Func<T1, T2, R> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public InlineFunction(Func<T1, T2, R> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T1, T2, R}"/> to <see cref="InlineFunction{T1,T2,R}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlineFunction<T1, T2, R>(Func<T1, T2, R> value) => new(value);

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
        
        public override string ToString() => this.func.Method.Name;
    }
}