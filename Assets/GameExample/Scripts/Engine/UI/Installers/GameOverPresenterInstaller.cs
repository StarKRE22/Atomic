using System.Collections.Generic;
using Atomic.UI;
using Atomic.UI.Installer;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class GameOverPresenterInstaller : SceneViewControllerInstaller
    {
        [SerializeField]
        private GameOverPresenter gameOverPresenter;

        protected override IEnumerable<IViewController> GetControllers()
        {
            yield return this.gameOverPresenter;
        }
    }
}