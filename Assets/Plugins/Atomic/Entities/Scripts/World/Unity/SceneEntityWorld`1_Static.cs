#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntityWorld<E>
    {
        /// <summary>
        /// Creates a new inactive <see cref="GameObject"/> with an attached <see cref="SceneEntityWorld{E}"/> component.
        /// </summary>
        /// <param name="name">The name of the GameObject and world instance.</param>
        /// <param name="scanEntities">If true, the world will scan the scene for entities on Awake.</param>
        /// <param name="entities">Optional entities to add immediately after creation.</param>
        /// <returns>The initialized <see cref="SceneEntityWorld{E}"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(
            string name = null,
            bool scanEntities = true,
            bool useUnityLifecycle = true
        )
            where T : SceneEntityWorld<E>
        {
            GameObject go = new GameObject();
            go.SetActive(false);

            T world = go.AddComponent<T>();
            world.Name = name;
            world.collectOnAwake = scanEntities;
            world.useUnityLifecycle = useUnityLifecycle;

            go.SetActive(true);
            return world;
        }

        /// <summary>
        /// Destroys the <see cref="SceneEntityWorld{E}"/> and its associated GameObject after an optional delay.
        /// </summary>
        /// <typeparam name="E">The type of scene entity managed by the world.</typeparam>
        /// <param name="world">The world instance to destroy.</param>
        /// <param name="t">Optional delay in seconds before destruction. Default is 0.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(SceneEntityWorld<E> world, float t = 0)
        {
            if (world)
                GameObject.Destroy(world.gameObject, t);
        }
    }
}
#endif