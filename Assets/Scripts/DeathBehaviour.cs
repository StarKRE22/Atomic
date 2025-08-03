using Atomic.Entities;

namespace SampleGame
{
    public class DeathBehaviour : IEntitySpawn, IEntityDespawn, IEntityActivate, IEntityDeactivate
    {
        public void OnSpawn(IEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void OnDespawn(IEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void OnActivate(IEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public void OnDeactivate(IEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}