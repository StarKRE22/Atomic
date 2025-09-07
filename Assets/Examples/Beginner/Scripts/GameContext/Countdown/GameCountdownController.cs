using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class GameCountdownController : IEntityInit<IGameContext>, IEntityFixedTick
    {
        private IGameContext _context;
        private ICooldown _countdown;

        public void Init(IGameContext context)
        {
            _context = context;
            _countdown = context.GetGameCountdown();
            _countdown.ResetTime();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            if (_countdown.IsCompleted())
                GameOverUseCase.GameOver(_context);
            else
                _countdown.Tick(deltaTime);
        }
    }
}