using System;
using Cysharp.Threading.Tasks;

namespace ShooterGame.App
{
    public static class GameLoadingUseCase
    {
        public static void StartGame(IAppContext context)
        {
            int currentLevel = context.GetCurrentLevel().Value;
            StartGame(context, currentLevel);
        }

        public static UniTask StartGame(IAppContext context, int level)
        {
            if (level <= 0 || level > context.GetMaxLevel().Value)
                throw new ArgumentOutOfRangeException(nameof(level));

            var bundle = new LoadingBundle
            {
                {"level", level}
            };
            
            ILoadingTask loadingTask = context.GetGameLoadingAction();
            return loadingTask.Invoke(context, bundle);
        }
    }
}