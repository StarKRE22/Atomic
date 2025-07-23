using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneWorld<E>
    {
        public static SceneWorld<E> Create(
            string name = null,
            bool scanEntities = false,
            params E[] entities
        )
        {
            GameObject go = new GameObject(name);
            go.SetActive(false);
            
            SceneWorld<E> world = go.AddComponent<SceneWorld<E>>();
            world.Name = name;
            world.scanOnAwake = scanEntities;

            go.SetActive(true);
            world.AddRange(entities);
            return world;
        }
    }
}