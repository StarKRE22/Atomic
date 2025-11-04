#if UNITY_5_3_OR_NEWER
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a serialized, immutable (read-only) constant value as a ScriptableObject.
    /// Implements <see cref="IValue{T}"/> and supports implicit conversions.
    /// Useful for sharing constant data across multiple scenes or objects in Unity.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped constant value.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Values/ScriptableConst.md")]
    public abstract class ScriptableConst<T> : ScriptableObject, IValue<T>
    {
        /// <summary>
        /// Gets the wrapped constant value.
        /// </summary>
        public T Value => this.value;

#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private T value;

        /// <summary>
        /// Invokes the constant value as a function (from <see cref="IValue{T}"/>).
        /// </summary>
        /// <returns>The wrapped constant value.</returns>
        public T Invoke() => this.value;

        /// <summary>
        /// Returns a string that represents the wrapped constant value.
        /// </summary>
        /// <returns>A string representation of the value.</returns>
        public override string ToString() => this.value?.ToString();
    }
}
#endif