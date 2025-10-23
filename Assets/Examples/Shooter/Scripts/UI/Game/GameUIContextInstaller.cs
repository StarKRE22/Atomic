using Atomic.Entities;
using TMPro;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class GameUIContextInstaller : SceneEntityInstaller<IGameUIContext>
    {
        [SerializeField]
        private TMP_Text _countdownView;

        [Header("Kills")]
        [SerializeField]
        private TMP_Text _blueKillsView;
        
        [SerializeField]
        private TMP_Text _redKillsView;

        public override void Install(IGameUIContext context)
        {
            context.AddBehaviour(new CountdownPresenter(_countdownView));
            context.AddBehaviour(new KillsPresenter(_blueKillsView, TeamType.BLUE));
            context.AddBehaviour(new KillsPresenter(_redKillsView, TeamType.RED));
        }
    }
}