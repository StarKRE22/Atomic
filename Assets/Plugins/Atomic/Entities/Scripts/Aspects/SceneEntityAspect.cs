#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for scene-based entity aspects.
    /// </summary>
    /// <remarks>
    /// This class is a concrete specialization of <see cref="SceneEntityAspect{E}"/> 
    /// with <typeparamref name="E"/> fixed to <see cref="IEntity"/>. 
    /// It implements <see cref="IEntityAspect"/> and can be added to GameObjects in a Unity scene.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Aspects/SceneEntityAspect.md")]
    public abstract class SceneEntityAspect : SceneEntityAspect<IEntity>, IEntityAspect
    {
    }
}
#endif