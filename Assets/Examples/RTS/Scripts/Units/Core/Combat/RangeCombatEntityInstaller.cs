using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class RangeCombatEntityInstaller : IEntityInstaller<IUnit>
    {
        [SerializeField]
        private Const<float> _fireDistance = 5;
        
        [SerializeField]
        private float _fireCooldown = 1;
        
        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);
        
        [SerializeField]
        private ProjectileFactory _projectileFactory;
        
        public void Install(IUnit entity)
        {
            var gameContext = GameContext.Instance;
            
            entity.AddFireCooldown(new Cooldown(_fireCooldown));
            entity.AddFirePoint(new InlineValue<Vector3>(() => CombatUseCase.GetFirePoint(entity, _fireOffset.Value)));
            entity.AddFireRequest(new Request<IUnit>());
            entity.WhenFixedTick(_ =>
            {
                if (LifeUseCase.IsAlive(entity) &&
                    entity.GetFireCooldown().IsCompleted() &&
                    entity.GetFireRequest().Consume(out IUnit target))
                {
                    CombatUseCase.FireProjectile(entity, _projectileFactory.name, target, gameContext);
                    entity.GetFireCooldown().ResetTime();
                    entity.GetFireEvent().Invoke(target);
                }
            });

            entity.WhenFixedTick(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_fireDistance);
            entity.AddFireEvent(new Event<IUnit>());
        }
    }
}