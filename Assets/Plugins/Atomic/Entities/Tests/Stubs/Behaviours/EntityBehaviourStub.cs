using System.Collections.Generic;

namespace Atomic.Entities
{
    public class EntityBehaviourStub :
        IEntitySpawn,
        IEntityActivate,
        IEntityDeactivate,
        IEntityDespawn,
        IEntityUpdate,
        IEntityFixedUpdate,
        IEntityLateUpdate
    {
        public bool spawned;
        public bool enabled;
        public bool disabled;
        public bool despawned;
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;

        public readonly List<string> invocationList = new();

        public void OnSpawn(IEntity entity)
        {
            this.spawned = true;
            this.invocationList.Add(nameof(OnSpawn));
        }

        public void OnActivate(IEntity entity)
        {
            this.enabled = true;
            this.invocationList.Add(nameof(OnActivate));
        }

        public void OnDeactivate(IEntity entity)
        {
            this.disabled = true;
            this.invocationList.Add(nameof(OnDeactivate));
        }

        public void OnDespawn(IEntity entity)
        {
            this.despawned = true;
            this.invocationList.Add(nameof(OnDespawn));
        }

        public virtual void OnUpdate(IEntity entity, float deltaTime)
        {
            this.updated = true;
            this.invocationList.Add(nameof(OnUpdate));
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            this.fixedUpdated = true;
            this.invocationList.Add(nameof(OnFixedUpdate));
        }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            this.lateUpdated = true;
            this.invocationList.Add(nameof(OnLateUpdate));
        }
    }
}