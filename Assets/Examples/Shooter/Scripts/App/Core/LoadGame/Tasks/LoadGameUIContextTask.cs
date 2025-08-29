using Cysharp.Threading.Tasks;
using ShooterGame.Gameplay;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class LoadGameUIContextTask : ILoadingTask
    {
        private readonly GameUIContext _prefab;
        
        public LoadGameUIContextTask(GameUIContext prefab)
        {
            _prefab = prefab;
        }

        public UniTask Invoke(IAppContext context, LoadingBundle bundle)
        {
            GameObject.Instantiate(_prefab);
            return UniTask.CompletedTask;
        }
    }
}