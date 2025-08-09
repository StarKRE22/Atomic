using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ShooterGame.App
{
    public sealed class SpawnGameObjectsTask : ILoadingTask
    {
        private readonly Transform _parent;
        private readonly GameObject[] _prefabs;

        public SpawnGameObjectsTask(Transform parent = null, params GameObject[] prefabs)
        {
            _parent = parent;
            _prefabs = prefabs;
        }

        public UniTask Invoke(IAppContext context, LoadingBundle bundle)
        {
            for (int i = 0, count = _prefabs.Length; i < count; i++)
            {
                GameObject prefab = _prefabs[i];
                GameObject.Instantiate(prefab, _parent);
            }
            
            return UniTask.CompletedTask;
        }
    }
}