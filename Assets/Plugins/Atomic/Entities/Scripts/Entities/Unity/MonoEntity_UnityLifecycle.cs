#if UNITY_5_3_OR_NEWER
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    public partial class MonoEntity : ISerializationCallbackReceiver
    {
        private bool _started;

        private protected virtual void Awake()
        {
            this.Register();

            if (this.installOnAwake)
                this.Install();
        }

        private protected virtual void OnEnable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Enable();
                TickableManager.Instance.Register(this);
            }
        }

        private protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Init();
                this.Enable();
                TickableManager.Instance.Register(this);
            }

            _started = true;
        }

        private protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Disable();
                TickableManager.Instance.Unregister(this);
            }
        }

        private protected virtual void OnDestroy()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.OnDispose();
                this.Deinitialize();

                if (this.disposeValues)
                    this.DisposeValues();
            }

            if (this.uninstallOnDestroy)
                this.Uninstall();

            this.ClearTags();
            this.ClearValues();
            this.ClearBehaviours();

            this.OnStateChanged?.Invoke(this);
            this.UnsubscribeEvents();
            this.Unregister();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Construct();
            this.Register();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}
#endif