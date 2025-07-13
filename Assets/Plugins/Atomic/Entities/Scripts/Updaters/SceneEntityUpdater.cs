using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    /**
     * Controls Entity lifecycle by Unity Callbacks.
     */
    
    [AddComponentMenu("Atomic/Entities/Entity Updater")]
    [DisallowMultipleComponent]
    public class SceneEntityUpdater : MonoBehaviour
    {
        [SerializeField]
        private List<SceneEntity> entities = new();

        private bool started;
        
        private void Reset()
        {
            this.entities = new List<SceneEntity>(this.GetComponentsInChildren<SceneEntity>());
        }

        private void Start()
        {
            for (int i = 0, count = this.entities.Count; i < count; i++)
            {
                SceneEntity entity = this.entities[i];
                if (!entity)
                {
                    Debug.LogWarning("SceenEntityRunner: Ops: Detected null entity!", this);
                    continue;
                }

                entity.Init();
                entity.Enable();
                GlobalSceneEntityUpdater.AddEntity(entity);
            }

            this.started = true;
        }

        private void OnEnable()
        {
            if (!this.started)
                return;

            for (int i = 0, count = this.entities.Count; i < count; i++)
            {
                SceneEntity entity = this.entities[i];
                if (!entity)
                    continue;

                entity.Enable();
                GlobalSceneEntityUpdater.AddEntity(entity);
            }
        }

        private void OnDisable()
        {
            if (!this.started)
                return;

            for (int i = 0, count = this.entities.Count; i < count; i++)
            {
                SceneEntity entity = this.entities[i];
                if (!entity)
                    continue;

                entity.Disable();
                GlobalSceneEntityUpdater.DelEntity(entity);
            }
        }

        private void OnDestroy()
        {
            if (!this.started)
                return;

            for (int i = 0, count = this.entities.Count; i < count; i++)
            {
                SceneEntity entity = this.entities[i];
                if (!entity)
                    continue;

                entity.Dispose();
                GlobalSceneEntityUpdater.DelEntity(entity);
            }
        }

        public bool Add(in SceneEntity entity)
        {
            if (entity == null) 
                return false;

            if (this.entities.Contains(entity))
                return false;

            
            this.entities.Add(entity);
            
            if (this.started)
            {
                entity.Init();
                entity.Enable();
                GlobalSceneEntityUpdater.AddEntity(entity);
            }
            
            return true;
        }
        
        public bool Del(in SceneEntity entity)
        {
            if (!entity)
                return false;

            if (!this.entities.Remove(entity))
                return false;

            if (this.started)
            {
                entity.Disable();
                entity.Dispose();
                GlobalSceneEntityUpdater.DelEntity(entity);
            }

            return true;
        }
    }
}