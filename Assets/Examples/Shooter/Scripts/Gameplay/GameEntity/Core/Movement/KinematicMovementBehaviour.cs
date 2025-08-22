using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class KinematicMovementBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Rigidbody _rigidbody;
        private IVariable<Vector3> _position;

        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        private IFunction<bool> _moveCondition;
        private IEvent<Vector3> _moveEvent;

        public void OnSpawn(IGameEntity entity)
        {
            _rigidbody = entity.GetRigidbody();
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

            float magnitude = _moveSpeed.Value * deltaTime;
            if (_rigidbody.SweepTest(direction, out _, magnitude))
                return;

            _position.Value += direction * magnitude;
            _moveEvent.Invoke(direction);
        }
    }
}