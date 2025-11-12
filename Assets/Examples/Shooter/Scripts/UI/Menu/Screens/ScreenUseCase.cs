using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.UI
{
    //Instead of Screen Manager
    public static class ScreenUseCase
    {
        public static void ShowScreen<T>(IMenuUI context) where T : ScreenView =>
            ShowScreen(context, typeof(T));

        public static void ShowScreen(IMenuUI context, Type screenType)
        {
            IDictionary<Type, (ScreenView, IEntityBehaviour)> screens = context.GetScreens();
            IReactiveVariable<ScreenView> currentScreenPtr = context.GetCurrentScreen();

            //Hide previous screen
            ScreenView previousScreen = currentScreenPtr.Value;
            if (previousScreen)
            {
                Type previousScreenType = previousScreen.GetType();
                IEntityBehaviour previousPresenter = screens[previousScreenType].Item2;
                previousScreen.Hide();
                context.DelBehaviour(previousPresenter);
            }

            //Show next screen
            (ScreenView nextScreen, IEntityBehaviour nextPresenter) = screens[screenType];
            context.AddBehaviour(nextPresenter);
            nextScreen.Show();
            currentScreenPtr.Value = nextScreen;
        }
    }
}