using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private int _health;
        
        public void Install(IGameEntity entity)
        {
            IGameContext gameContext = GameContext.Instance;
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health));
            entity.AddTakeDamageEvent(new BaseEvent<int>());
            entity.AddBehaviour(new LifeBehaviour(gameContext));
        }
    }
}