using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World Controller")]
    [DefaultExecutionOrder(-1000)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SceneEntityWorld))]
    public sealed class SceneEntityWorldController : MonoBehaviour
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
            {
                this.world.EnableEntities();
            }
        }

        private void Start()
        {
            this.world.InitEntities();
            this.world.EnableEntities();
            this.started = true;
        }

        private void FixedUpdate()
        {
            this.world.FixedUpdateEntities(Time.fixedDeltaTime);
        }

        private void Update()
        {
            this.world.UpdateEntities(Time.deltaTime);
        }

        private void LateUpdate()
        {
            this.world.LateUpdateEntities(Time.fixedDeltaTime);
        }

        private void OnDisable()
        {
            if (this.started)
            {
                this.world.DisableEntities();
            }
        }

        private void OnDestroy()
        {
            if (this.started)
            {
                this.world.DisposeEntities();
            }
        }
    }
}