using Unity.Burst;
using Unity.Mathematics;

namespace SampleGame
{
    [BurstCompile]
    public static class RotationUseCase
    {
        private static readonly float3 UP = new(0, 1, 0);

        [BurstCompile]
        public static void RotateStep(
            in quaternion current,
            in float3 direction,
            in float speed,
            in float deltaTime,
            out quaternion result
        )
        {
            result = current;
            if (math.lengthsq(direction) < 1e-6f)
                return;

            float3 forward = math.normalize(direction);
            quaternion target = quaternion.LookRotation(forward, UP);

            float maxRadians = speed * deltaTime;
            float angle = math.degrees(math.acos(math.clamp(math.dot(current.value, target.value), -1f, 1f))) * 2f;

            if (angle < 1e-3f)
            {
                result = target;
            }
            else
            {
                float t = math.saturate(maxRadians / math.radians(angle));
                result = math.slerp(current, target, t);
            }
        }
    }
}