using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class CoinSpawnController : IEntitySpawn<IGameContext>, IEntityFixedUpdate
    {
        private readonly Cooldown _spawnPeriod;
        private IGameContext _context;

        public CoinSpawnController(Cooldown spawnPeriod)
        {
            _spawnPeriod = spawnPeriod;
        }

        public void OnSpawn(IGameContext context)
        {
            _context = context;
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _spawnPeriod.Tick(deltaTime);
            if (_spawnPeriod.IsExpired())
            {
                CoinUseCase.Spawn(_context);
                _spawnPeriod.Reset();
            }
        }
    }
}