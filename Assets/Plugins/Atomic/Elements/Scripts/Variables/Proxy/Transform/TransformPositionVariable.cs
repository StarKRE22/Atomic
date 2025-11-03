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
        private readonly Transform _transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformPositionVariable"/> class.
        /// </summary>
        /// <param name="transform">The transform whose position is exposed.</param>
        public TransformPositionVariable(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            _transform = transform;
        }

        /// <summary>
        /// Gets or sets the current position of the target transform.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Vector3 Value
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        /// <summary>
        /// Returns the current position of the transform.
        /// </summary>
        public Vector3 Invoke() => _transform.position;
    }
}
#endif