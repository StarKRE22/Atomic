// using Atomic.Contexts;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CameraDisplayController : IContextInit<IPlayerContext>
//     {
//         public void Init(IPlayerContext context)
//         {
//             Camera camera = context.GetCamera();
//             TeamType teamType = context.GetTeam().Value;
//             
//             camera.targetDisplay = GameContext.Instance
//                 .GetTeamConfig()
//                 .GetTeam(teamType).CameraDisplay;
//         }
//     }
// }