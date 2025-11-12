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
        private ReactiveFloat _gameDuration = 20;
        
        public void Install(IGameContext context)
        {
            context.AddGameTime(_gameDuration);
            context.AddBehaviour<GameCycleController>();    
        }
    }
}