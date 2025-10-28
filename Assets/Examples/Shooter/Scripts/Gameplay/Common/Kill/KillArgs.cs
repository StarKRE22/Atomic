using Atomic.Entities;

namespace ShooterGame.Gameplay
{
    public struct KillArgs
    {
        public IActor instigator;
        public IActor victim;
    }
}