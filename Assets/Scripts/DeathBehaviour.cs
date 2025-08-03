using Atomic.Entities;

namespace SampleGame
{
    public class DeathBehaviour : IEntityActive, IEntityInactive, IEntitySpawned, IEntityDespawned
    {
        public void OnActive(IEntity entity)
        {
        }

        public void OnInactive(IEntity entity)
        {
        }

        public void OnSpawn(IEntity entity)
        {
        }

        public void OnDespawn(IEntity entity)
        {
        }
    }
}