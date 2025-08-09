// using System;
// using Atomic.Contexts;
// using Atomic.Elements;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     [Serializable]
//     public sealed class GameCycleInstaller : IContextInstaller<IGameContext>
//     {
//         [SerializeField]
//         private float _gameDuration = 20;
//         
//         public void Install(IGameContext context)
//         {
//             context.AddGameTime(new ReactiveFloat(_gameDuration));
//             context.AddController<GameCycleController>();    
//         }
//     }
// }