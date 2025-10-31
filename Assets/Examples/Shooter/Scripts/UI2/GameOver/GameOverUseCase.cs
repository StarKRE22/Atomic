// using Atomic.Entities;
// using UnityEngine;
//
// namespace BeginnerGame
// {
//     public static partial class GameOverUseCase
//     {
//         public static void ShowPopup(IUIContext context)
//         {
//             GameOverView viewPrefab = context.GetGameOverViewPrefab();
//             Transform parent = context.GetPopupTransform();
//             GameOverView view = GameObject.Instantiate(viewPrefab, parent);
//             context.AddGameOverView(view);
//             context.AddBehaviour<GameOverPresenter>();
//         }
//
//         public static void HidePopup(IUIContext context)
//         {
//             if (context.TryGetGameOverView(out GameOverView view))
//             {
//                 context.DelBehaviour<GameOverPresenter>();
//                 context.DelGameOverView();
//                 GameObject.Destroy(view.gameObject);
//             }
//         }
//     }
// }