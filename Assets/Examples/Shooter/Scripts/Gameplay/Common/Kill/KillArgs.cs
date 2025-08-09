using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public struct KillArgs
    {
        public IEntity instigator;
        public IEntity victim;
    }
}