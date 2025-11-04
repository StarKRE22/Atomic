#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for scriptable entity aspects.
    /// </summary>
    /// <remarks>
    /// This class is a concrete specialization of <see cref="ScriptableEntityAspect{E}"/> 
    /// with <typeparamref name="E"/> fixed to <see cref="IEntity"/>. 
    /// It implements <see cref="IEntityAspect"/> and can be used as a ScriptableObject asset.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Aspects/ScriptableEntityAspect.md")]
    public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
    {
    }
}
#endif