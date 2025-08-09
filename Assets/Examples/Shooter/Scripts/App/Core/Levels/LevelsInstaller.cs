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
        private int _currentLevel;
        
        public void Install(IAppContext context)
        {
            context.AddCurrentLevel(new ReactiveVariable<int>(_currentLevel));
            context.AddBehaviour<SaveLevelController>();
        }
    }
}