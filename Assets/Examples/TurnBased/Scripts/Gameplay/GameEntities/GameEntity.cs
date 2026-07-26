using Atomic.Entities;

namespace Game.Gameplay
{
    public sealed class GameEntity : Entity, IGameEntity
    {
        public GameEntity(string name = null, int tagCapacity = 0, int valueCapacity = 0, int behaviourCapacity = 0, Settings? settings = null) : base(name, tagCapacity, valueCapacity, behaviourCapacity, settings)
        {
        }
    }
}