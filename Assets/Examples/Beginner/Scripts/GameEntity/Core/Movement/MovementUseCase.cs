using Unity.Burst;
using Unity.Mathematics;

namespace BeginnerGame
{
    [BurstCompile]
    public static class MoveUseCase
    {
        [BurstCompile]
        public static void MovementStep(
            in float3 position,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out float3 result
        ) => result = position + speed * deltaTime * direction;
    }
}