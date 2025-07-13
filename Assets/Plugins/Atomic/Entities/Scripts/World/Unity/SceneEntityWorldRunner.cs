using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World Runner")]
    // [DefaultExecutionOrder(-1000)] cause breaks Unity LateUpdate()
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SceneEntityWorld))]
    public class SceneEntityWorldRunner : MonoBehaviour
    {
        private SceneEntityWorld world;
        private bool started;

        private void Awake()
        {
            this.world = this.GetComponent<SceneEntityWorld>();
        }

        private void OnEnable()
        {
            if (this.started) 
                this.world.Enable();
        }

        private void Start()
        {
            this.world.Init();
            this.world.Enable();
            this.started = true;
        }

        private void FixedUpdate()
        {
            this.world.OnFixedUpdate(Time.fixedDeltaTime);
        }

        private void Update()
        {
            this.world.OnUpdate(Time.deltaTime);
        }

        private void LateUpdate()
        {
            this.world.OnLateUpdate(Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            if (this.started) 
                this.world.Disable();
        }

        private void OnDestroy()
        {
            if (this.started) 
                this.world.Dispose();
        }
    }
}