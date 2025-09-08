#if UNITY_5_3_OR_NEWER
using System;
using Atomic.Elements;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A simple serialized variable for <see cref="Vector2Int"/> values.
    /// Implements <see cref="IVariable{Vector2Int}"/>.
    /// </summary>
    [Serializable]
    public sealed class Vector2IntVariable : IVariable<Vector2Int>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private Vector2Int value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Vector2Int Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0).
        /// </summary>
        public Vector2IntVariable() => this.value = Vector2Int.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial Vector2Int value.</param>
        public Vector2IntVariable(Vector2Int value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public Vector2Int Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="Vector2Int"/> to a <see cref="Vector2IntVariable"/>.
        /// </summary>
        public static implicit operator Vector2IntVariable(Vector2Int value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif