using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class EntityBehaviourStub : 
        IEntityInit,
        IEntityEnable,
        IEntityDisable,
        IEntityDispose,
        IEntityUpdate,
        IEntityFixedUpdate,
        IEntityLateUpdate
    {
        public bool initialized;
        public bool enabled;
        public bool disabled;
        public bool disposed;
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;

        public readonly List<string> invokationList = new();

        public void Init(IEntity entity)
        {
            this.initialized = true;
            this.invokationList.Add(nameof(Init));
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

        public void Dispose(IEntity entity)
        {
            this.disposed = true;
            this.invokationList.Add(nameof(Dispose));
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