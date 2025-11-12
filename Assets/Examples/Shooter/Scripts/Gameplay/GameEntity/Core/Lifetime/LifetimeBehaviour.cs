using Atomic.Elements;

namespace ShooterGame.Gameplay
{
    public sealed class LifetimeBehaviour : IGameEntityInit, IGameEntityFixedTick
    {
        private Cooldown _lifetime;
        private IAction _destroyAction;

        public void Init(IGameEntity entity)
        {
            _destroyAction = entity.GetDestroyAction();
            _lifetime = entity.GetLifetime();
        }

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
            _lifetime.Tick(deltaTime);

            if (_lifetime.IsCompleted())
                _destroyAction.Invoke();
        }
    }
}