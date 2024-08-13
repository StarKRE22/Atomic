using Unity.Burst;
using Unity.Mathematics;

namespace GameExample.Engine
{
    [BurstCompile]
    public static class MovementFunctions
    {
        [BurstCompile]
        public static void MoveStep(
            in float3 position,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out float3 newPosition
        )
        {
            newPosition = position + speed * deltaTime * direction;
        }
    }
}