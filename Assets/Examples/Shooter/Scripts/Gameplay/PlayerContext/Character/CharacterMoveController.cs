// using Atomic.Contexts;
// using Atomic.Entities;
// using UnityEngine;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterMoveController : IContextInit<IPlayerContext>, IContextUpdate
//     {
//         private IGameContext _gameContext;
//         private IEntity _character;
//         private IPlayerContext _playerContext;
//
//         public void Init(IPlayerContext context)
//         {
//             _character = context.GetCharacter();
//             _playerContext = context;
//             _gameContext = GameContext.Instance;
//         }
//
//         public void OnUpdate(IContext context, float deltaTime)
//         {
//             Vector3 moveDirection = MoveInputUseCase.GetMoveDirection(_playerContext, _gameContext);
//             _character.GetMoveDirection().Value = moveDirection;
//         }
//     }
// }