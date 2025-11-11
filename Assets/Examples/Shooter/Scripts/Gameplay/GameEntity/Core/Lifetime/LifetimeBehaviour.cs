using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public sealed class LifetimeBehaviour : IEntityInit<IGameEntity>, IEntityFixedTick
    {
        private Cooldown _lifetime;
        private IAction _destroyAction;

        public void Init(IGameEntity entity)
        {
            _destroyAction = entity.GetDestroyAction();
            _lifetime = entity.GetLifetime();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            _lifetime.Tick(deltaTime);
            
            if (_lifetime.IsCompleted()) 
                _destroyAction.Invoke();
        }
    }
}