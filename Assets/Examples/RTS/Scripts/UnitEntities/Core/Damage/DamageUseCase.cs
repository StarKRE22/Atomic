
namespace RTSGame
{
    public static class DamageUseCase
    {
        public static bool DealDamage(IUnitEntity source, IUnitEntity target) => 
            TakeDamage(target, source.GetDamage().Value);

        public static bool TakeDamage(IUnitEntity target, int damage)
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