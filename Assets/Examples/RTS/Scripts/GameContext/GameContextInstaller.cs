// using Atomic.Contexts;
// using UnityEngine;
//
// namespace RTSGame
// {
//     [CreateAssetMenu(
//         fileName = "GameContextInstaller",
//         menuName = "SampleGame/GameContext/New GameContextInstaller"
//     )]
//     public sealed class GameContextInstaller : ScriptableContextInstaller<GameContext>
//     {
//         [SerializeField]
//         private EntitySystemInstaller _entityInstaller;
//
//         [SerializeField]
//         private PlayerSystemInstaller _playersInstaller;
//
//         [SerializeField]
//         private TeamViewConfig _teamViewConfig;
//         
//         protected override void Install(GameContext context)
//         {
//             _entityInstaller.Install(context);
//             _playersInstaller.Install(context);
//
//             context.AddTeamViewConfig(_teamViewConfig);
//         }
//     }
// }