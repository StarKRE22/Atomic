namespace ShooterGame.Gameplay
{
    public sealed class CharacterRespawnController : IPlayerContextInit, IPlayerContextDispose
    {
        private readonly GameContext _gameContext;

        private IPlayerContext _playerContext;
        private Health _characterHealth;

        public CharacterRespawnController(GameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IPlayerContext context)
        {
            _playerContext = context;
            _characterHealth = _playerContext.GetCharacter().GetHealth();
            _characterHealth.OnHealthEmpty += this.OnHealthEmpty;
        }

        public void Dispose(IPlayerContext entity)
        {
            _characterHealth.OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            _gameContext.StartCoroutine(CharacterUseCase.RespawnWithDelay(_playerContext, _gameContext));
        }
    }
}