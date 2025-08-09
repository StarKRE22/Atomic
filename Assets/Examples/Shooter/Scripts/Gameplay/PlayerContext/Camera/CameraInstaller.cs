// using System;
// using Atomic.Contexts;
// using SampleGame;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class CameraInstaller : IContextInstaller<IPlayerContext>
//     {
//         [SerializeField]
//         private Camera _camera;
//
//         [field: SerializeField]
//         private Vector3 _cameraOffset = new(0, 7, -10);
//
//         public void Install(IPlayerContext context)
//         {
//             context.AddCamera(_camera);
//             context.AddController<CameraDisplayController>();
//             context.AddController(new CameraFollowController(_cameraOffset));
//         }
//     }
// }