using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class GameCountdownController : IEntitySpawn<IGameContext>, IEntityFixedUpdate
    {
        private IGameContext _context;
        private ICooldown _countdown;

        public void OnSpawn(IGameContext context)
        {
            _context = context;
            _countdown = context.GetGameCountdown();
            _countdown.Reset();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            if (_countdown.IsExpired())
                GameOverUseCase.GameOver(_context);
            else
                _countdown.Tick(deltaTime);
        }
    }
}