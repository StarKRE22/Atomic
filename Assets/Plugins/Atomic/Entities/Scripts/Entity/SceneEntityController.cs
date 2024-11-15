using UnityEngine;

namespace Atomic.Entities
{
    /**
     * Controls Entity lifecycle by Unity Callbacks.
     */

    [AddComponentMenu("Atomic/Entities/Entity Controller")]
    [DefaultExecutionOrder(-1000)]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SceneEntity))]
    public sealed class SceneEntityController : MonoBehaviour
    {
        private SceneEntity entity;
        
        private void Awake()
        {
            this.entity = this.GetComponent<SceneEntity>();
        }

        private void OnEnable()
        {
            if (this.entity.Initialized && !this.entity.Enabled)
            {
                this.entity.Enable();
                SceneEntityUpdater.AddEntity(this.entity);
            }
        }

        private void OnDisable()
        {
            if (this.entity.Enabled)
            {
                SceneEntityUpdater.DelEntity(this.entity);
                this.entity.Disable();
            }
        }

        private void Start()
        {
            this.entity.Init();
            this.entity.Enable();
            SceneEntityUpdater.AddEntity(this.entity);
        }

        private void OnDestroy()
        {
            if (this.entity.Initialized)
            {
                this.entity.Dispose();
            } 
        }
    }
}