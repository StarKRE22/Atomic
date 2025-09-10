using System.Runtime.CompilerServices;
using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class AIDetectTargetBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick, IEntityDisable
    {
        private readonly IGameContext _gameContext;
        private readonly IEntityWorld<IGameEntity> _entityWorld;
        private readonly ICooldown _cooldown;

        private IVariable<IGameEntity> _target;
        private IGameEntity _entity;

        public AIDetectTargetBehaviour(ICooldown cooldown, IGameContext context)
        {
            _cooldown = cooldown;
            _gameContext = context;
            _entityWorld = context.GetEntityWorld();
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
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
            IGameEntity enemy = GameEntitiesUseCase.FindFreeEnemyFor(_gameContext, _entity);
            if (enemy != null) 
                enemy.AddTargetedTag();
            
            _target.Value = enemy;
        }

        public void Disable(IEntity entity)
        {
            IGameEntity target = _target.Value;
            if (target != null) 
                target.DelTargetedTag();
        }
    }
}