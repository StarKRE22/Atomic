using Atomic.Elements;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class RigidbodyMovementBehaviour : IGameEntityInit, IGameEntityFixedTick
    {
        private const float SWEEP_TEST_FACTOR = 2;
        
        private Rigidbody _rigidbody;
        private IValue<float> _moveSpeed;
        private IValue<Vector3> _moveDirection;
        private IFunction<bool> _moveCondition;
        private IEvent<Vector3> _moveEvent;

        public void Init(IGameEntity entity)
        {
            _rigidbody = entity.GetRigidbody();
            _moveSpeed = entity.GetMovementSpeed();
            _moveDirection = entity.GetMovementDirection();
            _moveCondition = entity.GetMovementCondition();
            _moveEvent = entity.GetMovementEvent();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
            Vector3 direction = _moveDirection.Value;
            if (direction == Vector3.zero || !_moveCondition.Invoke())
                return;

            float moveStep = _moveSpeed.Value * deltaTime;
            if (_rigidbody.SweepTest(direction, out _, moveStep * SWEEP_TEST_FACTOR))
                return;

            _rigidbody.MovePosition(_rigidbody.position + direction * moveStep);
            _moveEvent.Invoke(direction);
        }
    }
}