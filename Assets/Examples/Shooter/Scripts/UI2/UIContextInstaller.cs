// using Atomic.Entities;
// using UnityEngine;
//
// namespace BeginnerGame
// {
//     public sealed class UIContextInstaller : SceneEntityInstaller<IUIContext>
//     {
//         [SerializeField]
//         private CountdownView _countdownView;
//
//         [SerializeField]
//         private Transform _popupTransform;
//
//         [SerializeField]
//         private GameOverView _gameOverViewPrefab;
//         
//         public override void Install(IUIContext context)
//         {
//             //Base:
//             context.AddPopupTransform(_popupTransform);
//             
//             //Countdown:
//             context.AddGameCountdownView(_countdownView);
//             context.AddBehaviour<CountdownPresenter>();
//             
//             //GameOver:
//             context.AddBehaviour<GameOverObserver>();
//             context.AddGameOverViewPrefab(_gameOverViewPrefab);
//         }
//     }
// }