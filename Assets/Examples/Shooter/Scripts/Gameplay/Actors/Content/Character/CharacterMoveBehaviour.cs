using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterMoveBehaviour : IEntityInit<IActor>, IEntityFixedTick
    {
        private IValue<Vector3> _moveDirection;
        private IVariable<Vector3> _rotationDirection;

        public void Init(IActor entity)
        {
            _moveDirection = entity.GetMovementDirection();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction != Vector3.zero) 
                _rotationDirection.Value = direction;
        }
    }
}