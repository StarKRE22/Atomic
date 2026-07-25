using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class RangeCombatEntityInstaller
    {
        [SerializeField]
        private Const<float> _fireDistance = 5;

        [SerializeField]
        private float _fireCooldown = 1;

        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);
        
        public void Install(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddFireableTag();
            entity.AddFireCooldown(new Cooldown(_fireCooldown));
            entity.AddFirePoint(new InlineValue<Vector3>(() => entity.GetFirePoint(_fireOffset.Value)));
            entity.AddFireRequest(new Request<IGameEntity>());
            entity.AddFireCommand(new Command<IGameEntity>()
                .AddCondition(_ => entity.IsAlive())
                .AddCondition(_ => entity.GetFireCooldown().IsCompleted())
                .AddAction(target => entity.FireProjectile(target, gameContext))
                .AddAction(_ => entity.GetFireCooldown().ResetTime())
            );
            
            // entity.WhenFixedTick(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_fireDistance);
            // entity.AddBehaviour<FireBehaviour>();
        }
    }
}