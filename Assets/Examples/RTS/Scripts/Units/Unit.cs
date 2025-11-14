using Atomic.Entities;

namespace RTSGame
{
    public sealed class Unit : Entity, IUnit
    {
        public Unit(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0
        ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
        {
        }
    }
}