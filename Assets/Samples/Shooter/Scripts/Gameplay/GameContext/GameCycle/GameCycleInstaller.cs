using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    [Serializable]
    public sealed class GameCycleInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private float _gameDuration = 20;
        
        public void Install(IGameContext context)
        {
            context.AddGameTime(new ReactiveFloat(_gameDuration));
            context.AddBehaviour<GameCycleController>();    
        }
    }
}