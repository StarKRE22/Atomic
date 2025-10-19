using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterRespawnController : IEntityInit<IPlayerContext>, IEntityDispose
    {
        private GameContext _gameContext;

        private IPlayerContext _playerContext;
        private Health _characterHealth;

        public void Init(IPlayerContext context)
        {
            _gameContext = GameContext.Instance;
            _playerContext = context;
            _characterHealth = _playerContext.GetCharacter().GetHealth();
            _characterHealth.OnHealthEmpty += this.OnHealthEmpty;
        }

        public void Dispose(IEntity entity)
        {
            _characterHealth.OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            _gameContext.StartCoroutine(CharacterUseCase.RespawnWithDelay(_playerContext, _gameContext));
        }
    }
}