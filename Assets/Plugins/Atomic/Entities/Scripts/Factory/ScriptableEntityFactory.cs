using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// An abstract base class for creating <see cref="IEntity"/> instances via Unity's <see cref="ScriptableObject"/>.
    /// Used as a factory to define and configure entity blueprints in the Unity Editor.
    /// </summary>
    public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>
    {
        /// <summary>
        /// The number of tags assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _tagCount;

        /// <summary>
        /// The number of values assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _valueCount;

        /// <summary>
        /// The number of behaviors assigned during the precompilation step. Used to allocate internal storage.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _behaviourCount;

        /// <summary>
        /// Creates a new <see cref="IEntity"/> instance using this factory's configuration.
        /// </summary>
        /// <returns>A new instance of <see cref="IEntity"/> with installed tags, values, and behaviors.</returns>
        public override IEntity Create()
        {
            Entity entity = new Entity(this.Name, _tagCount, _valueCount, _behaviourCount);
            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Defines how this factory installs tags, values, and behaviors onto the provided <see cref="IEntity"/>.
        /// This method must be implemented by derived classes.
        /// </summary>
        /// <param name="entity">The entity to configure.</param>
        protected abstract void Install(IEntity entity);

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
            var entity = new Entity();
            this.Install(entity);

            _tagCount = entity.TagCount;
            _valueCount = entity.ValueCount;
            _behaviourCount = entity.BehaviourCount;
        }
    }

    public abstract class ScriptableEntityFactory<T> : ScriptableObject, IEntityFactory<T> where T : IEntity
    {
        /// <summary>
        /// Gets the name of the entity factory, by default uses the ScriptableObject's name.
        /// </summary>
        public virtual string Name => this.name;

        public abstract T Create();
    }
}