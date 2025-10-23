using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class AIDetectTargetBehaviour : IEntityInit<IUnitEntity>, IEntityFixedTick, IEntityDisable
    {
        private readonly IGameContext _gameContext;
        private readonly ICooldown _cooldown;

        private IEntityWorld<IUnitEntity> _entityWorld;
        private IVariable<IUnitEntity> _target;
        private IUnitEntity _entity;

        public AIDetectTargetBehaviour(ICooldown cooldown, IGameContext context)
        {
            _cooldown = cooldown;
            _gameContext = context;
        }

        public void Init(IUnitEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
            _entityWorld = _gameContext.GetEntityWorld();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            _cooldown.Tick(deltaTime);
            
            if (_cooldown.IsCompleted() && !_entityWorld.Contains(_target.Value))
            {
                this.AssignTarget();
                _cooldown.ResetTime();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AssignTarget()
        {
            IUnitEntity enemy = UnitsUseCase.FindFreeEnemyFor(_gameContext, _entity);
            if (enemy != null) 
                enemy.AddTargetedTag();
            
            _target.Value = enemy;
        }

        public void Disable(IEntity entity)
        {
            IUnitEntity target = _target.Value;
            if (target != null) 
                target.DelTargetedTag();
        }
    }
}