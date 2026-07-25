using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public abstract class FixedTickGameEntitySystem : FixedTickEntitySystem<IGameContext, IGameEntity>
    {
        [SerializeField]
        private EntityUpdateSettings _settings;
        
        protected override EntityUpdateSettings ProvideUpdateSettings(IGameContext context) => 
            _settings;

    }
}