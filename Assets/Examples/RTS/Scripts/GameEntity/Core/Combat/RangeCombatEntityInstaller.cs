using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class RangeCombatEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _fireDistance = 5;
        
        [SerializeField]
        private float _fireCooldown = 1;
        
        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);
        
        [SerializeField]
        private ProjectileFactory _projectileFactory;
        
        public void Install(IGameEntity entity)
        {
            var gameContext = GameContext.Instance;
            
            entity.AddFireCooldown(new Cooldown(_fireCooldown));
            entity.AddFirePoint(new InlineFunction<Vector3>(() => CombatUseCase.GetFirePoint(entity, _fireOffset.Value)));
            entity.AddFireRequest(new BaseRequest<IGameEntity>());
            entity.WhenFixedTick(_ =>
            {
                if (HealthUseCase.IsAlive(entity) &&
                    entity.GetFireCooldown().IsCompleted() &&
                    entity.GetFireRequest().Consume(out IGameEntity target))
                {
                    CombatUseCase.FireProjectile(entity, _projectileFactory.name, target, gameContext);
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