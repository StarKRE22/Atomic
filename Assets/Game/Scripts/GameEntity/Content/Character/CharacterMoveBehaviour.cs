using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public class CharacterMoveBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private IValue<Vector3> _moveDirection;
        private IVariable<Vector3> _rotationDirection;
        
        public void OnSpawn(IGameEntity entity)
        {
            _moveDirection = entity.GetMoveDirection();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _rotationDirection.Value = _moveDirection.Value;
        }
    }
}