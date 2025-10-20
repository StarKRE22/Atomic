using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class CombatEntityBaker : IEntityInstaller<IUnitEntity>
    {
        [SerializeField]
        private Optional<float> _fireCooldown = 1;

        [SerializeField]
        private Optional<float> _fireDistance = 5;

        public void Install(IUnitEntity entity)
        {
            if (_fireCooldown) entity.GetFireCooldown().SetDuration(_fireCooldown);
            if (_fireDistance) entity.SetFireDistance(new Const<float>(_fireDistance));
        }
    }
}