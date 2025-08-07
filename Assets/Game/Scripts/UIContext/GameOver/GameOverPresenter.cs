using Atomic.Entities;

namespace SampleGame
{
    public sealed class GameOverPresenter : IEntitySpawn<IUIContext>, IEntityDespawn
    {
        private GameOverView _view;

        public void OnSpawn(IUIContext context)
        {
            _view = context.GetGameOverView();
            
            TeamType teamType = GameContext.Instance.GetWinnerTeam().Value;
            _view.SetMessage($"{teamType} PLAYER WINS");
            _view.SetMessageColor(teamType.GetColor());
            _view.OnRestartClicked += RestartGameUseCase.RestartGame;
        }

        public void OnDespawn(IEntity context)
        {
            _view.OnRestartClicked -= RestartGameUseCase.RestartGame;
        }
    }
}