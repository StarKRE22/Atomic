using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class SceneUpdater<E> : MonoBehaviour where E : SceneEntity<E>
    {
        private static readonly IEqualityComparer<E> s_entityComparer = EqualityComparer<E>.Default;

        [SerializeField]
        private E[] _entities;

        [SerializeField]
        private int _entityCount;

        private bool started;
        
        private void Reset()
        {
            _entities = this.GetComponentsInChildren<E>();
            _entityCount = _entities.Length;
        }

        private void Start()
        {
            for (int i = 0; i < _entityCount; i++)
            {
                E entity = _entities[i];
                if (!entity)
                {
                    Debug.LogWarning("SceenEntityRunner: Ops: Detected null entity!", this);
                    continue;
                }

                entity.Init();
                entity.Enable();
                ScenePlayerLoop<E>.AddEntity(entity);
            }

            this.started = true;
        }

        private void OnEnable()
        {
            if (!this.started)
                return;

            for (int i = 0; i < _entityCount; i++)
            {
                E entity = _entities[i];
                if (entity)
                {
                    entity.Enable();
                    ScenePlayerLoop<E>.AddEntity(entity);
                }
            }
        }

        private void OnDisable()
        {
            if (!this.started)
                return;

            for (int i = 0; i < _entityCount; i++)
            {
                E entity = this._entities[i];
                if (entity)
                {
                    entity.Disable();
                    ScenePlayerLoop<E>.DelEntity(entity);
                }
            }
        }

        private void OnDestroy()
        {
            if (!this.started)
                return;

            for (int i = 0, count = _entityCount; i < count; i++)
            {
                E entity = _entities[i];
                if (entity)
                {
                    entity.Dispose();
                    ScenePlayerLoop<E>.DelEntity(entity);
                }
            }
        }

        public bool Add(E entity)
        {
            if (entity == null) 
                return false;

            if (!InternalUtils.AddIfAbsent(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;
            
            if (this.started)
            {
                entity.Init();
                entity.Enable();
                ScenePlayerLoop<E>.AddEntity(entity);
            }
            
            return true;
        }
        
        public bool Del(E entity)
        {
            if (!entity)
                return false;

            if (!InternalUtils.Remove(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (this.started)
            {
                entity.Disable();
                entity.Dispose();
                ScenePlayerLoop<E>.DelEntity(entity);
            }

            return true;
        }
    }
}