using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterMoveBehaviour : IGameEntityInit, IGameEntityFixedTick
    {
        private IValue<Vector3> _moveDirection;
        private IVariable<Vector3> _rotationDirection;

        public void Init(IGameEntity entity)
        {
            _moveDirection = entity.GetMovementDirection();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction != Vector3.zero) 
                _rotationDirection.Value = direction;
        }
    }
}