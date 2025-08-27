
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class LeaderboardController : IEntitySpawn<IGameContext>, IEntityDespawn
    {
        private ISignal<KillArgs> _killEvent;
        private IGameContext _gameContext;

        private void OnKill(KillArgs args)
        {
            LeaderboardUseCase.AddScore(_gameContext, args);
        }

        public void OnSpawn(IGameContext context)
        {
            _gameContext = context;
            _killEvent = context.GetKillEvent();
            _killEvent.Subscribe(this.OnKill);
        }

        public void OnDespawn(IEntity entity)
        {
            _killEvent.Unsubscribe(this.OnKill);
        }
    }
}