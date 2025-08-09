// using System;
// using Atomic.Contexts;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterRespawnController : IContextInit<IPlayerContext>, IDisposable
//     {
//         private IGameContext _gameContext;
//         private IPlayerContext _playerContext;
//
//         public void Init(IPlayerContext context)
//         {
//             _gameContext = GameContext.Instance;
//             _playerContext = context;
//             _playerContext.GetCharacter().GetHealth().OnHealthEmpty += this.OnHealthEmpty;
//         }
//
//         public void Dispose()
//         {
//             _playerContext.GetCharacter().GetHealth().OnHealthEmpty -= this.OnHealthEmpty;
//         }
//
//         private void OnHealthEmpty()
//         {
//             CharacterRespawnUseCase.RespawnWithDelay(_playerContext, _gameContext).Forget();
//         }
//     }
// }