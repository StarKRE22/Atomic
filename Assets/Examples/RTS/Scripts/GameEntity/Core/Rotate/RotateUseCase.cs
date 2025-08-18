using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class RotateUseCase
    {
        public static void RotateStep(in IGameEntity entity, in Vector3 direction, in float deltaTime)
        {
            if (direction == Vector3.zero)
                return;

            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            RotateStep(entity, targetRotation, deltaTime);
        }

        public static void RotateStep(in IGameEntity entity, in Quaternion target, in float deltaTime)
        {
            float speed = entity.GetRotationSpeed().Value * deltaTime;
            IReactiveVariable<Quaternion> rotation = entity.GetRotation();
            rotation.Value = Quaternion.Lerp(rotation.Value, target, speed);
        }
    }
}