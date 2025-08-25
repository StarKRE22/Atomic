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
        private Const<float> _attackDistance = 5;
        
        [SerializeField]
        private float _attackCooldown = 1;
        
        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);
        
        [SerializeField]
        private ProjectileFactory _projectileFactory;
        
        public void Install(IGameEntity entity)
        {
            var gameContext = GameContext.Instance;
            
            entity.AddFireCooldown(new Cooldown(_attackCooldown));
            entity.AddFirePoint(new InlineFunction<Vector3>(() => CombatUseCase.GetFirePoint(entity, _fireOffset.Value)));
            entity.AddFireRequest(new BaseRequest<IGameEntity>());
            entity.WhenFixedUpdate(_ =>
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

            entity.WhenFixedUpdate(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_attackDistance);
            entity.AddFireEvent(new BaseEvent<IGameEntity>());
        }
    }
}