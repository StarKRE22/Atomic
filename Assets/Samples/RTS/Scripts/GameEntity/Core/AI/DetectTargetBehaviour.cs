using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class DetectTargetBehaviour : IEntityInit<IGameEntity>, IEntityFixedUpdate
    {
        private readonly IGameContext _gameContext;
        private readonly IEntityWorld<IGameEntity> _entityWorld;
        private readonly ICooldown _cooldown;

        private IVariable<IGameEntity> _target;
        private IGameEntity _entity;

        public DetectTargetBehaviour(ICooldown cooldown, IGameContext context)
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

        public void FixedUpdate(IEntity entity, float deltaTime)
        {
            _cooldown.Tick(deltaTime);
            
            if (_cooldown.IsCompleted() && !_entityWorld.Contains(_target.Value))
            {
                _target.Value = GameEntityUseCase.FindClosestEnemyFor(_gameContext, _entity);
                _cooldown.ResetTime();
            }
        }
    }
}