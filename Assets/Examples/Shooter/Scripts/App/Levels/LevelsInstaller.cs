using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.App
{
    [Serializable]
    public sealed class LevelsInstaller : IAppContextInstaller
    {
        [SerializeField]
        private int _startLevel = 1;

        [SerializeField]
        private int _maxLevel = 9;
        
        [SerializeField]
        private int _currentLevel = 1;

        public void Install(IAppContext context)
        {
            context.AddStartLevel(new Const<int>(_startLevel));
            context.AddMaxLevel(new Const<int>(_maxLevel));
            context.AddCurrentLevel(new ReactiveVariable<int>(_currentLevel));
            context.AddBehaviour<LevelPersistentController>();
        }
    }
}