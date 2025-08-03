using System.Collections.Generic;

namespace Atomic.Entities
{
    public class DummyEntityBehaviour :
        IEntitySpawned,
        IEntityActive,
        IEntityInactive,
        IEntityDespawned,
        IEntityUpdate,
        IEntityFixedUpdate,
        IEntityLateUpdate
    {
        public bool Spawned;
        public bool Activated;
        public bool Deactivated;
        public bool Despawned;
        public bool Updated;
        public bool FixedUpdated;
        public bool LateUpdated;

        public readonly List<string> InvocationList = new();

        public void OnSpawn(IEntity entity)
        {
            this.Spawned = true;
            this.InvocationList.Add(nameof(OnSpawn));
        }

        public void OnActive(IEntity entity)
        {
            this.Activated = true;
            this.InvocationList.Add(nameof(OnActive));
        }

        public void OnInactive(IEntity entity)
        {
            this.Deactivated = true;
            this.InvocationList.Add(nameof(OnInactive));
        }

        public void OnDespawn(IEntity entity)
        {
            this.Despawned = true;
            this.InvocationList.Add(nameof(OnDespawn));
        }

        public virtual void OnUpdate(IEntity entity, float deltaTime)
        {
            this.Updated = true;
            this.InvocationList.Add(nameof(OnUpdate));
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            this.FixedUpdated = true;
            this.InvocationList.Add(nameof(OnFixedUpdate));
        }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            this.LateUpdated = true;
            this.InvocationList.Add(nameof(OnLateUpdate));
        }
    }
}