using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterRespawnController : IEntitySpawn<IPlayerContext>, IEntityDespawn
    {
        private IGameContext _gameContext;

        private IPlayerContext _playerContext;

        public void OnSpawn(IPlayerContext context)
        {
            _gameContext = GameContext.Instance;
            _playerContext = context;
            _playerContext.GetCharacter().GetHealth().OnHealthEmpty += this.OnHealthEmpty;
        }

        public void OnDespawn(IEntity entity)
        {
            _playerContext.GetCharacter().GetHealth().OnHealthEmpty -= this.OnHealthEmpty;
        }

        private void OnHealthEmpty()
        {
            CharacterUseCase.RespawnWithDelay(_playerContext, _gameContext).Forget();
        }
    }
}