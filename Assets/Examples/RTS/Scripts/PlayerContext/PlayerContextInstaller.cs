// using Atomic.Contexts;
// using UnityEngine;
//
// namespace RTSGame
// {
//     [CreateAssetMenu(
//         fileName = "PlayerContextInstaller",
//         menuName = "SampleGame/PlayerContext/New PlayerContextInstaller"
//     )]
//     public sealed class PlayerContextInstaller : ScriptableContextInstaller<PlayerContext>
//     {
//         [SerializeField]
//         private UnitSelectionInstaller _unitSelectionInstaller;
//         
//         protected override void Install(PlayerContext context)
//         {
//             _unitSelectionInstaller.Install(context);
//         }
//     }
// }