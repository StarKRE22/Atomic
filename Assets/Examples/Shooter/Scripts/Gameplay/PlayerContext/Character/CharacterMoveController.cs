using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterMoveController : IEntityInit<IPlayerContext>, IEntityTick
    {
        private IGameContext _gameContext;
        private IWorldEntity _character;
        private IPlayerContext _playerContext;
        
        public void Init(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _playerContext = context;
            _gameContext = GameContext.Instance;
        }

        public void Tick(IEntity entity, float deltaTime)
        {
            Vector3 moveDirection = MoveInputUseCase.GetMoveDirection(_playerContext, _gameContext);
            _character.GetMovementDirection().Value = moveDirection;
        }
    }
}