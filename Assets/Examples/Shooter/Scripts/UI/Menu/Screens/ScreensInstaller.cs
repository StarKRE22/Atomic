using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

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
            Dictionary<Type, (ScreenView, IEntityBehaviour)> screens = this
                .ProvideScreens()
                .ToDictionary(it => it.Item1.GetType());

            ui.AddScreens(screens);
            ui.AddCurrentScreen(new ReactiveVariable<ScreenView>());
        }

        private (ScreenView, IEntityBehaviour)[] ProvideScreens() => new (ScreenView, IEntityBehaviour)[]
        {
            (_startScreen, new StartScreenPresenter(_startScreen)),
            (_levelScreen, new LevelScreenPresenter(_levelScreen))
        };
    }
}