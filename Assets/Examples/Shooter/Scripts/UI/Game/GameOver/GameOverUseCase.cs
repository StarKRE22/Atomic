using ShooterGame.App;
using ShooterGame.Gameplay;
using UnityEngine;

namespace ShooterGame.UI
{
    public static class GameOverUseCase
    {
        public static void ShowPopup(IGameUI uiContext, IGameContext gameContext, IAppContext appContext)
        {
            GameOverView viewPrefab = uiContext.GetGameOverViewPrefab();
            Transform parent = uiContext.GetPopupTransform();
            GameOverView view = GameObject.Instantiate(viewPrefab, parent);
            uiContext.AddGameOverView(view);
            uiContext.AddBehaviour(new GameOverPresenter(gameContext, appContext));
        }

        public static void HidePopup(IGameUI uiContext)
        {
            if (uiContext.TryGetGameOverView(out GameOverView view))
            {
                uiContext.DelBehaviour<GameOverPresenter>();
                uiContext.DelGameOverView();
                GameObject.Destroy(view.gameObject);
            }
        }
    }
}