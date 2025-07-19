using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class ScenePoolRegistry<E> : MonoBehaviour, IScenePoolRegistry<E> where E : SceneEntity<E>
    {
        private const string NUMBER_PATTERN = @"\s*\(\d+\)$";

        private struct Pool
        {
            public Stack<E> queue;
            public Transform transform;
            public GameObject go;
        }
        
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<string, Pool> pools = new();

        [SerializeField]
        private Transform container;
        
        public E Rent(E prefab) => this.Rent(prefab, Vector3.zero, Quaternion.identity);

        public E Rent(E prefab, Transform parent) => this.Rent(prefab, parent.position, parent.rotation);

        public E Rent(E prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            string entityName = this.GetEntityName(prefab);

            if (!this.pools.TryGetValue(entityName, out Pool pool))
            {
                pool = this.CreatePool(entityName);
                this.pools.Add(entityName, pool);
            }

            if (pool.queue.TryPop(out E entity))
            {
                Transform transform = entity.transform;
                transform.parent = parent;
                transform.position = position;
                transform.rotation = rotation;
            }
            else
            {
                entity = SceneEntity<E>.Create(prefab, position, rotation, parent);
                this.OnSpawn(entity);
                entity.name = entityName;
            }

            this.OnRent(entity);
            return entity;
        }

        public void Return(E entity)
        {
            string entityName = this.GetEntityName(entity);

            if (!this.pools.TryGetValue(entityName, out Pool pool))
            {
                pool = this.CreatePool(entityName);
                this.pools.Add(entityName, pool);
            }

            if (pool.queue.Contains(entity)) 
                return;
            
            this.OnReturn(entity);
            
            entity.transform.parent = pool.transform;
            pool.queue.Push(entity);
        }

        public void Clear(E prefab)
        {
            string objName = this.GetEntityName(prefab);

            if (!this.pools.Remove(objName, out Pool pool))
                return;

            foreach (E entity in pool.queue)
            {
                this.OnDespawn(entity);
                Destroy(entity);
            }
            
            Destroy(pool.go);
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, Pool> pair in this.pools)
            {
                Pool pool = pair.Value;
                foreach (E entity in pool.queue)
                {
                    this.OnDespawn(entity);
                    Destroy(entity);
                }

                Destroy(pool.go);
            }

            this.pools.Clear();
        }

        protected virtual void OnSpawn(E entity) => entity.gameObject.SetActive(false);
        
        protected virtual void OnRent(E entity) => entity.gameObject.SetActive(true);

        protected virtual void OnReturn(E entity) => entity.gameObject.SetActive(false);

        protected virtual void OnDespawn(E entity)
        {
        }
        
        protected virtual string GetEntityName(E entity) => 
            Regex.Replace(entity.name, NUMBER_PATTERN, string.Empty).Trim();

        private Pool CreatePool(string name)
        {
            Stack<E> stack = new Stack<E>();
            Transform transform = new GameObject($"<{name}s>").transform;
            transform.parent = this.container;
          
            return new Pool
            {
                queue = stack,
                transform = transform
            };
        }
    }
}