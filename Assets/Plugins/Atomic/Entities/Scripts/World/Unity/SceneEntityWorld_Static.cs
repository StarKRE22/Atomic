using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        public static SceneEntityWorld<E> Create(
            string name = null,
            bool scanEntities = false,
            params E[] entities
        )
        {
            GameObject go = new GameObject(name);
            go.SetActive(false);
            
            SceneEntityWorld<E> world = go.AddComponent<SceneEntityWorld<E>>();
            world.Name = name;
            world.scanOnAwake = scanEntities;

            go.SetActive(true);
            world.AddRange(entities);
            return world;
        }

      
    }
}