#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="SceneEntityWorld{SceneEntity}"/>.
    /// Represents a Unity scene-bound entity world operating on base <see cref="SceneEntity"/> types.
    /// </summary>
    /// <remarks>
    /// Use this when you don't need to specialize the world with a custom entity type.
    /// Useful for simple scenarios where only <see cref="SceneEntity"/> is involved.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Worlds/SceneEntityWorld.md")]
    public class SceneEntityWorld : SceneEntityWorld<SceneEntity>
    {
        public static SceneEntityWorld Create(
            string name = null,
            bool scanEntities = true,
            bool useUnityLifecycle = true
        ) => Create<SceneEntityWorld>(name, scanEntities, useUnityLifecycle);
    }
}
#endif