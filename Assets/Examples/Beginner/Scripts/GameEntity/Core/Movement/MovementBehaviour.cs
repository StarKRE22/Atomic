using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class MovementBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private IVariable<Vector3> _position;
        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;

        public void OnSpawn(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMoveSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            MovementUseCase.MovementStep(
                _position.Value,
                _moveDirection.Value,
                _moveSpeed.Value,
                deltaTime,
                out float3 next
            );
            _position.Value = next;
        }
    }
}