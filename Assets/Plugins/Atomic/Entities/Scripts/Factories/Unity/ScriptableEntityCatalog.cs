#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A concrete Unity <see cref="ScriptableObject"/> implementation of <see cref="ScriptableEntityCatalog{K,E,F,TArgs}"/>
    /// specialized for <see cref="string"/> keys, <see cref="IEntity"/> entities, 
    /// and <see cref="EntityFactory"/> factories.
    /// Implements <see cref="IMultiEntityFactory"/>.
    /// </summary>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/ScriptableEntityCatalog.md")]
    [CreateAssetMenu(
        fileName = "MultiEntityFactory",
        menuName = "Atomic/Entities/MultiEntityFactory"
    )]
    public abstract class ScriptableEntityCatalog<TArgs> :
        ScriptableEntityCatalog<string, IEntity, ScriptableEntityFactory<TArgs>, TArgs>,
        IMultiEntityFactory<TArgs>
        where TArgs : IArgs
    {
        /// <summary>
        /// Extracts the string key for a given factory.
        /// Uses the factory's asset name as the key.
        /// </summary>
        /// <param name="factory">The factory to extract a key from.</param>
        /// <returns>The name of the factory asset.</returns>
        protected override string GetKey(ScriptableEntityFactory<TArgs> factory) => factory.Name;
    }
}
#endif