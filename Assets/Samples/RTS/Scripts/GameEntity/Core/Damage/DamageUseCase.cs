
namespace RTSGame
{
    public static class DamageUseCase
    {
        public static bool DealDamage(IGameEntity source, IGameEntity target) => 
            TakeDamage(target, source.GetDamage().Value);

        public static bool TakeDamage(IGameEntity target, int damage)
        {
            if (!target.HasDamageableTag())
                return false;

            Health health = target.GetHealth();
            if (health.IsEmpty() || !health.Reduce(damage)) 
                return false;
            
            target.GetTakeDamageEvent().Invoke(damage);
            return true;
        }
    }
}