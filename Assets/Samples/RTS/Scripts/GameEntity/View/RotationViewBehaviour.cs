using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class RotationViewBehaviour : IEntitySpawn<IGameEntity>, IEntityDespawn
    {
        private readonly Transform _transform;
        private IReactiveValue<Quaternion> _rotation;

        public RotationViewBehaviour(Transform transform)
        {
            _transform = transform;
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            _transform.rotation = rotation;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _rotation = entity.GetRotation();
            _rotation.Observe(this.OnRotationChanged);
        }

        public void OnDespawn(IEntity entity)
        {
            _rotation.Unsubscribe(this.OnRotationChanged);
        }
    }
}