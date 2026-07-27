using Atomic.Elements;
using Atomic.Entities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShooterGame.App
{
    [GenerateEntityExtensionsAPI]
    public static partial class AppContextAPI
    {
        public static readonly ValueKey<IAppContext, IValue<KeyCode>> ExitKeyCode = new(nameof(ExitKeyCode));
        public static readonly ValueKey<IAppContext, IValue<int>> StartLevel = new(nameof(StartLevel));
        public static readonly ValueKey<IAppContext, IValue<int>> MaxLevel = new(nameof(MaxLevel));
        public static readonly ValueKey<IAppContext, IReactiveVariable<int>> CurrentLevel = new(nameof(CurrentLevel));
        public static readonly ValueKey<IAppContext, ILoadingTask> GameLoadingAction = new(nameof(GameLoadingAction));
    }
}
