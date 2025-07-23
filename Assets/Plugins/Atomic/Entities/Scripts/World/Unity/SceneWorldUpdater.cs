using UnityEngine;

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity World Runner")]
    [DisallowMultipleComponent]
    public class SceneWorldUpdater<E> : MonoBehaviour where E : SceneEntity<E>
    {
        [SerializeField]
        private SceneWorld<E> world;

        private bool started;

        private void OnEnable()
        {
            if (this.started) 
                this.world.Enable();
        }

        private void Start()
        {
            this.world.Spawn();
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
                this.world.Despawn();
        }
    }
}