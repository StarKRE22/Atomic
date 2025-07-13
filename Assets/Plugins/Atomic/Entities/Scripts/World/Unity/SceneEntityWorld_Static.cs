using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntityWorld
    {
        public static SceneEntityWorld Create(
            string name = null,
            bool scanEntities = false,
            params IEntity[] entities
        )
        {
            GameObject go = new GameObject(name);
            go.SetActive(false);
            
            SceneEntityWorld world = go.AddComponent<SceneEntityWorld>();
            world.Name = name;
            world.scanOnAwake = scanEntities;

            go.SetActive(true);
            world.AddEntities(entities);
            return world;
        }
    }
}