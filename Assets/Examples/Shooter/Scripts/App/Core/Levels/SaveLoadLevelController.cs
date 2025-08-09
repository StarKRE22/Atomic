using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class SaveLoadLevelController : IEntitySpawn<IAppContext>, IEntityActivate, IEntityDeactivate
    {
        private IReactiveVariable<int> _currentLevel;

        public void OnSpawn(IAppContext context)
        {
            _currentLevel = context.GetCurrentLevel();

            if (SaveLevelUseCase.LoadLevel(out int level))
                _currentLevel.Value = level;
        }

        public void OnActivate(IEntity entity)
        {
            _currentLevel.Subscribe(SaveLevelUseCase.SaveLevel);
        }

        public void OnDeactivate(IEntity entity)
        {
            _currentLevel.Unsubscribe(SaveLevelUseCase.SaveLevel);
        }
    }
}