using Atomic.Elements;
using Atomic.Entities;

namespace SampleGame
{
    public sealed class CoinSpawnController : IEntitySpawn<IGameContext>, IEntityFixedUpdate
    {
        private IGameContext _context;
        private Cooldown _cooldown;

        public void OnSpawn(IGameContext context)
        {
            _context = context;
            _cooldown = context.GetCoinSpawnPeriod();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _cooldown.Tick(deltaTime);
            if (_cooldown.IsExpired())
            {
                CoinUseCase.Spawn(_context);
                _cooldown.Reset();
            }
        }
    }
}