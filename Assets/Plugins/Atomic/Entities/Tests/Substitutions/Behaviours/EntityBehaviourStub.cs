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
        public bool Spawned;
        public bool Activated;
        public bool Deactivated;
        public bool Despawned;
        public bool Updated;
        public bool FixedUpdated;
        public bool LateUpdated;

        public readonly List<string> InvocationList = new();

        public void Init(IEntity entity)
        {
            this.Spawned = true;
            this.InvocationList.Add(nameof(Init));
        }

        public void Enable(IEntity entity)
        {
            this.Activated = true;
            this.InvocationList.Add(nameof(Enable));
        }

        public void Disable(IEntity entity)
        {
            this.Deactivated = true;
            this.InvocationList.Add(nameof(Disable));
        }

        public void Dispose(IEntity entity)
        {
            this.Despawned = true;
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