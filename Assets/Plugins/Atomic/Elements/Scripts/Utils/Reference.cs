using System;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a serialized reference wrapper for a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value being referenced.</typeparam>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class Reference<T>
    {
        /// <summary>
        /// Gets a reference to the wrapped value.
        /// </summary>
        public ref T Value => ref this.value;
        
#if ODIN_INSPECTOR
        [HideLabel]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T value;

        public Reference(T value = default) => this.value = value;
    }
}