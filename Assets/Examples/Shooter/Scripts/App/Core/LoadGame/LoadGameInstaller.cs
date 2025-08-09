using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [CreateAssetMenu(
        fileName = "LoadGameInstaller",
        menuName = "ShooterGame/New LoadGameInstaller"
    )]
    public sealed class LoadGameInstaller : ScriptableEntityInstaller<IAppContext>
    {
        [SerializeField]
        private string _levelNameFormat = "ShooterGame (Level{0})";

        [Header("GameSystem")]
        [SerializeField]
        private GameObject _gameContextPrefab;

        [SerializeField]
        private GameObject _gameUIContextPrefab;

        protected override void Install(IAppContext context)
        {
            context.AddGameLoadingAction(new LoadingTaskSequence(
                new LoadLevelSceneTask(_levelNameFormat),
                new SpawnGameObjectsTask(null, _gameContextPrefab, _gameUIContextPrefab)
            ));
        }
    }
}