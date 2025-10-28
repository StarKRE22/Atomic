using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterFireController : IEntityInit<IPlayerContext>, IEntityTick
    {
        private IGameContext _gameContext;
        private IActor _character;
        private IPlayerContext _playerContext;

        public void Init(IPlayerContext context)
        {
            _character = context.GetCharacter();
            _playerContext = context;
            _gameContext = GameContext.Instance;
        }

        public void Tick(IEntity entity, float deltaTime)
        {
            if (FireInputUseCase.FireRequired(_playerContext, _gameContext))
                _character.GetFireAction().Invoke();
        }
    }
}