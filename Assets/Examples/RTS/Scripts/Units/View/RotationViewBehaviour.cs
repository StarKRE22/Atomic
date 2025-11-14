using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class RotationViewBehaviour : IEntityInit<IUnit>, IEntityDispose
    {
        [SerializeField]
        private Transform _transform;
        
        private IReactiveValue<Quaternion> _rotation;

        public RotationViewBehaviour(Transform transform)
        {
            _transform = transform;
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        public void Init(IUnit entity)
        {
            _rotation = entity.GetRotation();
            _rotation.Observe(this.OnRotationChanged);
        }

        public void Dispose(IEntity entity)
        {
            _rotation.Unsubscribe(this.OnRotationChanged);
        }
    }
}