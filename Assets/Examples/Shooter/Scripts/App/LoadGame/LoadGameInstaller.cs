using System;
using ShooterGame.Gameplay;
using UnityEngine;

namespace ShooterGame.App
{
    [Serializable]
    public sealed class LoadGameInstaller : IAppContextInstaller
    {
        [SerializeField]
        private string _levelNameFormat = "ShooterGame (Level{0})";

        [Header("GameSystem")]
        [SerializeField]
        private GameContext _gameContextPrefab;

        [SerializeField]
        private GameUI _gameUIContextPrefab;

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