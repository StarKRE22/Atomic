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

        public readonly List<string> invokationList = new();

        public void OnSpawn(IEntity entity)
        {
            this.spawned = true;
            this.invokationList.Add(nameof(OnSpawn));
        }

        public void OnActivate(IEntity entity)
        {
            this.enabled = true;
            this.invokationList.Add(nameof(OnActivate));
        }

        public void OnDeactivate(IEntity entity)
        {
            this.disabled = true;
            this.invokationList.Add(nameof(OnDeactivate));
        }

        public void OnDespawn(IEntity entity)
        {
            this.despawned = true;
            this.invokationList.Add(nameof(OnDespawn));
        }

        public virtual void OnUpdate(IEntity entity, float deltaTime)
        {
            this.updated = true;
            this.invokationList.Add(nameof(OnUpdate));
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            this.fixedUpdated = true;
            this.invokationList.Add(nameof(OnFixedUpdate));
        }

        public void OnLateUpdate(IEntity entity, float deltaTime)
        {
            this.lateUpdated = true;
            this.invokationList.Add(nameof(OnLateUpdate));
        }
    }
}