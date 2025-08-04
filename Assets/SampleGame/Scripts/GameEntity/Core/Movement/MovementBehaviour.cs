using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class MovementBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Transform _transform;
        private IValue<float> _moveSpeed;
        private IValue<float3> _moveDirection;
        
        public void OnSpawn(IGameEntity entity)
        {
            _transform = entity.GetTransform();
            _moveSpeed = entity.GetMoveSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            Vector3 moveStep = _moveSpeed.Value * deltaTime * _moveDirection.Value;
            _transform.position += moveStep;
        }
    }
}