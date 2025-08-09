// using Atomic.Contexts;
// using Atomic.Entities;
//
// namespace ShooterGame.Gameplay
// {
//     public sealed class CharacterFireController : IContextInit<IPlayerContext>, IContextUpdate
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
//             if (FireInputUseCase.FireRequired(_playerContext, _gameContext))
//                 _character.GetFireAction().Invoke();
//         }
//     }
// }