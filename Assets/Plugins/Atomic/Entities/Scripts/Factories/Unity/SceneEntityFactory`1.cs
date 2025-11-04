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
    /// Base factory class for creating scene entities.
    /// </summary>
    /// <typeparam name="E">The type of entity produced by this factory. Must implement <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// This class inherits from <see cref="MonoBehaviour"/> and is designed to be used both at runtime
    /// and in the Unity Editor. Several operations (such as <see cref="Precompile"/>) are editor-only
    /// and wrapped in <c>UNITY_EDITOR</c> compilation directives.
    /// 
    /// Derived classes must implement <see cref="Create"/> to construct new entity instances.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Factories/SceneEntityFactory%601.md")]
    public abstract partial class SceneEntityFactory<E> : MonoBehaviour, IEntityFactory<E> where E : IEntity
    {
#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
#endif
        [Header("Optimization")]
        [Tooltip("Initial number of tags to assign to the entity")]
        [SerializeField]
        protected int initialTagCapacity;

#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
#endif
        [Tooltip("Initial number of values to assign to the entity")]
        [SerializeField]
        protected int initialValueCapacity;

#if ODIN_INSPECTOR
        [PropertyOrder(1200)]
        [FoldoutGroup("Optimization")]
#endif
        [Tooltip("Initial number of behaviours to assign to the entity")]
        [SerializeField]
        protected int initialBehaviourCapacity;

        [Header("Editor")]
#if ODIN_INSPECTOR
        [PropertyOrder(900)]
#endif
        [Tooltip("Should precompute capacities when OnValidate happens?")]
        [SerializeField]
        private bool autoCompile = true;

#if ODIN_INSPECTOR
        [Title("Debug")]
        [PropertyOrder(2000)]
        [ShowInInspector, ReadOnly]
#endif
        private protected IEntity _previewEntity;

        /// <summary>
        /// Creates and returns a new instance of the entity.
        /// </summary>
        /// <returns>A new <typeparamref name="E"/> instance.</returns>
        public abstract E Create();

        /// <summary>
        /// Unity callback invoked when the script is loaded or a value is changed in the Inspector.
        /// Used here to update cached metadata via <see cref="Compile"/>.
        /// </summary>
        protected virtual void OnValidate()
        {
            if (this.autoCompile)
                this.Compile();
        }


        /// <summary>
        /// Generates a preview entity and extracts metadata such as tag count, value count, and name.
        /// This is useful for optimizing asset previews and reducing runtime introspection.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
        [PropertyOrder(900)]
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [ContextMenu(nameof(Compile))]
        private protected virtual void Compile()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                return;

            try
            {
                _previewEntity = this.Create();
                if (_previewEntity == null)
                {
                    Debug.LogWarning($"{nameof(SceneEntityFactory<E>)}: Create() returned null.",
                        this);
                }
                else
                {
                    this.initialTagCapacity = _previewEntity.TagCount;
                    this.initialValueCapacity = _previewEntity.ValueCount;
                    this.initialBehaviourCapacity = _previewEntity.BehaviourCount;
                }
            }
            catch (Exception ex)
            {
                    Debug.LogError($"<color=#FF3C3C>{this.name} Compilation failed: {ex.Message}</color>\n{ex.StackTrace}", this);
            }
            
            // Debug.Log($"<color=#00D4FF>{this.name} Compilation completed successfully!</color>", this);
#endif
        }

        /// <summary>
        /// Unity callback used to reset factory fields to their default values.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
        [PropertyOrder(900)]
        [GUIColor(1f, 0.92f, 0.02f)]
        [PropertySpace(SpaceBefore = 4, SpaceAfter = 4)]
#endif
        protected virtual void Reset()
        {
#if UNITY_EDITOR
            this.initialTagCapacity = 0;
            this.initialValueCapacity = 0;
            this.initialBehaviourCapacity = 0;
            // Debug.Log($"<color=#FFEB04>{this.name} Reset completed successfully!</color>", this);
#endif
        }
    }
}
#endif