using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class GenericSceneEntityPool : IGenericSceneEntityPool
    {
        private struct Pool
        {
            public Queue<SceneEntity> queue;
            public Transform transform;
            public GameObject go;
        }
        
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<string, Pool> pools = new();

        private readonly Transform container;

        public GenericSceneEntityPool(Transform container)
        {
            this.container = container;
        }

        public SceneEntity Rent(SceneEntity prefab)
        {
            return Rent(prefab, Vector3.zero, Quaternion.identity);
        }
        
        public SceneEntity Rent(SceneEntity prefab, Transform parent)
        {
            return Rent(prefab, parent.position, parent.rotation);
        }

        public SceneEntity Rent(SceneEntity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            string objName = this.GetEntityType(prefab);

            if (!this.pools.TryGetValue(objName, out Pool pool))
            {
                pool = this.CreatePool(objName);
                this.pools.Add(objName, pool);
            }

            if (pool.queue.TryDequeue(out SceneEntity obj))
            {
                Transform transform = obj.transform;
                transform.parent = parent;
                transform.position = position;
                transform.rotation = rotation;
            }
            else
            {
                obj = SceneEntity.Create(prefab, position, rotation, parent);
                this.OnCreate(obj);
                obj.name = objName;
            }

            this.OnRent(obj);
            return obj;
        }

        public void Return(IEntity entity)
        {
            if (SceneEntity.TryCast(entity, out SceneEntity sceneEntity))
                this.Return(sceneEntity);
        }

        public void Return(SceneEntity obj)
        {
            string objName = this.GetEntityType(obj);

            if (!this.pools.TryGetValue(objName, out Pool pool))
            {
                pool = this.CreatePool(objName);
                this.pools.Add(objName, pool);
            }

            if (pool.queue.Contains(obj)) 
                return;
            
            this.OnReturn(obj);
            
            obj.transform.parent = pool.transform;
            pool.queue.Enqueue(obj);
        }

        public void Clear(SceneEntity prefab)
        {
            string objName = this.GetEntityType(prefab);

            if (!this.pools.Remove(objName, out Pool pool))
                return;

            foreach (SceneEntity entity in pool.queue)
            {
                this.OnDestroy(entity);
                GameObject.Destroy(entity);
            }
            
            GameObject.Destroy(pool.go);
        }

        protected virtual void OnCreate(SceneEntity entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy(SceneEntity entity)
        {
        }

        protected virtual void OnRent(SceneEntity entity)
        {
            entity.gameObject.SetActive(true);
        }

        protected virtual void OnReturn(SceneEntity entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected virtual string GetEntityType(SceneEntity entity)
        {
            //Remove '(n)' pattern:
            return Regex.Replace(entity.name, @"\s*\(\d+\)$", "").Trim();
        }

        private Pool CreatePool(string name)
        {
            var queue = new Queue<SceneEntity>();
            var transform = new GameObject($"<{name}s>").transform;
            transform.parent = this.container;
          
            return new Pool
            {
                queue = queue,
                transform = transform
            };
        }
    }
}