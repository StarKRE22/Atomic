using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class ScriptableFactory<E> : ScriptableObject, IEntityFactory<E> where E : IEntity<E>
    {
        /// <summary>
        /// Gets the name of the entity factory, by default uses the ScriptableObject's name.
        /// </summary>
        public virtual string Name => this.name;
        
        /// <summary>
        /// The number of tags assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        protected int tagCount;

        /// <summary>
        /// The number of values assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        protected int valueCount;

        /// <summary>
        /// The number of behaviors assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        protected int behaviourCount;

        /// <summary>
        /// Creates a new <see cref="IEntity"/> instance using this factory's configuration.
        /// </summary>
        /// <returns>A new instance of <see cref="IEntity"/> with installed tags, values, and behaviors.</returns>
        public abstract E Create();

        /// <summary>
        /// Automatically called when the asset is modified in the editor. 
        /// Recalculates capacity fields by simulating an entity install.
        /// </summary>
        private void OnValidate()
        {
            try
            {
                this.Precompile();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Precompiles the factory by simulating an install into a temporary entity.
        /// Updates internal capacity fields (_tagCount, _valueCount, _behaviourCount).
        /// Can be manually triggered from the context menu.
        /// </summary>
        [ContextMenu(nameof(Precompile))]
        private void Precompile()
        {
            E entity = this.Create();
            tagCount = entity.TagCount;
            valueCount = entity.ValueCount;
            behaviourCount = entity.BehaviourCount;
        }
    }
}