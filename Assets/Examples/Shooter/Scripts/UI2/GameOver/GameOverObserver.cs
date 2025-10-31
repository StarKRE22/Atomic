// using Atomic.Elements;
// using Atomic.Entities;
//
// namespace BeginnerGame
// {
//     public sealed class GameOverObserver : IEntityInit<IUIContext>, IEntityDispose
//     {
//         private IUIContext _context;
//         private IEvent _gameOverEvent;
//         
//         public void Init(IUIContext context)
//         {
//             _context = context;
//             _gameOverEvent = GameContext.Instance.GetGameOverEvent();
//             _gameOverEvent.Subscribe(this.OnGameOver);
//         }
//
//         public void Dispose(IEntity entity)
//         {
//             _gameOverEvent.Unsubscribe(this.OnGameOver);
//         }
//
//         private void OnGameOver() => GameOverUseCase.ShowPopup(_context);
//     }
// }