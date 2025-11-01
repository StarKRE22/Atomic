using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Provides a proxy variable that delegates its value to an external getter and setter.
    /// Allows wrapping access to any existing data source as a read-write variable.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    [Serializable]
    public class InlineVariable<T> : IVariable<T>
    {
        /// <summary>
        /// Gets or sets the value via the delegated getter and setter.
        /// Returns default if the getter is not assigned.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public T Value
        {
            get => this.getter.Invoke();
            set => this.setter.Invoke(value);
        }

        private readonly Func<T> getter;
        private readonly Action<T> setter;

        /// <summary>
        /// Creates a <see cref="InlineVariable{T}"/> with the specified getter and setter.
        /// </summary>
        /// <param name="getter">Function to retrieve the value.</param>
        /// <param name="setter">Action to update the value.</param>
        public InlineVariable(Func<T> getter, Action<T> setter)
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
        }
        
        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value?.ToString();

        /// <summary>
        /// Begins building a <see cref="InlineVariable{T}"/> using a fluent builder pattern.
        /// </summary>
        public static Builder StartBuild() => new();

        /// <summary>
        /// Fluent builder for constructing <see cref="InlineVariable{T}"/> instances.
        /// </summary>
        public struct Builder
        {
            private Func<T> _getter;
            private Action<T> _setter;

            /// <summary>
            /// Assigns the getter function.
            /// </summary>
            /// <param name="getter">Function to retrieve the value.</param>
            public Builder WithGetter(Func<T> getter)
            {
                _getter = getter ?? throw new ArgumentNullException(nameof(getter));
                return this;
            }

            /// <summary>
            /// Assigns the setter action.
            /// </summary>
            /// <param name="setter">Action to update the value.</param>
            public Builder WithSetter(Action<T> setter)
            {
                _setter = setter ?? throw new ArgumentNullException(nameof(setter));
                return this;
            }

            /// <summary>
            /// Builds and returns the configured <see cref="InlineVariable{T}"/>.
            /// </summary>
            /// <exception cref="InvalidOperationException">Thrown if getter or setter is not provided.</exception>
            public InlineVariable<T> Build()
            {
                if (_getter == null) throw new InvalidOperationException("Getter must be provided.");
                if (_setter == null) throw new InvalidOperationException("Setter must be provided.");
                return new InlineVariable<T>(_getter, _setter);
            }
        }
    }
}