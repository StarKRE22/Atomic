using Atomic.Entities;
using ShooterGame.App;
using ShooterGame.Gameplay;
using TMPro;
using UnityEngine;

namespace ShooterGame.UI
{
    public sealed class GameUIInstaller : SceneEntityInstaller<IGameUI>
    {
        [Header("Countdown")]
        [SerializeField]
        private TMP_Text _countdownView;

        [Header("Score")]
        [SerializeField]
        private TMP_Text _blueScoreView;

        [SerializeField]
        private TMP_Text _redScoreView;

        [Header("Popups")]
        [SerializeField]
        private Transform _popupTransform;

        [Header("GameOver")]
        [SerializeField]
        private GameOverView _gameOverViewPrefab;

        public override void Install(IGameUI ui)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            AppContext.TryGetInstance(out AppContext appContext);

            // Countdown
            ui.AddBehaviour(new CountdownPresenter(_countdownView, gameContext));

            // Score
            ui.AddBehaviour(new ScorePresenter(_blueScoreView, TeamType.BLUE, gameContext));
            ui.AddBehaviour(new ScorePresenter(_redScoreView, TeamType.RED, gameContext));
            
            // Popups
            ui.AddPopupTransform(_popupTransform);
            
            // Game Over
            ui.AddBehaviour(new GameOverObserver(gameContext, appContext));
            ui.AddGameOverViewPrefab(_gameOverViewPrefab);
        }
    }
}