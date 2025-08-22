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
        private float _attackCooldown = 1;
        
        [SerializeField]
        private Const<float> _attackDistance = 1;

        [SerializeField]
        private Const<int> _damage;
        
        public void Install(IGameEntity entity)
        {
            entity.AddDamage(_damage);
            entity.AddFireCooldown(new Cooldown(_attackCooldown));
            entity.AddFireRequest(new BaseRequest<IGameEntity>());
            entity.WhenFixedUpdate(_ =>
            {
                if (HealthUseCase.IsAlive(entity) &&
                    entity.GetFireCooldown().IsCompleted() &&
                    entity.GetFireRequest().Consume(out IGameEntity target))
                {
                    DamageUseCase.DealDamage(entity, target);
                    entity.GetFireCooldown().ResetTime();
                    entity.GetFireEvent().Invoke(target);
                }
            });
            
            entity.WhenFixedUpdate(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_attackDistance);
            entity.AddFireEvent(new BaseEvent<IGameEntity>());
        }
    }
}