using Atomic.Elements;
using Atomic.Entities;
using System;
using System.Collections.Generic;

namespace ShooterGame.UI
{
    [EntityAPI]
    public static partial class MenuUIAPI
    {
        public static readonly ValueKey<IMenuUI, IDictionary<Type, (ScreenView, IEntityBehaviour)>> Screens =
            new(nameof(Screens));

        public static readonly ValueKey<IMenuUI, IReactiveVariable<ScreenView>> CurrentScreen = new(nameof(CurrentScreen));
    }
}
