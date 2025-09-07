using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public class CharacterMoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedUpdate
    {
        private IValue<Vector3> _moveDirection;
        private IVariable<Vector3> _rotationDirection;

        public void Init(IGameEntity entity)
        {
            _moveDirection = entity.GetMoveDirection();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction != Vector3.zero) 
                _rotationDirection.Value = direction;
        }
    }
}