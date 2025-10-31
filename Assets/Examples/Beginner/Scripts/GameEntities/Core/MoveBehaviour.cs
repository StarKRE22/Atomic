using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public class MoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
        private IValue<Vector3> _moveDirection;
        private IVariable<Vector3> _rotationDirection;

        public void Init(IGameEntity entity)
        {
            _moveDirection = entity.GetMoveDirection();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction != Vector3.zero) 
                _rotationDirection.Value = direction;
        }
        
        private IVariable<Vector3> _position;
        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;

        public void Init(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMoveSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            MoveUseCase.MovementStep(
                _position.Value,
                _moveDirection.Value,
                _moveSpeed.Value,
                deltaTime,
                out float3 next
            );
            _position.Value = next;
        }
        
        [BurstCompile]
        public static void MovementStep(
            in float3 position,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out float3 result
        ) => result = position + speed * deltaTime * direction;
    }
}