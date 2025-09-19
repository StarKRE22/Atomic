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
    /// A simple serialized variable that stores a <see cref="Quaternion"/>.
    /// Implements <see cref="IVariable{Quaternion}"/>.
    /// </summary>
    [Serializable]
    public sealed class QuaternionVariable : IVariable<Quaternion>
    {
        /// <summary>
        /// Gets or sets the current <see cref="Quaternion"/> value.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Quaternion Value
        {
            get => this.value;
            set => this.value = value;
        }

        /// <summary>
        /// The serialized <see cref="Quaternion"/> value.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private Quaternion value;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionVariable"/> class with the default value.
        /// </summary>
        public QuaternionVariable() => this.value = Quaternion.identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuaternionVariable"/> class with a specified value.
        /// </summary>
        /// <param name="value">The initial <see cref="Quaternion"/> value.</param>
        public QuaternionVariable(Quaternion value) => this.value = value;

        /// <summary>
        /// Returns the current value.
        /// </summary>
        public Quaternion Invoke() => this.value;

        /// <summary>
        /// Implicitly converts a <see cref="Quaternion"/> to a <see cref="QuaternionVariable"/>.
        /// </summary>
        /// <param name="value">The value to wrap.</param>
        public static implicit operator QuaternionVariable(Quaternion value) => new(value);

        /// <summary>
        /// Returns a string representation of the current value.
        /// </summary>
        public override string ToString() => this.value.ToString();
    }
}
#endif
