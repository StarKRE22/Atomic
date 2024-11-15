using System;
using System.Collections.Generic;
using UnityEngine;
// ReSharper disable NotAccessedField.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.UI
{
    [Serializable]
    public sealed class ViewControllerGroup :
        IViewInit,
        IViewEnable,
        IViewDisable,
        IViewDispose,
        IViewUpdate,
        IViewFixedUpdate,
        IViewLateUpdate
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f), HideLabel]
#endif
        [SerializeField]
        private string name;
        
        [Space]
        [SerializeReference]
        private IViewController[] controllers = default;
        
        private readonly List<IViewUpdate> updates = new();
        private readonly List<IViewFixedUpdate> fixedUpdates = new();
        private readonly List<IViewLateUpdate> lateUpdates = new();

        public void Init()
        {
            foreach (IViewController controller in this.controllers)
            {
                if (controller is IViewInit init)
                {
                    init.Init();
                }
                
                if (controller is IViewUpdate update)
                {
                    this.updates.Add(update);
                }

                if (controller is IViewFixedUpdate fixedUpdate)
                {
                    this.fixedUpdates.Add(fixedUpdate);
                }

                if (controller is IViewLateUpdate lateUpdate)
                {
                    this.lateUpdates.Add(lateUpdate);
                }
            }
        }

        public void Enable()
        {
            foreach (IViewController controller in this.controllers)
            {
                if (controller is IViewEnable enable)
                {
                    enable.Enable();
                }
            }
        }

        public void Disable()
        {
            foreach (IViewController controller in this.controllers)
            {
                if (controller is IViewDisable disable)
                {
                    disable.Disable();
                }
            }
        }

        public void Dispose()
        {
            foreach (IViewController controller in this.controllers)
            {
                if (controller is IViewDispose dispose)
                {
                    dispose.Dispose();
                }
            }
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0, count = this.updates.Count; i < count; i++)
            {
                this.updates[i].OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime)
        {
            for (int i = 0, count = this.fixedUpdates.Count; i < count; i++)
            {
                this.fixedUpdates[i].OnFixedUpdate(deltaTime);
            }
        }

        public void OnLateUpdate(float deltaTime)
        {
            for (int i = 0, count = this.lateUpdates.Count; i < count; i++)
            {
                this.lateUpdates[i].OnLateUpdate(deltaTime);
            }
        }
    }
}