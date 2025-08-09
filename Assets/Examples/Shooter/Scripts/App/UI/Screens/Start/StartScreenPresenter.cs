using Atomic.Entities;

namespace ShooterGame.App
{
    public sealed class StartScreenPresenter :
        IEntitySpawn<IMenuUIContext>,
        IEntityActivate,
        IEntityDeactivate
    {
        private readonly StartScreenView _screenView;
        
        private IAppContext _appContext;
        private IMenuUIContext _uIContext;

        public StartScreenPresenter(StartScreenView screenView)
        {
            _screenView = screenView;
        }

        public void OnSpawn(IMenuUIContext context)
        {
            _uIContext = context;
            _appContext = AppContext.Instance;
        }   

        public void OnActivate(IEntity entity)
        {
            _screenView.OnSelectLevelClicked += this.OnSelectLevelClicked;
            _screenView.OnStartClicked += this.OnStartClicked;
            _screenView.OnExitClicked += ExitAppUseCase.Exit;
        }

        public void OnDeactivate(IEntity entity)
        {
            _screenView.OnStartClicked -= this.OnStartClicked;
            _screenView.OnSelectLevelClicked -= this.OnSelectLevelClicked;
            _screenView.OnExitClicked -= ExitAppUseCase.Exit;
        }

        private void OnStartClicked() => LoadGameUseCase.StartGame(_appContext);

        private void OnSelectLevelClicked() => ScreenUseCase.ShowScreen<LevelScreenView>(_uIContext);
    }
}