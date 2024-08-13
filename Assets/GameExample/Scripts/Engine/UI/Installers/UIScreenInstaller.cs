using System.Collections.Generic;
using Atomic.UI;
using Atomic.UI.Installer;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class UIScreenInstaller : SceneViewControllerInstaller
    {
        [SerializeField]
        private MoneyPresenter moneyPresenter;

        [SerializeField]
        private GameTimePresenter gameTimePresenter;

        [SerializeField]
        private GameOverPopupShower gameOverPopupShower;
        
        protected override IEnumerable<IViewController> GetControllers()
        {
            yield return this.moneyPresenter;
            yield return this.gameTimePresenter;
            yield return this.gameOverPopupShower;
        }
    }
}


