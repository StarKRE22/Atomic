// using Atomic.Contexts;
// using Atomic.Entities;
// using ShooterGame.Gameplay;
// using UnityEngine;
//
// namespace SampleGame
// {
//     public sealed class CameraFollowController : IContextInit<IPlayerContext>, IContextLateUpdate
//     {
//         private readonly Vector3 _cameraOffset;
//         private IEntity _character;
//         private Transform _camera;
//         
//         public CameraFollowController(Vector3 cameraOffset)
//         {
//             _cameraOffset = cameraOffset;
//         }
//
//         public void Init(IPlayerContext context)
//         {
//             _character = context.GetCharacter();
//             _camera = context.GetCamera().transform;
//         }
//
//         public void OnLateUpdate(IContext context, float deltaTime)
//         {
//             _camera.position = _character.GetTransform().position + _cameraOffset;
//         }
//     }
// }