// using Atomic.Contexts;
// using Atomic.Elements;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class PlayerContextInstaller : SceneContextInstaller<IPlayerContext>
//     {
//         [SerializeField]
//         private CharacterSystemInstaller _characterInstaller;
//
//         [SerializeField]
//         private CameraInstaller _cameraInstaller;
//         
//         [SerializeField]
//         private InputMap _inputMap;
//
//         [SerializeField]
//         private TeamType _teamType;
//         
//         protected override void Install(IPlayerContext context)
//         {
//             context.AddTeam(new Const<TeamType>(_teamType));
//             context.AddInputMap(_inputMap);
//             
//             _characterInstaller.Install(context);
//             _cameraInstaller.Install(context);
//
//             this.InstallGameContext(context);
//         }
//
//         private void InstallGameContext(IPlayerContext context)
//         {
//             GameContext gameContext = GameContext.Instance;
//             gameContext.GetLeaderboard().Add(_teamType, 0);
//             gameContext.GetPlayers().Add(_teamType, context);
//         }
//     }
// }