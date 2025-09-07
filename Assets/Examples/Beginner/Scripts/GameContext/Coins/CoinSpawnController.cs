using Atomic.Elements;
using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class CoinSpawnController : IEntityInit<IGameContext>, IEntityFixedUpdate
    {
        private readonly Cooldown _spawnPeriod;
        private IGameContext _context;

        public CoinSpawnController(Cooldown spawnPeriod)
        {
            _spawnPeriod = spawnPeriod;
        }

        public void Init(IGameContext context)
        {
            _context = context;
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _spawnPeriod.Tick(deltaTime);
            if (_spawnPeriod.IsCompleted())
            {
                CoinUseCase.Spawn(_context);
                _spawnPeriod.ResetTime();
            }
        }
    }
}