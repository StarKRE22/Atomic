namespace ShooterGame.Gameplay
{
    public sealed class CharacterFireController : IPlayerContextInit, IPlayerContextTick
    {
        private readonly IGameContext _gameContext;
        
        private IGameEntity _character;
        private IPlayerContext _playerContext;
        
        public CharacterFireController(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IPlayerContext context)
        {
            _playerContext = context;
            _character = context.GetCharacter();
        }

        public void Tick(IPlayerContext entity, float deltaTime)
        {
            if (InputUseCase.FireRequired(_playerContext, _gameContext))
                _character.GetFireAction().Invoke();
        }
    }
}