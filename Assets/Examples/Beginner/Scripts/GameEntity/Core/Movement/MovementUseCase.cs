using Unity.Burst;
using Unity.Mathematics;

namespace BeginnerGame
{
    [BurstCompile]
    public static class MovementUseCase
    {
        public static void MovementStep(
            in float3 position,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out float3 result
        ) => result = position + speed * deltaTime * direction;
    }
}