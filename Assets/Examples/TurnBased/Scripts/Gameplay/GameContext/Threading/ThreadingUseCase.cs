using System;
using Atomic.Entities;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public static class ThreadingUseCase
    {
        public static async UniTask RunInBackground(this IGameContext gameContext, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            await UniTask.RunOnThreadPool(action);
            IGameEventBus eventBus = gameContext.GetEventBus();
            eventBus.Flush();
        }
        
        public static async UniTask RunInBackground(this IGameContext gameContext, Func<UniTask> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));
            
            await UniTask.RunOnThreadPool(func);
            IGameEventBus eventBus = gameContext.GetEventBus();
            eventBus.Flush();
        }

        public static void RunInMainThread(this IGameContext gameContext, Action action)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            action.Invoke();
            IGameEventBus eventBus = gameContext.GetEventBus();
            eventBus.Flush();
        }
        
        public static async UniTask<T> RunInBackground<T>(this IGameContext gameContext, Func<T> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            var result = await UniTask.RunOnThreadPool(func);
            IGameEventBus eventBus = gameContext.GetEventBus();
            eventBus.Flush();
            return result;
        }
        
        public static T RunInMainThread<T>(this IGameContext gameContext, Func<T> func)
        {
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            T result = func.Invoke();
            IGameEventBus eventBus = gameContext.GetEventBus();
            eventBus.Flush();
            return result;
        }
    }
}