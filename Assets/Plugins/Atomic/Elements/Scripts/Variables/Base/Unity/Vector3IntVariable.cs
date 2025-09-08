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
    /// A simple serialized variable for <see cref="Vector3Int"/> values.
    /// Implements <see cref="IVariable{Vector3Int}"/>.
    /// </summary>
    [Serializable]
    public sealed class Vector3IntVariable : IVariable<Vector3Int>
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private Vector3Int value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Vector3Int Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// Initializes a new instance with the default value (0,0,0).
        /// </summary>
        public Vector3IntVariable() => this.value = Vector3Int.zero;

        /// <summary>
        /// Initializes a new instance with a specified value.
        /// </summary>
        /// <param name="value">The initial Vector3Int value.</param>
        public Vector3IntVariable(Vector3Int value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public Vector3Int Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="Vector3Int"/> to a <see cref="Vector3IntVariable"/>.
        /// </summary>
        public static implicit operator Vector3IntVariable(Vector3Int value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif