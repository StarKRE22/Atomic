#if UNITY_5_3_OR_NEWER
namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IPrefabEntityPool{E}"/> specialized for base <see cref="MonoEntity"/> types.
    /// </summary>
    /// <remarks>
    /// This interface provides a non-generic abstraction for working with multi-scene entity pools,
    /// typically used for pooling and managing <see cref="MonoEntity"/> instances across multiple scenes.
    /// </remarks>
    public interface IPrefabEntityPool : IPrefabEntityPool<IEntity, MonoEntity>
    {
    }
}
#endif