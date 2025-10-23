using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class LevelPersistentController : IEntityInit<IAppContext>, IEntityEnable, IEntityDisable
    {
        private IReactiveVariable<int> _currentLevel;

        public void Init(IAppContext context)
        {
            _currentLevel = context.GetCurrentLevel();

            if (LevelsUseCase.LoadLevel(out int level))
                _currentLevel.Value = level;
        }

        public void Enable(IEntity entity)
        {
            _currentLevel.Subscribe(LevelsUseCase.SaveLevel);
        }

        public void Disable(IEntity entity)
        {
            _currentLevel.Unsubscribe(LevelsUseCase.SaveLevel);
        }
    }
}