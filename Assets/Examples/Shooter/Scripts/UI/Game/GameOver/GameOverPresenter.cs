using ShooterGame.App;
using ShooterGame.Gameplay;

namespace ShooterGame.UI
{
    public sealed class GameOverPresenter : IGameUIInit, IGameUIEnable, IGameUIDispose
    {
        private readonly IGameContext _gameContext;
        private readonly IAppContext _appContext;

        private TeamCatalog _catalog;
        private GameOverView _view;

        public GameOverPresenter(IGameContext gameContext, IAppContext appContext)
        {
            _gameContext = gameContext;
            _appContext = appContext;
        }

        public void Init(IGameUI entity)
        {
            _catalog = _gameContext.GetTeamCatalog();
            _view = entity.GetGameOverView();
            _view.OnRestartClicked += this.OnRestartClicked;
            _view.OnCloseClicked += this.OnCloseClicked;
        }

        public void Dispose(IGameUI entity)
        {
            _view.OnRestartClicked -= this.OnRestartClicked;
            _view.OnCloseClicked -= this.OnCloseClicked;
        }

        private void OnRestartClicked() => GameLoadingUseCase.StartGame(_appContext);

        private void OnCloseClicked() => MenuUseCase.LoadMenu().Forget();
        
        public void Enable(IGameUI entity)
        {
            TeamType teamType = LeaderboardUseCase.GetWinner(_gameContext);
            _view.SetMessage($"{teamType} PLAYER \nWINS");
            _view.SetMessageColor(_catalog.GetInfo(teamType).Material.color);
            _view.Show();
        }
    }
}