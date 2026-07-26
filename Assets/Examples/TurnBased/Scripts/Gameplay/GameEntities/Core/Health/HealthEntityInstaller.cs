using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class HealthEntityInstaller : IGameEntityInstaller
    {
        [SerializeField]
        private Const<int> _health = 4;
        
        public void Install(IGameEntity entity)
        {
            entity.AddValue(GameEntityAPI.Health, new ReactiveInt(_health));
            entity.AddValue(GameEntityAPI.MaxHealth, _health);
        }
    }
}