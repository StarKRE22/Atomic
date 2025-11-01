// namespace BeginnerGame
// {
//     using NUnit.Framework;
//     using Unity.Mathematics;
//
//     public sealed class MoveUseCaseTests
//     {
//         [Test]
//         public void MovementStep_ZeroDirection_PositionUnchanged()
//         {
//             float3 start = new float3(1f, 2f, 3f);
//             float3 dir = float3.zero;
//             float speed = 5f;
//             float deltaTime = 1f;
//
//             MoveUseCase.MovementStep(start, dir, speed, deltaTime, out float3 result);
//
//             Assert.AreEqual(start, result);
//         }
//
//         [Test]
//         public void MovementStep_UnitDirection_MovesBySpeedTimesDeltaTime()
//         {
//             float3 start = float3.zero;
//             float3 dir = math.normalize(new float3(1f, 0f, 0f));
//             float speed = 10f;
//             float deltaTime = 0.5f;
//
//             MoveUseCase.MovementStep(start, dir, speed, deltaTime, out float3 result);
//
//             float3 expected = new float3(5f, 0f, 0f); // 10 * 0.5 * (1,0,0)
//             Assert.That(result.x, Is.EqualTo(expected.x).Within(1e-6f));
//             Assert.That(result.y, Is.EqualTo(expected.y).Within(1e-6f));
//             Assert.That(result.z, Is.EqualTo(expected.z).Within(1e-6f));
//         }
//
//         [Test]
//         public void MovementStep_NonUnitDirection_MovesProportionallyToDirectionLength()
//         {
//             float3 start = float3.zero;
//             float3 dir = new float3(2f, 0f, 0f); // длина 2
//             float speed = 1f;
//             float deltaTime = 1f;
//
//             MoveUseCase.MovementStep(start, dir, speed, deltaTime, out float3 result);
//
//             float3 expected = dir * speed * deltaTime;
//             Assert.That(result.x, Is.EqualTo(expected.x).Within(1e-6f));
//             Assert.That(result.y, Is.EqualTo(expected.y).Within(1e-6f));
//             Assert.That(result.z, Is.EqualTo(expected.z).Within(1e-6f));
//         }
//
//         [Test]
//         public void MovementStep_NegativeDirection_MovesInOppositeDirection()
//         {
//             float3 start = float3.zero;
//             float3 dir = new float3(-1f, 0f, 0f);
//             float speed = 3f;
//             float deltaTime = 2f;
//
//             MoveUseCase.MovementStep(start, dir, speed, deltaTime, out float3 result);
//
//             float3 expected = new float3(-6f, 0f, 0f);
//             Assert.That(result, Is.EqualTo(expected));
//         }
//
//         [Test]
//         public void MovementStep_SmallDeltaTime_MovesSmallDistance()
//         {
//             float3 start = float3.zero;
//             float3 dir = math.normalize(new float3(1f, 1f, 0f));
//             float speed = 5f;
//             float deltaTime = 0.1f;
//
//             MoveUseCase.MovementStep(start, dir, speed, deltaTime, out float3 result);
//
//             float3 expected = start + speed * deltaTime * dir;
//             Assert.That(result.x, Is.EqualTo(expected.x).Within(1e-6f));
//             Assert.That(result.y, Is.EqualTo(expected.y).Within(1e-6f));
//             Assert.That(result.z, Is.EqualTo(expected.z).Within(1e-6f));
//         }
//     }
// }