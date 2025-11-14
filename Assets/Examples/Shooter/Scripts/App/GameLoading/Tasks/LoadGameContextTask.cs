using Cysharp.Threading.Tasks;
using ShooterGame.Gameplay;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class LoadGameContextTask : ILoadingTask
    {
        private readonly GameContext _prefab;

        public LoadGameContextTask(GameContext prefab)
        {
            _prefab = prefab;
        }

        public UniTask Invoke(IAppContext context, LoadingBundle bundle)
        {
            GameContext gameContext = GameObject.Instantiate(_prefab);
            gameContext.AddBehaviour(new GameCompletionObserver(context));
            return UniTask.CompletedTask;
        }
    }
}