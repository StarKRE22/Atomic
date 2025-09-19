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
    /// A variable that exposes the position of a <see cref="Transform"/> as an <see cref="IVariable{Vector3}"/>.
    /// Getting or setting the value reads/writes <see cref="Transform.position"/>.
    /// </summary>
    [Serializable]
    public sealed class TransformPositionVariable : IVariable<Vector3>
    {
        private readonly Transform target;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformPositionVariable"/> class.
        /// </summary>
        /// <param name="target">The transform whose position is exposed.</param>
        public TransformPositionVariable(Transform target)
        {
            this.target = target ?? throw new ArgumentNullException(nameof(target));
        }

        /// <summary>
        /// Gets or sets the current position of the target transform.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Vector3 Value
        {
            get => this.target.position;
            set => this.target.position = value;
        }

        /// <summary>
        /// Returns the current position of the transform.
        /// </summary>
        public Vector3 Invoke() => this.target.position;
    }
}
#endif