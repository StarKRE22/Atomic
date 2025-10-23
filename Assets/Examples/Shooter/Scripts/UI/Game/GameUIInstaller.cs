using Atomic.Entities;
using TMPro;
using UnityEngine;

namespace ShooterGame.Gameplay
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
            ui.AddBehaviour(new CountdownPresenter(_countdownView));
            ui.AddBehaviour(new KillsPresenter(_blueKillsView, TeamType.BLUE));
            ui.AddBehaviour(new KillsPresenter(_redKillsView, TeamType.RED));
        }
    }
}