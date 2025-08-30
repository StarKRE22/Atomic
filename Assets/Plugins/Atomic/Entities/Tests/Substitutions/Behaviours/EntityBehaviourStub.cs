using System.Collections.Generic;

namespace Atomic.Entities
{
    public class DummyEntityBehaviour :
        IEntityInit,
        IEntityEnable,
        IEntityDisable,
        IEntityDispose,
        IEntityUpdate,
        IEntityFixedUpdate,
        IEntityLateUpdate
    {
        public bool Initialized;
        public bool Enabled;
        public bool Disabled;
        public bool Disposed;
        public bool Updated;
        public bool FixedUpdated;
        public bool LateUpdated;

        public readonly List<string> InvocationList = new();

        public void Init(IEntity entity)
        {
            this.Initialized = true;
            this.InvocationList.Add(nameof(Init));
        }

        public void Enable(IEntity entity)
        {
            this.Enabled = true;
            this.InvocationList.Add(nameof(Enable));
        }

        public void Disable(IEntity entity)
        {
            this.Disabled = true;
            this.InvocationList.Add(nameof(Disable));
        }

        public void Dispose(IEntity entity)
        {
            this.Disposed = true;
            this.InvocationList.Add(nameof(Dispose));
        }

        public virtual void Update(IEntity entity, float deltaTime)
        {
            this.Updated = true;
            this.InvocationList.Add(nameof(Update));
        }

        public void FixedUpdate(IEntity entity, float deltaTime)
        {
            this.FixedUpdated = true;
            this.InvocationList.Add(nameof(FixedUpdate));
        }

        public void LateUpdate(IEntity entity, float deltaTime)
        {
            this.LateUpdated = true;
            this.InvocationList.Add(nameof(LateUpdate));
        }
    }
}