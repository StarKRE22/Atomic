// using Atomic.Contexts;
// using Atomic.Elements;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class GameCycleController : IContextInit<IGameContext>, IContextUpdate
//     {
//         private IReactiveVariable<float> _gameTime;
//
//         void IContextInit<IGameContext>.Init(IGameContext context)
//         {
//             _gameTime = context.GetGameTime();
//             Debug.Log("<color=yellow>Game Started!</color>");
//         }
//
//         void IContextUpdate.OnUpdate(IContext context, float deltaTime)
//         {
//             if (_gameTime.Value <= 0)
//                 return;
//
//             _gameTime.Value -= deltaTime;
//
//             if (_gameTime.Value <= 0) 
//                 Debug.Log("<color=yellow>Game Finished</color>");
//         }
//     }
// }