using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class SaveLoadLevelController : IEntityInit<IAppContext>, IEntityEnable, IEntityDisable
    {
        private IReactiveVariable<int> _currentLevel;

        public void Init(IAppContext context)
        {
            _currentLevel = context.GetCurrentLevel();

            if (LevelUseCase.LoadLevel(out int level))
                _currentLevel.Value = level;
        }

        public void Enable(IEntity entity)
        {
            _currentLevel.Subscribe(LevelUseCase.SaveLevel);
        }

        public void Disable(IEntity entity)
        {
            _currentLevel.Unsubscribe(LevelUseCase.SaveLevel);
        }
    }
}