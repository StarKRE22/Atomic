using System.Collections.Generic;
using System.Text;

namespace Atomic.Contexts
{
    public sealed class CommonSystemStub : 
        IContextInit,
        IContextEnable,
        IContextDisable,
        IContextUpdate,
        IContextFixedUpdate,
        IContextLateUpdate,
        IContextDispose
    {
        public bool initialized;
        public bool enabled;
        public bool disabled;
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;
        public bool destroyed;

        public readonly StringBuilder flowQueue = new();

        public void Init(IContext context)
        {
            this.initialized = true;
            this.flowQueue.Append("I");
        }

        public void Enable(IContext context)
        {
            this.enabled = true;
            this.flowQueue.Append("E");
        }

        public void Disable(IContext context)
        {
            this.disabled = true;
            this.flowQueue.Append("D");
        }

        public void Update(IContext context, float deltaTime)
        {
            this.updated = true;
            this.flowQueue.Append("U");
        }

        public void FixedUpdate(IContext context, float deltaTime)
        {
            this.fixedUpdated = true;
            this.flowQueue.Append("F");
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            this.lateUpdated = true;
            this.flowQueue.Append("L");
        }

        public void Dispose(IContext context)
        {
            this.destroyed = true;
            this.flowQueue.Append("X");
        }
    }
}