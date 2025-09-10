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
            entity.AddFireCooldown(new Cooldown(_fireCooldown));
            entity.AddFireRequest(new BaseRequest<IGameEntity>());
            entity.WhenFixedTick(_ =>
            {
                if (LifeUseCase.IsAlive(entity) &&
                    entity.GetFireCooldown().IsCompleted() &&
                    entity.GetFireRequest().Consume(out IGameEntity target))
                {
                    DamageUseCase.DealDamage(entity, target);
                    entity.GetFireCooldown().ResetTime();
                    entity.GetFireEvent().Invoke(target);
                }
            });
            
            entity.WhenFixedTick(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_fireDistance);
            entity.AddFireEvent(new BaseEvent<IGameEntity>());
        }
    }
}