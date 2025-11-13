using Atomic.Entities;
using ShooterGame.Gameplay;
using TMPro;
using UnityEngine;

namespace ShooterGame.UI
{
    public sealed class GameUIInstaller : SceneEntityInstaller<IGameUI>
    {
        [SerializeField]
        private TMP_Text _countdownView;

        [Header("Kills")]
        [SerializeField]
        private TMP_Text _blueKillsView;
        
        [SerializeField]
        private TMP_Text _redKillsView;

        public override void Install(IGameUI ui)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            ui.AddBehaviour(new CountdownPresenter(_countdownView, gameContext));
            ui.AddBehaviour(new KillsPresenter(_blueKillsView, TeamType.BLUE, gameContext));
            ui.AddBehaviour(new KillsPresenter(_redKillsView, TeamType.RED, gameContext));
        }
    }
}