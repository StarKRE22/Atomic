using UnityEngine;

namespace Atomic.Contexts
{
    [AddComponentMenu("Atomic/Contexts/Context Controller")]
    [RequireComponent(typeof(SceneContext))]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public sealed class SceneContextController : MonoBehaviour
    {
        private SceneContext context;

        [SerializeField]
        private bool dependencyInjection = true;

        private void Awake()
        {
            this.context = this.GetComponent<SceneContext>();
        }

        private void Start()
        {
            if (this.dependencyInjection)
            {
                this.context.Construct();
            }

            this.context.Init();
            this.context.Enable();
        }
        
        private void OnEnable()
        {
            if (this.context.Initialized && !this.context.Enabled)
            {
                this.context.Enable();
            }
        }

        private void OnDisable()
        {
            if (this.context.Initialized && this.context.Enabled)
            {
                this.context.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.context.Initialized)
            {
                this.context.Dispose();
            }
        }

        private void Update()
        {
            if (this.context.Enabled)
            {
                this.context.OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            if (this.context.Enabled)
            {
                this.context.OnFixedUpdate(Time.fixedDeltaTime);
            }
        }

        private void LateUpdate()
        {
            if (this.context.Enabled)
            {
                this.context.OnLateUpdate(Time.deltaTime);
            }
        }
    }
}