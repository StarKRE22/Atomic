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
    /// A variable that exposes the local scale of a <see cref="Transform"/> as an <see cref="IVariable{Vector3}"/>.
    /// Getting or setting the value reads/writes <see cref="Transform.localScale"/>.
    /// </summary>
    [Serializable]
    public sealed class TransformScaleVariable : IVariable<Vector3>
    {
        private readonly Transform _transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformScaleVariable"/> class.
        /// </summary>
        /// <param name="target">The transform whose scale is exposed.</param>
        public TransformScaleVariable(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            _transform = transform;
        }

        /// <summary>
        /// Gets or sets the current local scale of the target transform.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public Vector3 Value
        {
            get => _transform.localScale;
            set => _transform.localScale = value;
        }

        /// <summary>
        /// Returns the current local scale of the transform.
        /// </summary>
        public Vector3 Invoke() => _transform.localScale;
    }
}
#endif