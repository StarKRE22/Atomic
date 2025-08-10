using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class LifetimeBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Cooldown _lifetime;
        private IAction _destroyAction;

        public void OnSpawn(IGameEntity entity)
        {
            _destroyAction = entity.GetDestroyAction();
            _lifetime = entity.GetLifetime();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _lifetime.Tick(deltaTime);
            
            if (_lifetime.IsExpired()) 
                _destroyAction.Invoke();
        }
    }
}