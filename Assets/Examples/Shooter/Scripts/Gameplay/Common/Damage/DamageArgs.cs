using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public struct DamageArgs
    {
        public IWorldEntity source;
        public int damage;
    }
}