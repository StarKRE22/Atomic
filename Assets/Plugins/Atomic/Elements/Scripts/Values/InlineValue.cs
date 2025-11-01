using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a proxy wrapper that returns a value of type <typeparamref name="R"/>.
    /// </summary>
    /// <typeparam name="R">The return type of the function.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class InlineValue<T> : IValue<T>
    {
        private readonly Func<T> func;

        /// <summary>
        /// Initializes the function with the provided delegate.
        /// </summary>
        /// <param name="func">The function delegate.</param>
        public InlineValue(Func<T> func) => this.func = func ?? throw new ArgumentNullException(nameof(func));

        /// <summary>
        /// Implicit conversion from a <see cref="Func{T}"/> to <see cref="InlineFunction{T}"/>.
        /// </summary>
        /// <param name="value">The function delegate.</param>
        public static implicit operator InlineValue<T>(Func<T> value) => new(value);

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public T Value => this.func.Invoke();
        
#if ODIN_INSPECTOR
        [Button]
#endif

        /// <summary>
        /// Invokes the function and returns its result.
        /// </summary>
        public T Invoke() => this.func.Invoke();
        
        public override string ToString() => this.func.Method.Name;
    }
}