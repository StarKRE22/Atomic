using Atomic.Entities;
using ShooterGame.App;

namespace ShooterGame.UI
{
    public sealed class StartScreenPresenter :
        IEntityInit<IMenuUI>,
        IEntityEnable,
        IEntityDisable
    {
        private readonly StartScreenView _screenView;
        
        private IAppContext _appContext;
        private IMenuUI _uIContext;

        public StartScreenPresenter(StartScreenView screenView)
        {
            _screenView = screenView;
        }

        public void Init(IMenuUI context)
        {
            _uIContext = context;
            _appContext = AppContext.Instance;
        }   

        public void Enable(IEntity entity)
        {
            _screenView.OnSelectLevelClicked += this.OnSelectLevelClicked;
            _screenView.OnStartClicked += this.OnStartClicked;
            _screenView.OnExitClicked += QuitUseCase.Quit;
        }

        public void Disable(IEntity entity)
        {
            _screenView.OnStartClicked -= this.OnStartClicked;
            _screenView.OnSelectLevelClicked -= this.OnSelectLevelClicked;
            _screenView.OnExitClicked -= QuitUseCase.Quit;
        }

        private void OnStartClicked() => LoadGameUseCase.StartGame(_appContext);

        private void OnSelectLevelClicked() => ScreenUseCase.ShowScreen<LevelScreenView>(_uIContext);
    }
}