using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class RotationBehaviour : IEntityInit<IGameEntity>, IEntityFixedUpdate
    {
        private IVariable<Quaternion> _rotation;
        private IValue<float> _rotationSpeed;
        private IValue<Vector3> _rotationDirection;

        public void Init(IGameEntity entity)
        {
            _rotation = entity.GetRotation();
            _rotationSpeed = entity.GetRotationSpeed();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            RotateUseCase.RotationStep(
                _rotation.Value,
                _rotationDirection.Value,
                _rotationSpeed.Value,
                deltaTime,
                out quaternion next
            );
            
            _rotation.Value = next;
        }
    }
}