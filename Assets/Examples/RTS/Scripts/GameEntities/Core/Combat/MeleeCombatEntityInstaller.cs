using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MeleeCombatEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private float _fireCooldown = 1;
        
        [SerializeField]
        private Const<float> _fireDistance = 1;

        [SerializeField]
        private Const<int> _damage;
        
        public void Install(IGameEntity entity)
        {
            entity.AddDamage(_damage);
            entity.AddFireableTag();
            entity.AddFireCooldown(new Cooldown(_fireCooldown));
            entity.AddFireRequest(new Request<IGameEntity>());
            entity.AddFireCommand(new Command<IGameEntity>()
                .AddCondition(_ => entity.IsAlive())
                .AddCondition(_ => entity.GetFireCooldown().IsCompleted())
                .AddAction(target => entity.DealDamage(target))
                .AddAction(_ => entity.GetFireCooldown().ResetTime())
            );
            
            // entity.WhenFixedTick(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_fireDistance);
            // entity.AddBehaviour<FireBehaviour>();
        }
    }
}