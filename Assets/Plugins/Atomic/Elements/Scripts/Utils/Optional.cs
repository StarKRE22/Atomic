using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A Unity-friendly Optional value, serializable and visible in the Inspector.
    /// </summary>
    /// <typeparam name="T">The type of value.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public struct Optional<T>
    {
#if ODIN_INSPECTOR
        [HorizontalGroup]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private bool active;

#if ODIN_INSPECTOR
        [HorizontalGroup]
        [EnableIf(nameof(active))]
#endif
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private T value;

        /// <summary>
        /// Gets or sets the value. Setting also activates the optional.
        /// </summary>
        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                this.active = value != null;
            }
        }

        /// <summary>
        /// Indicates whether the optional is active (contains a value).
        /// </summary>
        public bool Active
        {
            get => this.active;
            set => this.active = value;
        }

        /// <summary>
        /// Tries to get the value. Returns true if active.
        /// </summary>
        public bool TryGetValue(out T value)
        {
            value = this.value;
            return this.active;
        }

        /// <summary>
        /// Implicit conversion from T to Optional&lt;T&gt;.
        /// </summary>
        public static implicit operator Optional<T>(T it) => new() {value = it, active = it != null};

        /// <summary>
        /// Implicit conversion from Optional&lt;T&gt; to T.
        /// </summary>
        public static implicit operator T(Optional<T> it) => it.value;
        
        public static bool operator true(Optional<T> it) => it.active;

        public static bool operator false(Optional<T> it) => !it.active;

        /// <summary>
        /// Returns the value if active, otherwise the default.
        /// </summary>
        public T GetValueOrDefault(T defaultValue) => this.active ? this.value : defaultValue;
    }
}