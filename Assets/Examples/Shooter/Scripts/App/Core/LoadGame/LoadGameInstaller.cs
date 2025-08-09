using System;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{

    [Serializable]
    public sealed class LoadGameInstaller : IEntityInstaller<IAppContext>
    {
        [SerializeField]
        private string _levelNameFormat = "ShooterGame (Level{0})";

        [Header("GameSystem")]
        [SerializeField]
        private GameObject _gameContextPrefab;

        [SerializeField]
        private GameObject _gameUIContextPrefab;

       public void Install(IAppContext context)
        {
            context.AddGameLoadingAction(new LoadingTaskSequence(
                new LoadLevelSceneTask(_levelNameFormat),
                new SpawnGameObjectsTask(null, _gameContextPrefab, _gameUIContextPrefab)
            ));
        }
    }
}