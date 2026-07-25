using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TransformViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        [SerializeField]
        private TransformAgent _transform;

        private IReactiveValue<Vector3> _position;
        private IReactiveValue<Quaternion> _rotation;

        public void Init(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            
            _transform.SetPositionAndRotation(_position.Value, _rotation.Value);
            
            _rotation.OnEvent += this.OnRotationChanged;
            _position.Subscribe(this.OnPositionChanged);
        }

        public void Dispose(IEntity entity)
        {
            _rotation.Unsubscribe(this.OnRotationChanged);
            _position.Unsubscribe(this.OnPositionChanged);
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            _transform.SetPositionAndRotation(_position.Value, rotation);
        }

        private void OnPositionChanged(Vector3 position)
        {
            _transform.SetPositionAndRotation(position, _rotation.Value);
        }
    }
}