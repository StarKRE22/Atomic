using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public static class TakeDamageUseCase
    {
        public static bool TakeDamage(Collider collider, DamageArgs args, IGameContext context = null) => 
            collider.TryGetComponent(out IGameEntity target) && TakeDamage(target, args, context);

        public static bool TakeDamage(IGameEntity target, DamageArgs args, IGameContext context = null)
        {
            if (!target.HasDamageableTag())
                return false;

            Health health = target.GetHealth();
            if (!health.Reduce(args.damage))
                return false;

            target.GetTakeDamageEvent().Invoke(args);
            
            if (!health.IsEmpty())
                return true;

            target.GetTakeDeathEvent().Invoke(args);

            context?.GetKillEvent().Invoke(new KillArgs
            {
                instigator = args.source,
                victim = target
            });

            return true;
        }
    }
}