// using Atomic.Contexts;
// using Atomic.Elements;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class LeaderboardController : IContextInit<IGameContext>, IContextDispose
//     {
//         private IReactive<KillArgs> _killEvent;
//         private IGameContext _gameContext;
//
//         public void Init(IGameContext context)
//         {
//             _gameContext = context;
//             _killEvent = context.GetKillEvent();
//             _killEvent.Subscribe(this.OnKill);
//         }
//
//         public void Dispose(IContext context)
//         {
//             _killEvent.Unsubscribe(this.OnKill);
//         }
//
//         private void OnKill(KillArgs args)
//         {
//             LeaderboardUseCase.AddScore(in _gameContext, in args);
//         }
//     }
// }