
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class LeaderboardController : IEntityInit<IGameContext>, IEntityDispose
    {
        private ISignal<KillArgs> _killEvent;
        private IGameContext _gameContext;

        public void Init(IGameContext context)
        {
            _gameContext = context;
            _killEvent = context.GetKillEvent();
            _killEvent.Subscribe(this.OnKill);
        }

        public void Dispose(IEntity entity)
        {
            _killEvent.Unsubscribe(this.OnKill);
        }

        private void OnKill(KillArgs args)
        {
            LeaderboardUseCase.AddScore(_gameContext, args);
        }
    }
}