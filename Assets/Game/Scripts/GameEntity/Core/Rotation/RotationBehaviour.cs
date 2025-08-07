using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class RotationBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Transform _transform;
        private IValue<float> _rotationSpeed;
        private IValue<Vector3> _rotationDirection;

        public void OnSpawn(IGameEntity entity)
        {
            _transform = entity.GetTransform();
            _rotationSpeed = entity.GetRotationSpeed();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            RotationUseCase.RotateStep(
                _transform.rotation,
                _rotationDirection.Value,
                _rotationSpeed.Value,
                in deltaTime,
                out quaternion result
            );

            _transform.rotation = result;
        }
    }
}