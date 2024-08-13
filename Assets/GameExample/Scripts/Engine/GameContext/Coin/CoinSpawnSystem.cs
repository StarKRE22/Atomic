using Atomic.Contexts;
using Atomic.Elements;

namespace GameExample.Engine
{
    public sealed class CoinSpawnSystem : IContextInit, IContextEnable, IContextDisable, IContextUpdate
    {
        private IContext _gameContext;
        private Cycle _spawnPeriod;
        
        public void Init(IContext context)
        {
            _gameContext = context;
            _spawnPeriod = context.GetCoinSystemData().spawnCycle;
        }
        
        public void Enable(IContext context)
        {
            _spawnPeriod.Start();
            _spawnPeriod.OnCycle += this.Spawn;
        }

        public void Update(IContext context, float deltaTime)
        {
            _spawnPeriod.Tick(deltaTime);
        }

        public void Disable(IContext context)
        {
            _spawnPeriod.Stop();
            _spawnPeriod.OnCycle -= this.Spawn;
        }

        private void Spawn()
        {
            _gameContext.SpawnCoinInArea();
        }
    }
}