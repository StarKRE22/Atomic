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
            entity.AddHealth( new ReactiveInt(_health));
            entity.AddMaxHealth( _health);
        }
    }
}