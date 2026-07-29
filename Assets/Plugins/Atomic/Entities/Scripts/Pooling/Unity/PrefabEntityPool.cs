#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Default implementation of <see cref="PrefabEntityPool{E}"/> for base <see cref="MonoEntity"/> types.
    /// </summary>
    /// <remarks>
    /// This class provides a convenient non-generic entry point for working with pooled <see cref="MonoEntity"/> instances
    /// across multiple Unity scenes. Use this when generic type inference is not needed.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Prefab Entity Pool")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Pooling/PrefabEntityPool.md")]
    public class PrefabEntityPool : PrefabEntityPool<IEntity, MonoEntity>, IPrefabEntityPool
    {
    }
}
#endif