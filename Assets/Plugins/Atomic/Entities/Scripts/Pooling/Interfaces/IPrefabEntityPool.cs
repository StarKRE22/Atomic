#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IPrefabEntityPool{E}"/> specialized for base <see cref="SceneEntity"/> types.
    /// </summary>
    /// <remarks>
    /// This interface provides a non-generic abstraction for working with multi-scene entity pools,
    /// typically used for pooling and managing <see cref="SceneEntity"/> instances across multiple scenes.
    /// </remarks>
    public interface IPrefabEntityPool : IPrefabEntityPool<SceneEntity>
    {
    }
}
#endif