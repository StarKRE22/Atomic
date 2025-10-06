#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic catalog of <see cref="EntityView"/> prefabs.
    /// </summary>
    /// <remarks>
    /// This is a concrete version of <see cref="EntityViewCatalog{E, V}"/> 
    /// with <typeparamref name="E"/> fixed to <see cref="IEntity"/> and 
    /// <typeparamref name="V"/> fixed to <see cref="EntityView"/>.
    /// It is useful when you do not need strong typing for a specific entity type.
    /// </remarks>
    [CreateAssetMenu(
        fileName = "EntityViewCatalog",
        menuName = "Atomic/Entities/New EntityViewCatalog"
    )]
    public class EntityViewCatalog : EntityViewCatalog<IEntity, EntityView>
    {
    }
}
#endif