using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class CombatEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private float _fireCooldown = 1;
        
        [SerializeField]
        private Const<float> _fireDistance = 5;
        
        public void Install(IGameEntity entity)
        {
            entity.GetFireCooldown().SetDuration(_fireCooldown);
            entity.SetFireDistance(_fireDistance);
        }
    }
}