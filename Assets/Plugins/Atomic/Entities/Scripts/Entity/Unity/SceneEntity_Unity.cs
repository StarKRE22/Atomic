#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntity : ISerializationCallbackReceiver
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
                TickManager.Instance.Register(this);
            }
        }

        private protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Init();
                this.Enable();
                TickManager.Instance.Register(this);
            }

            _started = true;
        }

        private protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Disable();
                TickManager.Instance.Unregister(this);
            }
        }

        private protected virtual void OnDestroy()
        {
            if (this.useUnityLifecycle && _started)
                this.Dispose();

            if (this.uninstallOnDestroy)
                this.Uninstall();

            this.Unregister();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Construct();
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
        }
    }
}
#endif