using System.Collections.Generic;
using System.Text.RegularExpressions;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Game.UI
{
    public class GameObjectPool
    {
        private struct Pool
        {
            public Queue<GameObject> queue;
            public Transform transform;
            public GameObject go;
        }

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private readonly Dictionary<string, Pool> pools = new();

        private readonly Transform container;

        public GameObjectPool(Transform container)
        {
            this.container = container;
        }

        public GameObject Rent(GameObject prefab)
        {
            return Rent(prefab, Vector3.zero, Quaternion.identity);
        }

        public GameObject Rent(GameObject prefab, Transform parent)
        {
            return Rent(prefab, parent.position, parent.rotation);
        }

        public GameObject Rent(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            string objName = this.GetEntityType(prefab);

            if (!this.pools.TryGetValue(objName, out Pool pool))
            {
                pool = this.CreatePool(objName);
                this.pools.Add(objName, pool);
            }

            if (pool.queue.TryDequeue(out GameObject obj))
            {
                Transform transform = obj.transform;
                transform.parent = parent;
                transform.position = position;
                transform.rotation = rotation;
            }
            else
            {
                obj = GameObject.Instantiate(prefab, position, rotation, parent);
                this.OnCreate(obj);
                obj.name = objName;
            }

            this.OnRent(obj);
            return obj;
        }

        public void Return(GameObject obj)
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

        public void Clear(GameObject prefab)
        {
            string objName = this.GetEntityType(prefab);

            if (!this.pools.Remove(objName, out Pool pool))
                return;

            foreach (GameObject entity in pool.queue)
            {
                this.OnDestroy(entity);
                GameObject.Destroy(entity);
            }

            GameObject.Destroy(pool.go);
        }

        protected virtual void OnCreate(GameObject entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected virtual void OnDestroy(GameObject entity)
        {
        }

        protected virtual void OnRent(GameObject entity)
        {
            entity.gameObject.SetActive(true);
        }

        protected virtual void OnReturn(GameObject entity)
        {
            entity.gameObject.SetActive(false);
        }

        protected virtual string GetEntityType(GameObject entity)
        {
            //Remove '(n)' pattern:
            return Regex.Replace(entity.name, @"\s*\(\d+\)$", "").Trim();
        }

        private Pool CreatePool(string name)
        {
            var queue = new Queue<GameObject>();
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