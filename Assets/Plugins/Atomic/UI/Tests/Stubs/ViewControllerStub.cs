using System;
using System.Collections.Generic;

namespace Atomic.UI
{
    [Serializable]
    public sealed class ViewControllerStub :
        IViewInit,
        IViewEnable,
        IViewDisable,
        IViewDispose,
        IViewUpdate,
        IViewFixedUpdate,
        IViewLateUpdate
    {
        public bool wasInit;
        public bool wasEnable;
        public bool wasDisable;
        public bool wasDispose;
        public bool wasUpdate;
        public bool wasFixedUpdate;
        public bool wasLateUpdate;

        public readonly List<string> approvalSequence = new();

        public void Init()
        {
            this.approvalSequence.Add(nameof(Init));
            this.wasInit = true;
        }

        public void Enable()
        {
            this.approvalSequence.Add(nameof(Enable));
            this.wasEnable = true;
        }

        public void Disable()
        {
            this.approvalSequence.Add(nameof(Disable));
            this.wasDisable = true;
        }

        public void Dispose()
        {
            this.approvalSequence.Add(nameof(Dispose));
            this.wasDispose = true;
        }

        public void OnUpdate(float deltaTime)
        {
            this.approvalSequence.Add(nameof(OnUpdate));
            this.wasUpdate = true;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            this.approvalSequence.Add(nameof(OnFixedUpdate));
            this.wasFixedUpdate = true;
        }

        public void OnLateUpdate(float deltaTime)
        {
            this.approvalSequence.Add(nameof(OnLateUpdate));
            this.wasLateUpdate = true;
        }
    }
}