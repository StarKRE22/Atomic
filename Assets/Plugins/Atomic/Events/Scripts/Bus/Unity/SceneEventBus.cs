using System;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    [AddComponentMenu("Atomic/Events/Event Bus")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public partial class SceneEventBus : MonoBehaviour, IEventBus
    {
        public bool Installed
        {
            get { return this.installed; }
        }

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled, the Install() method will be called on Awake()")]
        [SerializeField]
        private bool installOnAwake = true;

#if ODIN_INSPECTOR
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 12)]
        [GUIColor(1f, 0.92156863f, 0.015686275f)]
        [HideInPlayMode]
        [InfoBox(
            "WARINING: If you create Unity objects or another heavy objects in the Install() method, be sure to turn off!",
            InfoMessageType.Warning,
            nameof(installInEditMode))
        ]
#endif
        [Tooltip(
            "If this option is enabled, the Install() method will be called every time OnValidate is called in Edit Mode")]
        [SerializeField]
        private bool installInEditMode;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Tooltip("Specify the installers that will put values and systems to this context")]
        [Space, SerializeField]
        private List<SceneEventBusInstaller> installers;

        public IReadOnlyCollection<int> DeclaredEvents => _eventBus.DeclaredEvents;

        private readonly EventBus _eventBus = new();
        private bool installed;

        protected virtual void Awake()
        {
            if (this.installOnAwake) this.Install();
        }

        protected virtual void OnDestroy()
        {
            _eventBus.Dispose();
        }

        public void Install()
        {
            if (this.installed)
                return;

            this.InstallInternal();
            this.installed = true;
        }

        private void InstallInternal()
        {
            if (this.installers == null)
                return;

            for (int i = 0, count = this.installers.Count; i < count; i++)
            {
                SceneEventBusInstaller installer = this.installers[i];
                if (installer != null) installer.Install(this);
                else Debug.LogWarning("SceneEventBus: Ops! Detected null installer!", this);
            }
        }

        public void Declare(in int key) =>
            _eventBus.Declare(in key);

        public void Declare<T>(in int key) =>
            _eventBus.Declare<T>(in key);

        public void Declare<T1, T2>(in int key) =>
            _eventBus.Declare<T1, T2>(in key);

        public void Declare<T1, T2, T3>(in int key) =>
            _eventBus.Declare<T1, T2, T3>(in key);

        public Action Subscribe(in int key, in Action action) =>
            _eventBus.Subscribe(in key, in action);

        public Action<T> Subscribe<T>(in int key, in Action<T> action) =>
            _eventBus.Subscribe(in key, in action);

        public Action<T1, T2> Subscribe<T1, T2>(in int key, in Action<T1, T2> action) =>
            _eventBus.Subscribe(in key, in action);

        public Action<T1, T2, T3> Subscribe<T1, T2, T3>(in int key, in Action<T1, T2, T3> action) =>
            _eventBus.Subscribe(in key, in action);

        public void Invoke(in int key) =>
            _eventBus.Invoke(in key);

        public void Invoke<T>(in int key, in T arg) =>
            _eventBus.Invoke(in key, in arg);

        public void Invoke<T1, T2>(in int key, in T1 arg1, in T2 arg2) =>
            _eventBus.Invoke(in key, in arg1, in arg2);

        public void Invoke<T1, T2, T3>(in int key, in T1 arg1, in T2 arg2, in T3 arg3) =>
            _eventBus.Invoke(in key, in arg1, in arg2, in arg3);

        public bool IsDeclared(in int key) =>
            _eventBus.IsDeclared(in key);

        public bool Undeclare(in int key) =>
            _eventBus.Undeclare(in key);

        public void Unsubscribe(in int key, in Action action) =>
            _eventBus.Unsubscribe(in key, in action);

        public void Unsubscribe<T>(in int key, in Action<T> action) =>
            _eventBus.Unsubscribe(in key, in action);

        public void Unsubscribe<T1, T2>(in int key, in Action<T1, T2> action) =>
            _eventBus.Unsubscribe(in key, in action);

        public void Unsubscribe<T1, T2, T3>(in int key, in Action<T1, T2, T3> action) =>
            _eventBus.Unsubscribe(in key, in action);

        public void Dispose() =>
            _eventBus.Dispose();
    }
}