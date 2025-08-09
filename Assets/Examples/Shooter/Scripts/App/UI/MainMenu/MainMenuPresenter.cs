using Atomic.Elements;
using Atomic.Entities;
using ShooterGame.App;

namespace ShooterGame.UI
{
    public sealed class MainMenuPresenter : IEntitySpawn<IMenuUIContext>, IEntityActive, IEntityInactive
    {
        private MainMenuView _menuView;

        private IReactiveVariable<int> _currentLevel;

        public void OnSpawn(IMenuUIContext context)
        {
            _menuView = context.GetMainMenuView();
            _currentLevel = AppContext.Instance.GetCurrentLevel();
        }   

        public void OnActive(IEntity entity)
        {
            _menuView.SetCurrentLevel(_currentLevel.Value.ToString());
            _menuView.OnLevelChanged += this.OnLevelChanged;
            _menuView.OnStartClicked += this.OnStartClicked;
            _menuView.OnExitClicked += ExitAppUseCase.Exit;
        }

        public void OnInactive(IEntity entity)
        {
            _menuView.OnLevelChanged -= this.OnLevelChanged;
            _menuView.OnStartClicked -= this.OnStartClicked;
            _menuView.OnExitClicked -= ExitAppUseCase.Exit;
        }

        private void OnLevelChanged(string arg0)
        {
            if (int.TryParse(_menuView.GetCurrentLevel(), out int level))
                _currentLevel.Value = level;
        }

        private void OnStartClicked() => StartLevelUseCase.StartLevel(_currentLevel.Value);
    }
}