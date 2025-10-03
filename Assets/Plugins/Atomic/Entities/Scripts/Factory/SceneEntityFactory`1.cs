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
    public abstract partial class SceneEntityFactory<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
    {
        
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [DisableInPlayMode]
#endif
        [Tooltip("Should destroy this Game Object after baking?")]
        [SerializeField]
        protected internal bool _destroyAfterBake = true;
        
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
                Debug.LogWarning($"{nameof(SceneEntityFactory<E>)}: Create() returned null.", this);
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