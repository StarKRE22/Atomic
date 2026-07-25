using System;
using Atomic.Elements;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityInstaller
    {
        [SerializeField]
        private int _health;
        
        public void Install(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health));
            entity.AddTakeDamageEvent(new Event<int>());
            entity.AddBehaviour(new LifeBehaviour(gameContext));
        }
    }
}