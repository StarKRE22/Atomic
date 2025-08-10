using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterFireController : IEntitySpawn<IPlayerContext>, IEntityUpdate
    {
        private IGameContext _gameContext;
        private IGameEntity _character;
        private IPlayerContext _playerContext;

        public void OnSpawn(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _playerContext = context;
            _gameContext = GameContext.Instance;
        }

        public void OnUpdate(IEntity entity, float deltaTime)
        {
            if (FireInputUseCase.FireRequired(_playerContext, _gameContext))
                _character.GetFireAction().Invoke();
        }
    }
}