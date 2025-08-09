using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;

namespace ShooterGame.App
{
    public static class ScreenUseCase
    {
        public static void ShowScreen<T>(IMenuUIContext context) where T : ScreenView => 
            ShowScreen(context, typeof(T));

        public static void ShowScreen(IMenuUIContext context, Type screenType)
        {
            IDictionary<Type, (ScreenView, IEntityBehaviour)> screens = context.GetScreens();
            IReactiveVariable<ScreenView> currentScreenPtr = context.GetCurrentScreen();
            
            //Hide previous screen
            ScreenView previousScreen = currentScreenPtr.Value;
            if (previousScreen)
            {
                IEntityBehaviour previousPresenter = screens[previousScreen.GetType()].Item2;
                context.DelBehaviour(previousPresenter);
                previousScreen.Hide();
            }
            
            //Show next screen
            (ScreenView nextScreen, IEntityBehaviour nextPresenter) = screens[screenType];
            nextScreen.Show();
            context.AddBehaviour(nextPresenter);
            currentScreenPtr.Value = nextScreen;
        }
    }
}