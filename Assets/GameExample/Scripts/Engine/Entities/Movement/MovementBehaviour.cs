using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;

namespace GameExample.Engine
{
    public sealed class MovementBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private IVariable<float3> _position;
        private IValue<float> _moveSpeed;
        private IValue<float3> _moveDirection;

        public void Init(IEntity entity)
        {
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMoveSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            MovementFunctions.MoveStep(
                _position.Value,
                _moveDirection.Value,
                _moveSpeed.Value,
                deltaTime,
                out float3 newPosition
            );
            _position.Value = newPosition;
        }
    }
}



