using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Provides a setter interface to a specified source using an <see cref="Action{T}"/> delegate.
    /// </summary>
    /// <typeparam name="T">The type of value to be set.</typeparam>
    [Serializable]
    public class InlineSetter<T> : ISetter<T>
    {
        /// <summary>
        /// Sets the value by invoking the composed action.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public T Value
        {
            set => this.action?.Invoke(value);
        }

        private readonly Action<T> action;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSetter{T}"/> class with a specified action.
        /// </summary>
        /// <param name="action">The action to invoke when the value is set.</param>
        public InlineSetter(Action<T> action) => this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T}"/> to <see cref="InlineSetter{T}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineSetter<T>(Action<T> value) => new(value);
        
        public override string ToString() => this.action.Method.Name;
        
        /// <summary>
        /// [Editor Only] Invokes the setter manually in the Unity Editor.
        /// </summary>
        /// <param name="value">The value to set through the composed action.</param>
#if UNITY_EDITOR  
#if ODIN_INSPECTOR
        [Button("Set Value")]
#endif
        private void Editor_SetValue(T value) => this.action?.Invoke(value);
#endif
    }
}