using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [Serializable]
    public sealed class LevelsInstaller : IEntityInstaller<IAppContext>
    {
        [SerializeField]
        private Const<int> _startLevel = 1;

        [SerializeField]
        private Const<int> _maxLevel = 9;
        
        [SerializeField]
        private ReactiveVariable<int> _currentLevel = 1;

        public void Install(IAppContext context)
        {
            context.AddStartLevel(_startLevel);
            context.AddMaxLevel(_maxLevel);
            context.AddCurrentLevel(_currentLevel);
            context.AddBehaviour<LevelPersistentController>();
        }
    }
}