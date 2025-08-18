using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class PositionViewBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private readonly Transform _transform;
        private IReactiveValue<Vector3> _position;

        public PositionViewBehaviour(Transform transform)
        {
            _transform = transform;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _position.Observe(this.OnPositionChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _position.Unsubscribe(this.OnPositionChanged);
        }

        private void OnPositionChanged(Vector3 position)
        {
            _transform.position = position;
        }
    }
}