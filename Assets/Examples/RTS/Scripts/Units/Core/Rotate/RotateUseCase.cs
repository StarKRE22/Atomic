using Atomic.Elements;
using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace RTSGame
{
    [BurstCompile]
    public static class RotateUseCase
    {
        private const float EPS = 1e-4f;
        
        public static void RotateStep(IUnit entity, Vector3 direction, float deltaTime)
        {
            IReactiveVariable<Quaternion> rotation = entity.GetRotation();
            RotateStep(
                rotation.Value,
                direction,
                entity.GetRotationSpeed().Value,
                deltaTime,
                out quaternion next
            );
            
            rotation.Value = next;
        }
        
        [BurstCompile]
        public static void RotateStep(
            in quaternion current,
            in float3 direction,
            in float speedDeg,
            in float deltaTime,
            out quaternion result
        )
        {
            if (math.lengthsq(direction) < EPS)
            {
                result = current;
                return;
            }

            quaternion target = quaternion.LookRotation(math.normalize(direction), math.up());
            Angle(in current, in target, out float angle);

            float maxStep = speedDeg * deltaTime;
            if (angle <= maxStep)
            {
                result = target;
            }
            else
            {
                float t = maxStep / angle;
                result = math.slerp(current, target, t);
            }
        }

        [BurstCompile]
        public static void Angle(in quaternion current, in quaternion target, out float angle)
        {
            float dot = math.clamp(math.dot(current.value, target.value), -1f, 1f);
            dot = math.abs(dot);
            angle = math.degrees(math.acos(dot)) * 2f;
        }
    }
}