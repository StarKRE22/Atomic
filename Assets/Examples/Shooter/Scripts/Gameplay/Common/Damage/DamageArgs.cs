using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public struct DamageArgs
    {
        public IActor source;
        public int damage;
    }
}