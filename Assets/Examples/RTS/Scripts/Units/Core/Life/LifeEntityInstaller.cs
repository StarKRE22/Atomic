using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityInstaller : IEntityInstaller<IUnit>
    {
        [SerializeField]
        private int _health;
        
        public void Install(IUnit entity)
        {
            IGameContext gameContext = GameContext.Instance;
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health));
            entity.AddTakeDamageEvent(new Event<int>());
            entity.AddBehaviour(new LifeBehaviour(gameContext));
        }
    }
}