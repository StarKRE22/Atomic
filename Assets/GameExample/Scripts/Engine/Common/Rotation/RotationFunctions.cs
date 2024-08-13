using Unity.Burst;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    [BurstCompile]
    public static class RotationFunctions
    {
        [BurstCompile]
        public static void RotateStep(
            in quaternion rotation,
            in float3 direction,
            in float deltaTime,
            in float speed,
            out quaternion newRotation
        )
        {
            if (direction.Equals(float3.zero))
            {
                newRotation = rotation;
                return;
            }
            
            quaternion targetRotation = quaternion.LookRotation(direction, new float3(0, 1, 0));
            float percent = speed * deltaTime;
            newRotation = math.slerp(rotation, targetRotation, percent);
        }
    }
}