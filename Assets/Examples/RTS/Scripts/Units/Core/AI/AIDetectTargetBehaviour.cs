using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class AIDetectTargetBehaviour : IEntityInit<IUnit>, IEntityFixedTick
    {
        private static readonly IUnit[] BUFFER = new IUnit[512];

        private readonly IGameContext _gameContext;
        private readonly ICooldown _cooldown;

        private IEntityWorld<IUnit> _entityWorld;
        private IVariable<IUnit> _target;
        private IUnit _self;

        public AIDetectTargetBehaviour(ICooldown cooldown, IGameContext context)
        {
            _cooldown = cooldown;
            _gameContext = context;
        }

        public void Init(IUnit entity)
        {
            _self = entity;
            _target = entity.GetTarget();
            _entityWorld = _gameContext.GetEntityWorld();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            _cooldown.Tick(deltaTime);

            if (_cooldown.IsCompleted() && !_entityWorld.Contains(_target.Value))
            {
                _target.Value = UnitsUseCase.FindClosestEnemy(_self, _gameContext, BUFFER);
                _cooldown.ResetTime();
            }
        }
    }
}