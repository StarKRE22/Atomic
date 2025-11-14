using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class PositionViewBehaviour : IEntityInit<IUnit>, IEntityDispose
    {
        [SerializeField]
        private Transform _transform;
        
        private IReactiveValue<Vector3> _position;

        public PositionViewBehaviour(Transform transform)
        {
            _transform = transform;
        }

        public void Init(IUnit entity)
        {
            _position = entity.GetPosition();
            _position.Observe(this.OnPositionChanged);
        }

        public void Dispose(IEntity entity)
        {
            _position.Unsubscribe(this.OnPositionChanged);
        }

        private void OnPositionChanged(Vector3 position)
        {
            _transform.position = position;
        }
    }
}