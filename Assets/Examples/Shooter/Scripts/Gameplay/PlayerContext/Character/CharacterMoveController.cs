using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterMoveController : IPlayerContextInit, IPlayerContextTick
    {
        private readonly IGameContext _gameContext;

        private IGameEntity _character;
        private IPlayerContext _playerContext;

        public CharacterMoveController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _playerContext = context;
        }

        public void Tick(IPlayerContext entity, float deltaTime)
        {
            Vector3 moveDirection = InputUseCase.GetMoveDirection(_playerContext, _gameContext);
            _character.GetMovementDirection().Value = moveDirection;
        }
    }
}