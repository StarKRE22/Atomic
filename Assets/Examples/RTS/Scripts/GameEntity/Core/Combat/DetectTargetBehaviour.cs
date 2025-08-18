using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class DetectTargetBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private readonly IGameContext _gameContext;
        private readonly ICooldown _cooldown;

        private IVariable<IEntity> _target;
        private IGameEntity _entity;

        public DetectTargetBehaviour(ICooldown cooldown, IGameContext context)
        {
            _cooldown = cooldown;
            _gameContext = context;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _cooldown.Tick(deltaTime);
            if (_cooldown.IsCompleted())
            {
                _target.Value = GameEntitiesUseCase.FindClosestEnemyFor(_gameContext, _entity);
                _cooldown.ResetTime();
            }
        }
    }
}