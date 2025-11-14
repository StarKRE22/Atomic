using ShooterGame.App;
using ShooterGame.Gameplay;
using UnityEngine;

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

        public void Enable(IGameUI entity)
        {
            TeamType teamType = LeaderboardUseCase.GetWinner(_gameContext);

            string message = teamType == TeamType.NEUTRAL ? "DRAW" : $"{teamType} PLAYER \nWINS";
            _view.SetMessage(message);
            
            Color color = _catalog.GetInfo(teamType).Material.color;
            _view.SetMessageColor(color);
            _view.Show();
        }

        private void OnRestartClicked() => GameLoadingUseCase.StartGame(_appContext);

        private void OnCloseClicked() => MenuUseCase.LoadMenu().Forget();
    }
}