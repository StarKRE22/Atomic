using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class GameOverPresenter : IEntityInit<IUIContext>, IEntityEnable, IEntityDispose
    {
        private IUIContext _context;
        private TeamCatalog _catalog;
        private GameOverView _view;

        public void Init(IUIContext context)
        {
            _catalog = GameContext.Instance.GetTeamCatalog();
            _context = context;
            _view = context.GetGameOverView();

            TeamType teamType = GameContext.Instance.GetWinnerTeam().Value;
            _view.SetMessage($"{teamType} PLAYER \nWINS");
            _view.SetMessageColor(_catalog.GetInfo(teamType).Material.color);
            _view.OnRestartClicked += RestartUseCase.RestartGame;
            _view.OnCloseClicked += this.OnCloseClicked;
        }

        public void Enable(IEntity entity)
        {
            _view.Show();
        }

        public void Dispose(IEntity context)
        {
            _view.OnRestartClicked -= RestartUseCase.RestartGame;
            _view.OnCloseClicked -= this.OnCloseClicked;
        }

        private void OnCloseClicked()
        {
            GameOverUseCase.HidePopup(_context);
        }
    }
}