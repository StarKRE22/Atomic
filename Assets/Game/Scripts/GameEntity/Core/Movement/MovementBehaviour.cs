using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class MovementBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Transform _transform;
        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        
        public void OnSpawn(IGameEntity entity)
        {
            _transform = entity.GetTransform();
            _moveSpeed = entity.GetMoveSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _transform.position += _moveSpeed.Value * deltaTime * _moveDirection.Value;
        }
    }
}