using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class KinematicMovementBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
        private const float MAX_DISTANCE_COEFFICIENT = 2;
        
        private Rigidbody _rigidbody;
        private IVariable<Vector3> _position;

        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        private IFunction<bool> _moveCondition;
        private IEvent<Vector3> _moveEvent;

        public void Init(IGameEntity entity)
        {
            _rigidbody = entity.GetRigidbody();
            _position = entity.GetPosition();
            _moveSpeed = entity.GetMovementSpeed();
            _moveDirection = entity.GetMovementDirection();
            _moveCondition = entity.GetMovementCondition();
            _moveEvent = entity.GetMovementEvent();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction == Vector3.zero || !_moveCondition.Invoke())
                return;

            float moveStep = _moveSpeed.Value * deltaTime;
            float maxDistance = moveStep * MAX_DISTANCE_COEFFICIENT;
            if (_rigidbody.SweepTest(direction, out _, maxDistance))
                return;

            _position.Value += direction * moveStep;
            _moveEvent.Invoke(direction);
        }
    }
}