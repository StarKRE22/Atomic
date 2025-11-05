#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity-based pool manager for reusing entity view instances based on their names.
    /// This reduces memory allocations and improves performance by avoiding frequent instantiations.
    /// </summary>
    [AddComponentMenu("Atomic/Entities/Entity View Pool")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/UI/EntityViewPool.md")]
    public class EntityViewPool : EntityViewPool<IEntity, EntityView>
    {
    }
}
#endif