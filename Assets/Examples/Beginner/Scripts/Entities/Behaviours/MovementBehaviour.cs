using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Moves the entity's transform each physics frame according to its movement direction and speed.
    /// </summary>
    /// <remarks>
    /// This behaviour applies a simple kinematic movement without physics forces.
    /// It updates the entity's <see cref="Transform"/> position in every <c>FixedUpdate</c> step,
    /// multiplying the normalized direction by the movement speed and delta time.
    /// </remarks>
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