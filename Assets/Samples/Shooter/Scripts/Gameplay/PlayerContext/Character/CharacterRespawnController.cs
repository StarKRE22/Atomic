using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterRespawnController : IEntityInit<IPlayerContext>, IEntityDispose
    {
        private IGameContext _gameContext;

        private IPlayerContext _playerContext;

        public void Init(IPlayerContext context)
        {
            _gameContext = GameContext.Instance;
            _playerContext = context;
            _playerContext.GetCharacter().GetHealth().OnHealthEmpty += this.OnHealthEmpty;
        }

        public void Dispose(IEntity entity)
        {
            _playerContext.GetCharacter().GetHealth().OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            CharacterUseCase.RespawnWithDelay(_playerContext, _gameContext).Forget();
        }
    }
}