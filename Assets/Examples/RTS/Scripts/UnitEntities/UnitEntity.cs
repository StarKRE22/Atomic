using Atomic.Entities;

namespace RTSGame
{
    public sealed class UnitEntity : Entity, IUnitEntity
    {
        public UnitEntity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0
        ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
        {
        }
    }
}