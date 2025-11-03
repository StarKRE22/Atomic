#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a variable that gets or sets the parent of a <see cref="Transform"/>.
    /// </summary>
    public class TransformParentVariable : IVariable<Transform>
    {
        /// <summary>
        /// Gets or sets the parent of the underlying <see cref="Transform"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the underlying <see cref="Transform"/> is null during construction.
        /// </exception>
        public Transform Value
        {
            get => _transform.parent;
            set => _transform.parent = value;
        }

        private readonly Transform _transform;

        /// <summary>
        /// Initializes a new instance of the <see cref="TransformParentVariable"/> class
        /// with the specified <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">The <see cref="Transform"/> whose parent will be managed.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="transform"/> is null.</exception>
        public TransformParentVariable(Transform transform)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));

            _transform = transform;
        }
    }
}
#endif