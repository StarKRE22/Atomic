using System.Collections.Generic;
using UnityEngine;

namespace Atomic.UI
{
    [AddComponentMenu("Atomic/UI/View Controller")]
    public sealed class SceneViewController : MonoBehaviour
    {
        [SerializeReference]
        private List<IViewController> controllers;

        private readonly List<IViewUpdate> updates = new();
        private readonly List<IViewFixedUpdate> fixedUpdates = new();
        private readonly List<IViewLateUpdate> lateUpdates = new();

        private bool started;
        
        public T GetController<T>() where T : IViewController
        {
            if (this.controllers == null)
            {
                return default;
            }
            
            for (int i = 0, count = this.controllers.Count; i < count; i++)
            {
                if (this.controllers[i] is T controller)
                {
                    return controller;
                }
            }

            return default;
        }

        public void AddControllers(IEnumerable<IViewController> controllers)
        {
            foreach (IViewController controller in controllers)
            {
                if (controller != null)
                {
                    this.AddController(controller);
                }
            }
        }

        public void AddController<T>() where T : IViewController, new()
        {
            this.AddController(new T());
        }

        public bool AddController(IViewController controller)
        {
            this.controllers ??= new List<IViewController>();

            if (this.controllers.Contains(controller))
            {
                return false;
            }
            
            this.controllers.Add(controller);

            if (this.started)
            {
                if (controller is IViewInit init)
                {
                    init.Init();
                }

                if (controller is IViewEnable enable)
                {
                    enable.Enable();
                }
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

            return true;
        }

        public bool DelController(IViewController controller)
        {
            if (this.controllers == null)
            {
                return false;
            }
            
            if (!this.controllers.Remove(controller))
            {
                return false;
            }
            
            if (controller is IViewUpdate update)
            {
                this.updates.Remove(update);
            }

            if (controller is IViewFixedUpdate fixedUpdate)
            {
                this.fixedUpdates.Remove(fixedUpdate);
            }

            if (controller is IViewLateUpdate lateUpdate)
            {
                this.lateUpdates.Remove(lateUpdate);
            }
            
            if (this.started)
            {
                if (controller is IViewDisable disable)
                {
                    disable.Disable();
                }

                if (controller is IViewDispose init)
                {
                    init.Dispose();
                }
            }

            return true;
        }

        private void Awake()
        {
            if (this.controllers == null)
            {
                return;
            }
            
            foreach (var controller in this.controllers)
            {
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

        private void Start()
        {
            this.Init();
            this.Enable();
            UpdateManager.AddController(this);
            this.started = true;
        }

        private void OnEnable()
        {
            if (this.started)
            {
                this.Enable();
                UpdateManager.AddController(this);
            }
        }

        private void OnDisable()
        {
            if (this.started)
            {
                this.Disable();
                UpdateManager.RemoveController(this);
            }
        }

        private void OnDestroy()
        {
            if (this.started)
            {
                this.Dispose();
            }
        }

        internal void OnUpdate(float deltaTime)
        {
            for (int i = 0, count = this.updates.Count; i < count; i++)
            {
                this.updates[i].OnUpdate(deltaTime);
            }
        }

        internal void OnFixedUpdate(float deltaTime)
        {
            for (int i = 0, count = this.fixedUpdates.Count; i < count; i++)
            {
                this.fixedUpdates[i].OnFixedUpdate(deltaTime);
            }
        }

        internal void OnLateUpdate(float deltaTime)
        {
            for (int i = 0, count = this.lateUpdates.Count; i < count; i++)
            {
                this.lateUpdates[i].OnLateUpdate(deltaTime);
            }
        }

        private void Init()
        {
            if (this.controllers == null)
            {
                return;
            }

            for (int i = 0, count = this.controllers.Count; i < count; i++)
            {
                if (controllers[i] is IViewInit init)
                {
                    init.Init();
                }
            }
        }

        private void Dispose()
        {
            if (this.controllers == null)
            {
                return;
            }
            
            for (int i = 0, count = this.controllers.Count; i < count; i++)
            {
                if (controllers[i] is IViewDispose dispose)
                {
                    dispose.Dispose();
                }
            }

            this.started = false;
        }

        private void Enable()
        {
            if (this.controllers == null)
            {
                return;
            }
            
            for (int i = 0, count = this.controllers.Count; i < count; i++)
            {
                if (this.controllers[i] is IViewEnable enable)
                {
                    enable.Enable();
                }
            }
        }

        private void Disable()
        {
            if (this.controllers == null)
            {
                return;
            }
            
            for (int i = 0, count = this.controllers.Count; i < count; i++)
            {
                if (this.controllers[i] is IViewDisable dispose)
                {
                    dispose.Disable();
                }
            }
        }
    }
}