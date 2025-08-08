using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class UIContextInstaller : SceneEntityInstaller<IUIContext>
    {
        [SerializeField]
        private MoneyView _moneyView;

        [SerializeField]
        private CountdownView _countdownView;

        [SerializeField]
        private TeamType _teamType = TeamType.BLUE;

        [SerializeField]
        private Transform _popupTransform;

        [SerializeField]
        private GameOverView _gameOverViewPrefab;
        
        protected override void Install(IUIContext context)
        {
            //Base:
            context.AddPopupTransform(_popupTransform);
            
            //Money:
            context.AddMoneyView(_moneyView);
            context.AddBehaviour(new MoneyPresenter(_teamType));
            
            //Countdown:
            context.AddGameCountdownView(_countdownView);
            context.AddBehaviour<CountdownPresenter>();
            
            //GameOver:
            context.AddBehaviour<GameOverObserver>();
            context.AddGameOverViewPrefab(_gameOverViewPrefab);
        }
    }
}