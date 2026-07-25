#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Default non-generic implementation of a <see cref="MonoEntityPool{T}"/> for base <see cref="MonoEntity"/> types.
    /// </summary>
    /// <remarks>
    /// This component can be added to a GameObject in the Unity scene to manage pooling of <see cref="MonoEntity"/> instances
    /// without requiring generics. It implements <see cref="IEntityPool"/> for compatibility with systems that expect non-generic access.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject to preallocate and reuse pooled entities at runtime:
    /// <code>
    /// var pooledEntity = sceneEntityPool.Rent();
    /// sceneEntityPool.Return(pooledEntity);
    /// </code>
    /// </example>
    [AddComponentMenu("Atomic/Entities/Entity Pool")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Pooling/SceneEntityPool.md")]
    public class MonoEntityPool : MonoEntityPool<IEntity, MonoEntity>, IEntityPool
    {
        /// <summary>
        /// Creates a new instance of a non-generic <see cref="MonoEntityPool"/> in the scene.
        /// </summary>
        /// <param name="args">The <see cref="CreateArgs"/> structure containing initialization parameters such as name, prefab, container, initial count, and whether to initialize on Awake.</param>
        /// <returns>A newly created <see cref="MonoEntityPool"/> instance added to a new GameObject in the scene.</returns>
        /// <example>
        /// <code>
        /// var poolArgs = new MonoEntityPool.CreateArgs
        /// {
        ///     name = "EnemyPool",
        ///     prefab = enemyPrefab,
        ///     container = parentTransform,
        ///     initOnAwake = true,
        ///     initialCount = 10
        /// };
        /// 
        /// MonoEntityPool pool = MonoEntityPool.Create(poolArgs);
        /// </code>
        /// </example>
        public static MonoEntityPool Create(in CreateArgs args) => Create<MonoEntityPool>(in args);
    }
}
#endif