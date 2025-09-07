using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterMoveController : IEntityInit<IPlayerContext>, IEntityUpdate
    {
        private IGameContext _gameContext;
        private IGameEntity _character;
        private IPlayerContext _playerContext;
        
        public void Init(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _playerContext = context;
            _gameContext = GameContext.Instance;
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            Vector3 moveDirection = MoveInputUseCase.GetMoveDirection(_playerContext, _gameContext);
            _character.GetMovementDirection().Value = moveDirection;
        }
    }
}