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
    /// Base factory / baker class for creating scene entities.
    /// </summary>
    /// <typeparam name="E">The type of entity produced by this factory. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This class inherits from <see cref="MonoBehaviour"/> and is designed to be used both at runtime
    /// and in the Unity Editor. Several operations (such as <see cref="Precompile"/>) are editor-only
    /// and wrapped in <c>UNITY_EDITOR</c> compilation directives.
    /// 
    /// Derived classes must implement <see cref="Create"/> to construct new entity instances.
    /// </remarks>
    public abstract partial class SceneEntityBaker<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(1000)]
#endif
        [Tooltip("Initial number of tags to assign to the entity")]
        [SerializeField]
        protected int initialTagCapacity;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(1000)]
#endif
        [Tooltip("Initial number of values to assign to the entity")]
        [SerializeField]
        protected int initialValueCapacity;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
        [PropertyOrder(1000)]
#endif
        [Tooltip("Initial number of behaviours to assign to the entity")]
        [SerializeField]
        protected int initialBehaviourCapacity;

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
            this.initialTagCapacity = 0;
            this.initialValueCapacity = 0;
            this.initialBehaviourCapacity = 0;
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
                Debug.LogWarning($"{nameof(SceneEntityBaker<E>)}: Create() returned null.", this);
                return;
            }

            this.initialTagCapacity = entity.TagCount;
            this.initialValueCapacity = entity.ValueCount;
            this.initialBehaviourCapacity = entity.BehaviourCount;
#endif
        }
    }
}
#endif