using Unity.Burst;
using Unity.Mathematics;

namespace ShooterGame.Gameplay
{
    [BurstCompile]
    public static class RotationUseCase
    {
        private const float EPS = 1e-4f;

        [BurstCompile]
        public static void RotationStep(
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