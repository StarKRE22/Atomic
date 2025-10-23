using Cysharp.Threading.Tasks;
using ShooterGame.Gameplay;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class LoadGameUIContextTask : ILoadingTask
    {
        private readonly GameUI _prefab;
        
        public LoadGameUIContextTask(GameUI prefab)
        {
            _prefab = prefab;
        }

        public UniTask Invoke(IAppContext context, LoadGameBundle bundle)
        {
            GameObject.Instantiate(_prefab);
            return UniTask.CompletedTask;
        }
    }
}