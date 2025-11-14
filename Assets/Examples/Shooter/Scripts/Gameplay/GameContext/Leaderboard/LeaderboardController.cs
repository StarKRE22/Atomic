using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class LeaderboardController : IGameContextInit, IGameContextDispose
    {
        private ISignal<KillArgs> _killEvent;
        private IGameContext _gameContext;

        public void Init(IGameContext context)
        {
            _gameContext = context;
            _killEvent = context.GetKillEvent();
            _killEvent.OnEvent += this.OnKill;
        }

        public void Dispose(IGameContext context)
        {
            _killEvent.OnEvent -= this.OnKill;
        }

        private void OnKill(KillArgs args)
        {
            LeaderboardUseCase.AddScore(_gameContext, args);
        }
    }
}