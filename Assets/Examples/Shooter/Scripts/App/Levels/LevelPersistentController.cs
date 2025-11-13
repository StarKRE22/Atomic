using Atomic.Elements;

namespace ShooterGame.App
{
    public sealed class LevelPersistentController : IAppContextInit, IAppContextDispose
    {
        private IReactiveVariable<int> _currentLevel;

        public void Init(IAppContext context)
        {
            _currentLevel = context.GetCurrentLevel();
            _currentLevel.OnEvent += LevelsUseCase.SaveLevel;

            if (LevelsUseCase.LoadLevel(out int level))
                _currentLevel.Value = level;
        }

        public void Dispose(IAppContext context)
        {
            _currentLevel.OnEvent -= LevelsUseCase.SaveLevel;
        }
    }
}