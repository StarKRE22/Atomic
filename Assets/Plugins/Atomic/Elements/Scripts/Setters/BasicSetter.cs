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
    public class BasicSetter<T> : ISetter<T>
    {
        /// <summary>
        /// Sets the value by invoking the composed action.
        /// </summary>
        public T Value
        {
            set => this.action?.Invoke(value);
        }

        private Action<T> action;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSetter{T}"/> class with no initial action.
        /// </summary>
        public BasicSetter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSetter{T}"/> class with a specified action.
        /// </summary>
        /// <param name="action">The action to invoke when the value is set.</param>
        public BasicSetter(Action<T> action) => this.action = action;

        /// <summary>
        /// Assigns or replaces the internal action used to handle value setting.
        /// </summary>
        /// <param name="action">The new action to assign.</param>
        public void Construct(Action<T> action) => this.action = action;

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