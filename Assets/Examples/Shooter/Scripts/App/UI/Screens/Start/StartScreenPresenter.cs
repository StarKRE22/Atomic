using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class StartScreenPresenter :
        IEntityInit<IMenuUIContext>,
        IEntityEnable,
        IEntityDisable
    {
        private readonly StartScreenView _screenView;
        
        private IAppContext _appContext;
        private IMenuUIContext _uIContext;

        public StartScreenPresenter(StartScreenView screenView)
        {
            _screenView = screenView;
        }

        public void Init(IMenuUIContext context)
        {
            _uIContext = context;
            _appContext = AppContext.Instance;
        }   

        public void Enable(IEntity entity)
        {
            _screenView.OnSelectLevelClicked += this.OnSelectLevelClicked;
            _screenView.OnStartClicked += this.OnStartClicked;
            _screenView.OnExitClicked += ExitAppUseCase.Exit;
        }

        public void Disable(IEntity entity)
        {
            _screenView.OnStartClicked -= this.OnStartClicked;
            _screenView.OnSelectLevelClicked -= this.OnSelectLevelClicked;
            _screenView.OnExitClicked -= ExitAppUseCase.Exit;
        }

        private void OnStartClicked() => LoadGameUseCase.StartGame(_appContext);

        private void OnSelectLevelClicked() => ScreenUseCase.ShowScreen<LevelScreenView>(_uIContext);
    }
}