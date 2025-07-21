#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for creating <see cref="IEntity"/> instances via Unity's <see cref="ScriptableObject"/> system.
    /// Stores initial parameters used during entity creation and allows previewing entity properties in the Editor.
    /// </summary>
    /// <typeparam name="E">The type of entity to create. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This factory can be extended to define custom entity creation logic. The <see cref="Precompile"/> method
    /// extracts entity metadata (like name, tag count, etc.) and stores it for optimization or display purposes.
    /// </remarks>
    public abstract class ScriptableEntityFactory<E> : ScriptableObject, IEntityFactory<E> where E : IEntity
    {
        /// <summary>
        /// Initial number of tags to assign to the entity.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        protected int initialTagCount;

        /// <summary>
        /// Initial number of values to assign to the entity.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        protected int initialValueCount;

        /// <summary>
        /// Initial number of behaviours to assign to the entity.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        protected int initialBehaviourCount;

        /// <summary>
        /// Initial name to assign to the entity.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        protected string initialName;

        /// <summary>
        /// Creates and returns a new instance of the entity.
        /// </summary>
        /// <returns>A new <typeparamref name="E"/> instance.</returns>
        public abstract E Create();

        /// <summary>
        /// Unity callback invoked when the script is loaded or a value is changed in the Inspector.
        /// Used here to update cached metadata via <see cref="Precompile"/>.
        /// </summary>
        protected virtual void OnValidate()
        {
            try
            {
                this.Precompile();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[ScriptableEntityFactory] Precompile failed: {ex.Message}", this);
            }
        }

        /// <summary>
        /// Unity callback used to reset factory fields to their default values.
        /// </summary>
        protected virtual void Reset()
        {
            this.initialName = this.name;
            this.initialTagCount = 0;
            this.initialValueCount = 0;
            this.initialBehaviourCount = 0;
        }

        /// <summary>
        /// Generates a preview entity and extracts metadata such as tag count, value count, and name.
        /// This is useful for optimizing asset previews and reducing runtime introspection.
        /// </summary>
        [ContextMenu(nameof(Precompile))]
        protected virtual void Precompile()
        {
            E entity = this.Create();
            if (entity == null)
            {
                Debug.LogWarning($"{nameof(ScriptableEntityFactory<E>)}: Create() returned null.", this);
                return;
            }

            this.initialName = entity.Name;
            this.initialTagCount = entity.TagCount;
            this.initialValueCount = entity.ValueCount;
            this.initialBehaviourCount = entity.BehaviourCount;
        }
    }
    
    /// <summary>
    /// Abstract base class for Unity-based factories that create and configure <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// In addition to creating the entity with predefined values, this class defines an <see cref="Install"/> method
    /// that allows injecting additional behaviors, components, or configuration into the newly created entity.
    /// </remarks>
    public abstract class ScriptableEntityFactory : ScriptableEntityFactory<IEntity>, IEntityFactory
    {
        /// <summary>
        /// Creates a new <see cref="Entity"/> using the initial parameters defined in the factory
        /// and applies additional configuration via the <see cref="Install"/> method.
        /// </summary>
        /// <returns>A fully constructed and configured <see cref="Entity"/>.</returns>
        public sealed override IEntity Create()
        {
            var entity = new Entity(
                this.initialName,
                this.initialTagCount,
                this.initialValueCount,
                this.initialBehaviourCount
            );
            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Applies additional logic to the newly created <see cref="Entity"/> instance.
        /// Override this method to inject behaviors, tags, values, or other initialization logic.
        /// </summary>
        /// <param name="entity">The entity to configure after creation.</param>
        protected abstract void Install(Entity entity);
    }
}
#endif