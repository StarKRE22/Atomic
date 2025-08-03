#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Abstract base class for Unity-based factories that create and configure <see cref="Entity"/> instances.
    /// </summary>
    /// <remarks>
    /// This class is designed for use in scene-based workflows where entities need to be created at runtime
    /// from serialized MonoBehaviours (e.g. for prototyping, design-time composition, or runtime baking).
    /// It also defines an <see cref="Install"/> method that allows injecting custom configuration logic,
    /// such as adding tags, values, or behaviors after the entity has been created.
    /// </remarks>
    public abstract class SceneEntityFactory : SceneEntityFactory<IEntity>, IEntityFactory
    {
        /// <summary>
        /// Creates a new <see cref="Entity"/> using the predefined initialization values,
        /// then applies custom logic via the <see cref="Install"/> method.
        /// </summary>
        public sealed override IEntity Create()
        {
            Entity entity = new Entity(
                this.initialName,
                this.initialTagCount,
                this.initialValueCount,
                this.initialBehaviourCount
            );
            this.Install(entity);
            return entity;
        }

        /// <summary>
        /// Applies additional configuration to the newly created <see cref="Entity"/>.
        /// This method can be used to inject custom logic, add components, behaviors, or metadata.
        /// </summary>
        /// <param name="entity">The entity to configure after creation.</param>
        protected abstract void Install(Entity entity);
    }

    /// <summary>
    /// Abstract base class for Unity-based factories that create <typeparamref name="E"/> entities
    /// with customizable initial settings and optional support for scene baking workflows.
    /// </summary>
    /// <typeparam name="E">The type of entity to create. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This factory is useful for building entity templates in the Unity Editor,
    /// where entities can be created at runtime or during a "bake" phase by converting authoring components into runtime data.
    ///
    /// You can override <see cref="Create"/> to define custom instantiation logic,
    /// and use <see cref="Precompile"/> to extract editor-time metadata like name or component counts.
    /// </remarks>
    public abstract class SceneEntityFactory<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
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
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                return;

            try
            {
                this.Precompile();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[SceneEntityFactory] Precompile failed: {ex.Message}", this);
            }
#endif
        }

        /// <summary>
        /// Unity callback used to reset factory fields to their default values.
        /// </summary>
        protected virtual void Reset()
        {
#if UNITY_EDITOR
            this.initialName = this.name;
            this.initialTagCount = 0;
            this.initialValueCount = 0;
            this.initialBehaviourCount = 0;
#endif
        }


        /// <summary>
        /// Creates a temporary entity using <see cref="Create"/> and extracts basic metadata such as
        /// name, tag count, value count, and behaviour count. This is useful for editor workflows,
        /// asset previews, and scene baking optimization.
        /// </summary>
        [ContextMenu(nameof(Precompile))]
        protected virtual void Precompile()
        {
#if UNITY_EDITOR
            E entity = this.Create();
            if (entity == null)
            {
                Debug.LogWarning($"{nameof(SceneEntityFactory<E>)}: Create() returned null.", this);
                return;
            }

            this.initialName = entity.Name;
            this.initialTagCount = entity.TagCount;
            this.initialValueCount = entity.ValueCount;
            this.initialBehaviourCount = entity.BehaviourCount;
#endif
        }
    }
}
#endif