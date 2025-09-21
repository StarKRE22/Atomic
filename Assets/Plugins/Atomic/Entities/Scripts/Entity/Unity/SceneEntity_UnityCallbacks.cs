#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntity : ISerializationCallbackReceiver
    {
        private bool _started;

        private void Awake()
        {
            EntityRegistry.Instance.Register(this, out _instanceId);

            if (this.installOnAwake)
                this.Install();
        }

        private void OnEnable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Enable();
                UpdateLoop.Instance.Register(this);
            }
        }

        private void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Init();
                this.Enable();
                UpdateLoop.Instance.Register(this);
                _started = true;
            }
        }

        private void OnDisable()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Disable();
                UpdateLoop.Instance.Unregister(this);
            }
        }

        private void OnDestroy()
        {
            if (this.useUnityLifecycle && _started)
            {
                this.Dispose();
                _started = false;
            }
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