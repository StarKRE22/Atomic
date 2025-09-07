using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class RotationBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
        private IVariable<Quaternion> _rotation;
        private IValue<float> _rotationSpeed;
        private IValue<Vector3> _rotationDirection;
        private IFunction<bool> _rotationCondition;

        public void Init(IGameEntity entity)
        {
            _rotation = entity.GetRotation();
            _rotationSpeed = entity.GetRotationSpeed();
            _rotationDirection = entity.GetRotationDirection();
            _rotationCondition = entity.GetRotationCondition();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            if (!_rotationCondition.Invoke())
                return;
            
            RotationUseCase.RotationStep(
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