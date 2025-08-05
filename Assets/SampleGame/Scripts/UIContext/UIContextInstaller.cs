using Atomic.Entities;
using UnityEngine;

namespace SampleGame
{
    public sealed class UIContextInstaller : SceneEntityInstaller<IUIContext>
    {
        [SerializeField]
        private MoneyView _moneyView;

        [SerializeField]
        private GameCountdownView _countdownView;

        [SerializeField]
        private TeamType _teamType = TeamType.BLUE;
        
        
        protected override void Install(IUIContext context)
        {
            //Money:
            context.AddMoneyView(_moneyView);
            context.AddBehaviour(new MoneyPresenter(_teamType));
            
            //Countdown:
            context.AddGameCountdownView(_countdownView);
            context.AddBehaviour<GameCountdownPresenter>();
        }
        
    }
}