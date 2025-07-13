using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class BehaviourStub :
        IInit,
        IEnable,
        IDisable,
        IDispose,
        IUpdate,
        IFixedUpdate,
        ILateUpdate
    {
        public bool initialized;
        public bool enabled;
        public bool disabled;
        public bool disposed;
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;

        public readonly List<string> invokationList = new();

        public void Init(in IEntity entity)
        {
            this.initialized = true;
            this.invokationList.Add(nameof(Init));
        }

        public void Enable(in IEntity entity)
        {
            this.enabled = true;
            this.invokationList.Add(nameof(Enable));
        }

        public void Disable(in IEntity entity)
        {
            this.disabled = true;
            this.invokationList.Add(nameof(Disable));
        }

        public void Dispose(in IEntity entity)
        {
            this.disposed = true;
            this.invokationList.Add(nameof(Dispose));
        }

        public void OnUpdate(in IEntity entity, in float deltaTime)
        {
            this.updated = true;
            this.invokationList.Add(nameof(OnUpdate));
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            this.fixedUpdated = true;
            this.invokationList.Add(nameof(OnFixedUpdate));
        }

        public void OnLateUpdate(in IEntity entity, in float deltaTime)
        {
            this.lateUpdated = true;
            this.invokationList.Add(nameof(OnLateUpdate));
        }
    }
}