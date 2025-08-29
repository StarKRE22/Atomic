using System;
using Atomic.Entities;
using ShooterGame.Gameplay;
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
        private GameContext _gameContextPrefab;

        [SerializeField]
        private GameUIContext _gameUIContextPrefab;

       public void Install(IAppContext context)
        {
            context.AddGameLoadingAction(new LoadingTaskSequence(
                new LoadLevelSceneTask(_levelNameFormat),
                new LoadGameContextTask(_gameContextPrefab),
                new LoadGameUIContextTask(_gameUIContextPrefab)
            ));
        }
    }
}