using System;
using System.Collections.Generic;
using System.Linq;
using Atomic.Elements;
using Atomic.Entities;
using ShooterGame.UI;
using UnityEngine;

namespace ShooterGame.App
{
    [Serializable]
    public sealed class ScreensInstaller : SceneEntityInstaller<IMenuUIContext>
    {
        [SerializeField]
        private StartScreenView _startScreen;

        [SerializeField]
        private LevelScreenView _levelScreen;

        protected override void Install(IMenuUIContext entity)
        {
            Dictionary<Type, (ScreenView, IEntityBehaviour)> screens = this
                .ProvideScreens()
                .ToDictionary(it => it.Item1.GetType());

            entity.AddScreens(screens);
            entity.AddCurrentScreen(new ReactiveVariable<ScreenView>());
        }

        private (ScreenView, IEntityBehaviour)[] ProvideScreens() => new (ScreenView, IEntityBehaviour)[]
        {
            (_startScreen, new StartScreenPresenter(_startScreen)),
            (_levelScreen, new LevelScreenPresenter(_levelScreen))
        };
    }
}