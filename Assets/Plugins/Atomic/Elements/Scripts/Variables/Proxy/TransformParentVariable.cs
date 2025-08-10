using System;
using UnityEngine;

namespace Atomic.Elements
{
    public class TransformParentVariable : IVariable<Transform>
    {
        public Transform Value
        {
            get => _transform.parent;
            set => _transform.parent = value;
        }

        private readonly Transform _transform;
        
        public TransformParentVariable(Transform transform) => 
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
    }
}