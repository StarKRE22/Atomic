using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class MovementBehaviour : IEntityInit, IEntityFixedTick
    {
        private Transform _transform;
        private IValue<Vector3> _movementDirection;
        private IValue<float> _movementSpeed;

        public void Init(IEntity entity)
        {
            _transform = entity.GetTransform();
            _movementDirection = entity.GetMovementDirection();
            _movementSpeed = entity.GetMovementSpeed();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            _transform.position += _movementDirection.Value * (_movementSpeed.Value * deltaTime);
        }
    }
}