using Atomic.Entities;

namespace BeginnerGame
{
    public sealed class GameOverPresenter : IEntitySpawn<IUIContext>, IEntityDespawn
    {
        private IUIContext _context;
        private GameOverView _view;

        public void OnSpawn(IUIContext context)
        {
            _context = context;
            _view = context.GetGameOverView();

            TeamType teamType = GameContext.Instance.GetWinnerTeam().Value;
            _view.SetMessage($"{teamType} PLAYER WINS");
            _view.SetMessageColor(teamType.GetColor());
            _view.OnRestartClicked += RestartGameUseCase.RestartGame;
            _view.OnCloseClicked += this.OnCloseClicked;
        }

        public void OnDespawn(IEntity context)
        {
            _view.OnRestartClicked -= RestartGameUseCase.RestartGame;
            _view.OnCloseClicked -= this.OnCloseClicked;
        }

        private void OnCloseClicked()
        {
            GameOverViewUseCase.HidePopup(_context);
        }
    }
}