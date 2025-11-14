using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using AppContext = ShooterGame.App.AppContext;

namespace ShooterGame.UI
{
    [Serializable]
    public sealed class ScreensInstaller : SceneEntityInstaller<IMenuUI>
    {
        [SerializeField]
        private StartScreenView _startScreen;

        [SerializeField]
        private LevelScreenView _levelScreen;

        public override void Install(IMenuUI ui)
        {
            AppContext.TryGetInstance(out AppContext appContext);

            Dictionary<Type, (ScreenView, IEntityBehaviour)> screens = this
                .ProvideScreens(appContext)
                .ToDictionary(it => it.Item1.GetType());

            ui.AddScreens(screens);
            ui.AddCurrentScreen(new ReactiveVariable<ScreenView>());
        }

        private (ScreenView, IEntityBehaviour)[] ProvideScreens(AppContext appContext) =>
            new (ScreenView, IEntityBehaviour)[]
            {
                (_startScreen, new StartScreenPresenter(_startScreen, appContext)),
                (_levelScreen, new LevelScreenPresenter(_levelScreen, appContext))
            };
    }
}