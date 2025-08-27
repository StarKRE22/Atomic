using Atomic.Entities;

namespace RTSGame
{
    public sealed class GameContext : Entity, IGameContext
    {
        public static GameContext Instance;

        public GameContext(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0
        ) : base(name, tagCapacity, valueCapacity, behaviourCapacity) => Instance = this;

        protected override void OnDispose() => Instance = null;
    }
}