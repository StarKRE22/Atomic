// using Atomic.Elements;
// using NUnit.Framework;
// using UnityEngine;
//
// namespace BeginnerGame
// {
//     public class CharacterMoveBehaviourTests
//     {
//         private static void AssertVec3Equal(Vector3 a, Vector3 b, float tol = 1e-6f)
//         {
//             Assert.That(a.x, Is.EqualTo(b.x).Within(tol));
//             Assert.That(a.y, Is.EqualTo(b.y).Within(tol));
//             Assert.That(a.z, Is.EqualTo(b.z).Within(tol));
//         }
//
//         [Test]
//         public void OnFixedUpdate_SetsRotation_WhenMoveIsNonZero()
//         {
//             var entity = new TestActor();
//             entity.AddMoveDirection(new ReactiveVariable<Vector3>(new Vector3(1, 0, 0)));
//             entity.AddRotationDirection(new ReactiveVariable<Vector3>(new Vector3(0, 0, 1)));
//             entity.AddBehaviour(new CharacterMoveBehaviour());
//
//             entity.Enable();
//             entity.FixedTick(deltaTime: 0.02f);
//
//             AssertVec3Equal(entity.GetRotationDirection().Value, new Vector3(1, 0, 0));
//         }
//
//         [Test]
//         public void OnFixedUpdate_DoesNotChangeRotation_WhenMoveIsZero()
//         {
//             var entity = new TestActor();
//             entity.AddMoveDirection(new ReactiveVariable<Vector3>(Vector3.zero));
//             entity.AddRotationDirection(new ReactiveVariable<Vector3>(new Vector3(0, 1, 0)));
//             entity.AddBehaviour(new CharacterMoveBehaviour());
//
//             entity.Enable();
//             entity.FixedTick(deltaTime: 0.02f);
//
//             AssertVec3Equal(entity.GetRotationDirection().Value, new Vector3(0, 1, 0));
//         }
//
//         [Test]
//         public void OnFixedUpdate_UsesLatestMoveValue_AfterSpawn()
//         {
//             var entity = new TestActor();
//             var moveVar = new ReactiveVariable<Vector3>(new Vector3(0, 0, 1));
//             entity.AddMoveDirection(moveVar);
//             entity.AddRotationDirection(new ReactiveVariable<Vector3>(Vector3.zero));
//             entity.AddBehaviour(new CharacterMoveBehaviour());
//
//             entity.Enable();
//
//             // First tick
//             entity.FixedTick(deltaTime: 0.02f);
//             AssertVec3Equal(entity.GetRotationDirection().Value, new Vector3(0, 0, 1));
//
//             // Change move direction before second tick
//             moveVar.Value = new Vector3(-1, 0, 0);
//
//             entity.FixedTick(deltaTime: 0.02f);
//             AssertVec3Equal(entity.GetRotationDirection().Value, new Vector3(-1, 0, 0));
//         }
//
//         [Test]
//         public void OnFixedUpdate_TinyNonZeroVector_IsConsideredNonZero_ByDesign()
//         {
//             var tiny = new Vector3(1e-8f, 0, 0);
//             var entity = new TestActor();
//             entity.AddMoveDirection(new ReactiveVariable<Vector3>(tiny));
//             entity.AddRotationDirection(new ReactiveVariable<Vector3>(Vector3.zero));
//             entity.AddBehaviour(new CharacterMoveBehaviour());
//
//             entity.Enable();
//             entity.FixedTick(deltaTime: 0.02f);
//
//             AssertVec3Equal(entity.GetRotationDirection().Value, tiny);
//         }
//
//         [Test]
//         public void OnFixedUpdate_Idempotent_WhenMoveStaysSameNonZero()
//         {
//             var moveDir = new Vector3(0, 0, 1);
//             var entity = new TestActor();
//             entity.AddMoveDirection(new ReactiveVariable<Vector3>(moveDir));
//             entity.AddRotationDirection(new ReactiveVariable<Vector3>(Vector3.left));
//             entity.AddBehaviour(new CharacterMoveBehaviour());
//
//             entity.Enable();
//             entity.FixedTick(deltaTime: 0.02f);
//             entity.FixedTick(deltaTime: 0.02f);
//
//             AssertVec3Equal(entity.GetRotationDirection().Value, moveDir);
//         }
//     }
// }