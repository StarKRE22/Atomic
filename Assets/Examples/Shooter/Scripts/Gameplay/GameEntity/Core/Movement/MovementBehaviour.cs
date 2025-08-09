using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class MovementBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private IVariable<Vector3> _position;
        
        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        private IFunction<bool> _moveCondition;
        private IEvent<Vector3> _moveEvent;

        public void OnSpawn(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMovementSpeed();
            _moveDirection = entity.GetMovementDirection();
            _moveCondition = entity.GetMovementCondition();
            _moveEvent = entity.GetMovementEvent();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction == Vector3.zero || !_moveCondition.Invoke())
                return;

            MovementUseCase.MovementStep(
                _position.Value,
                direction,
                _moveSpeed.Value,
                deltaTime,
                out float3 next
            );
            _position.Value = next;
            _moveEvent.Invoke(direction);
        }
    }
}