// using Atomic.Entities;
// using UnityEngine;
//
// namespace RTSGame
// {
//     public sealed class HeadquatersViewInstaller : EntityViewInstaller
//     {
//         [SerializeField]
//         private Transform _transform;
//         
//         [SerializeField]
//         private Renderer[] _renderers;
//
//         public override void Install(EntityView view)
//         {
//             view.AddBehaviour(new PositionViewBehaviour(_transform));
//             view.AddBehaviour(new RotationViewBehaviour(_transform));
//             view.AddBehaviour(new TeamColorBehaviour(_renderers));
//         }
//     }
// }