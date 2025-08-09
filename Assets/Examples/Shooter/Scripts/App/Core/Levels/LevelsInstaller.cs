using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [CreateAssetMenu(
        fileName = "LevelsInstaller",
        menuName = "ShooterGame/New LevelsInstaller"
    )]
    public sealed class LevelsInstaller : ScriptableEntityInstaller<IAppContext>
    {
        [SerializeField]
        private int _currentLevel;

        protected override void Install(IAppContext context)
        {
            context.AddCurrentLevel(new ReactiveVariable<int>(_currentLevel));
            context.AddBehaviour<SaveLevelController>();
        }
    }
}