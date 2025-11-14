using ShooterGame.App;

namespace ShooterGame.UI
{
    public sealed class StartScreenPresenter :
        IMenuUIInit,
        IMenuUIEnable,
        IMenuUIDisable
    {
        private readonly StartScreenView _screenView;
        private readonly IAppContext _appContext;
        
        private IMenuUI _uIContext;

        public StartScreenPresenter(StartScreenView screenView, IAppContext appContext)
        {
            _screenView = screenView;
            _appContext = appContext;
        }

        public void Init(IMenuUI context)
        {
            _uIContext = context;
        }

        public void Enable(IMenuUI entity)
        {
            _screenView.OnSelectLevelClicked += this.OnSelectLevelClicked;
            _screenView.OnStartClicked += this.OnStartClicked;
            _screenView.OnExitClicked += QuitUseCase.Quit;
        }

        public void Disable(IMenuUI entity)
        {
            _screenView.OnStartClicked -= this.OnStartClicked;
            _screenView.OnSelectLevelClicked -= this.OnSelectLevelClicked;
            _screenView.OnExitClicked -= QuitUseCase.Quit;
        }

        private void OnStartClicked() => GameLoadingUseCase.StartGame(_appContext);

        private void OnSelectLevelClicked() => ScreenUseCase.ShowScreen<LevelScreenView>(_uIContext);
    }
}