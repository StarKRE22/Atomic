using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class EntityBehaviourStub :
        IEntitySpawn,
        IEntityEnable,
        IEntityDisable,
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

        public void Spawn(IEntity entity)
        {
            this.spawned = true;
            this.invokationList.Add(nameof(Spawn));
        }

        public void Enable(IEntity entity)
        {
            this.enabled = true;
            this.invokationList.Add(nameof(Enable));
        }

        public void Disable(IEntity entity)
        {
            this.disabled = true;
            this.invokationList.Add(nameof(Disable));
        }

        public void Despawn(IEntity entity)
        {
            this.despawned = true;
            this.invokationList.Add(nameof(Despawn));
        }

        public void OnUpdate(IEntity entity, float deltaTime)
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