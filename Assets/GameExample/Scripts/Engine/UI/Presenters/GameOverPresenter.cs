using System;
using Atomic.UI;
using UnityEngine;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class GameOverPresenter : IViewEnable, IViewDisable
    {
        [SerializeField]
        private GameOverView view;

        public void Enable()
        {
            TeamType teamType = GameContext.Instance.GetWinnerPlayerTeam();
            this.view.SetMessage($"{teamType} PLAYER WINS");
            this.view.SetMessageColor(teamType.GetColor());

            this.view.OnRestartClicked += RestartGameUseCase.RestartGame;
        }

        public void Disable()
        {
            this.view.OnRestartClicked -= RestartGameUseCase.RestartGame;
        }
    }
}