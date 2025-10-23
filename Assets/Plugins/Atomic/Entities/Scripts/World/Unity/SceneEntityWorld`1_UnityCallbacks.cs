#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        protected virtual void Awake()
        {
            if (this.collectOnAwake)
                this.CollectAllEntities(this.includeInactiveOnCollect);

            if (this.dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }

        private protected virtual void OnEnable()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                this.Enable();
                TickManager.Instance.Register(this);
            }
        }

        private protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Enable();
                TickManager.Instance.Register(this);
                this.isStarted = true;
            }
        }

        private protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                TickManager.Instance.Unregister(this);
                this.Disable();
            }
        }

        private protected virtual void OnDestroy()
        {
            if (this.useUnityLifecycle)
                this.Dispose();
        }
    }
}
#endif