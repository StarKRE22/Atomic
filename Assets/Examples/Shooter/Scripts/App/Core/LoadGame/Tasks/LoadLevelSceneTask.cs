using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ShooterGame.App
{
    public sealed class LoadLevelSceneTask : ILoadingTask
    {
        private readonly string _levelNameFormat;

        public LoadLevelSceneTask(string levelNameFormat = null)
        {
            _levelNameFormat = levelNameFormat;
        }

        public async UniTask Invoke(IAppContext context, LoadingBundle bundle)
        {
            int level = bundle.Get<int>("level");
            string sceneName = string.Format(_levelNameFormat, level);
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}