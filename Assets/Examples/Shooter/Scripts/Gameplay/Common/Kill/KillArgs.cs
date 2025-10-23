using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public struct KillArgs
    {
        public IWorldEntity instigator;
        public IWorldEntity victim;
    }
}