#if UNITY_5_3_OR_NEWER

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="SceneEntityFactoryProxy{E}"/>.
    /// </summary>
    public abstract class SceneEntityFactoryProxy : SceneEntityFactoryProxy<IEntity>
    {
    }
}
#endif