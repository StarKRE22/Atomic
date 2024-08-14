using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class RotationBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private IVariable<quaternion> _rotation;
        private IValue<float> _angularSpeed;
        private IValue<float3> _moveDirection;

        public void Init(IEntity entity)
        {
            _rotation = entity.GetRotation();
            _angularSpeed = entity.GetAngularSpeed();
            _moveDirection = entity.GetMoveDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            RotationFunctions.RotateStep(
                _rotation.Value,
                _moveDirection.Value,
                deltaTime,
                _angularSpeed.Value,
                out quaternion newRotation
            );
            _rotation.Value = newRotation;
        }
    }
}

