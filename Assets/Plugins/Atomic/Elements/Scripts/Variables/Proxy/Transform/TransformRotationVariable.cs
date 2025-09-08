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
    /// A variable that exposes the rotation of a <see cref="Transform"/> as an <see cref="IVariable{Quaternion}"/>.
    /// Getting or setting the value reads/writes <see cref="Transform.rotation"/>.
    /// </summary>
    [Serializable]
    public sealed class TransformRotationVariable : IVariable<Quaternion>
    {
        private readonly Transform target;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformRotationVariable"/> class.
        /// </summary>
        /// <param name="target">The transform whose rotation is exposed.</param>
        public TransformRotationVariable(Transform target)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Gets or sets the current rotation of the target transform.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Quaternion Value
        {
            get => this.target.rotation;
            set => this.target.rotation = value;
        }

        /// <summary>
        /// Returns the current rotation of the transform.
        /// </summary>
        public Quaternion Invoke() => this.target.rotation;
    }
}
#endif