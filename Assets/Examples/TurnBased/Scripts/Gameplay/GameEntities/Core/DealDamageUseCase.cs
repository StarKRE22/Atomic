using Atomic.Entities;
using Atomic.Events;

namespace Game.Gameplay
{
    public static class DealDamageUseCase
    {
        public static bool DealDamage(this IGameEntity source, IGameEntity target, int damage,
            IGameContext gameContext) =>
            target.TakeDamage(source, damage, gameContext);

        public static bool TakeDamage(
            this IGameEntity source, object instigator, int damage, IGameContext gameContext
        )
        {
            if (!source.ReduceHealth(damage))
                return false;

            IGameEventBus eventBus = gameContext.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.EntityDamaged, new TakeDamageEventArgs(source, instigator, damage));
            return true;
        }
    }
}