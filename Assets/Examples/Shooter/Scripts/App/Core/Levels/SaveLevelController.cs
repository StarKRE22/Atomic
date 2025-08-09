using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class SaveLevelController : IEntitySpawn<IAppContext>, IEntityDespawn
    {
        private IReactiveVariable<int> _currentLevel;

        public void OnSpawn(IAppContext context)
        {
            _currentLevel = context.GetCurrentLevel();
            _currentLevel.Value = SaveLevelUseCase.LoadLevel();
            _currentLevel.Subscribe(SaveLevelUseCase.SaveLevel);
        }

        public void OnDespawn(IEntity entity)
        {
            _currentLevel.Unsubscribe(SaveLevelUseCase.SaveLevel);
        }
    }
}