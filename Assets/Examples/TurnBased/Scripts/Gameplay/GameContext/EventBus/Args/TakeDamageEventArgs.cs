using Atomic.Entities;

namespace Game.Gameplay
{
    public readonly struct TakeDamageEventArgs
    {
        public readonly IGameEntity victim;
        public readonly object instigator;
        public readonly int damage;
        public readonly int health; 
        
        public TakeDamageEventArgs(IGameEntity victim, object instigator, int damage)
        {
            this.victim = victim;
            this.health = victim.GetValue(GameEntityAPI.Health).Value;
            this.instigator = instigator;
            this.damage = damage;
        }
    }
}