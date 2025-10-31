// using NUnit.Framework;
// using Unity.Mathematics;
//
// namespace BeginnerGame
// {
//     public sealed class RotateUseCaseTests
//     {
//         [Test]
//         public void RotationStep_ZeroDirection_RotationUnchanged()
//         {
//             quaternion start = quaternion.identity;
//             float3 dir = float3.zero;
//             float speed = 90f;
//             float deltaTime = 1f;
//
//             RotateUseCase.RotationStep(start, dir, speed, deltaTime, out quaternion result);
//
//             Assert.That(result, Is.EqualTo(start));
//         }
//
//         [Test]
//         public void RotationStep_FacingSameDirection_NoRotation()
//         {
//             quaternion start = quaternion.LookRotation(math.forward(), math.up());
//             float3 dir = math.forward();
//             float speed = 90f;
//             float deltaTime = 1f;
//
//             RotateUseCase.RotationStep(start, dir, speed, deltaTime, out quaternion result);
//
//             Assert.That(result, Is.EqualTo(start));
//         }
//
//         [Test]
//         public void RotationStep_RotatesTowardTargetWithinSpeedLimit()
//         {
//             quaternion start = quaternion.LookRotation(math.forward(), math.up());
//             float3 dir = math.right(); // 90° difference
//             float speed = 45f; // 45° per second
//             float deltaTime = 1f;
//
//             RotateUseCase.RotationStep(start, dir, speed, deltaTime, out quaternion result);
//
//             // Expect rotation to move exactly 45° toward the target
//             RotateUseCase.Angle(start, result, out float angleMoved);
//             Assert.That(angleMoved, Is.EqualTo(45f).Within(1e-3f));
//         }
//
//         [Test]
//         public void RotationStep_RotatesDirectlyIfMaxStepExceedsAngle()
//         {
//             quaternion start = quaternion.LookRotation(math.forward(), math.up());
//             float3 dir = math.right(); // 90° difference
//             float speed = 180f; // 180° per second
//             float deltaTime = 1f;
//
//             RotateUseCase.RotationStep(start, dir, speed, deltaTime, out quaternion result);
//
//             // Should be fully aligned with the target
//             quaternion expected = quaternion.LookRotation(math.normalize(dir), math.up());
//             RotateUseCase.Angle(result, expected, out float angleToTarget);
//             Assert.That(angleToTarget, Is.LessThan(0.5f));
//         }
//     }
// }