#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public partial class MonoEntityBaker<TEntity, TArgs>
    {
        [Header("Editor")]
#if ODIN_INSPECTOR
        [PropertyOrder(900)]
        [HideInPlayMode]
#endif
        [Tooltip("Should precompute capacities when OnValidate happens?")]
        [SerializeField]
        private bool autoCompile;

#if ODIN_INSPECTOR
        [Title("Debug")]
        [PropertyOrder(2000)]
        [ShowInInspector, ReadOnly]
        [HideInPlayMode]
#endif
        private protected IEntity _previewEntity;

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
        [HideInPlayMode]
#endif
        [ContextMenu(nameof(Compile))]
        private protected virtual void Compile()
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                return;

            try
            {
                _previewEntity = this.Create(
                    this.tagCapacity,
                    this.valueCapacity,
                    this.behaviourCapacity,
                    this.settings
                    ,default
                );
                
                if (_previewEntity == null)
                {
                    Debug.LogWarning($"{nameof(MonoEntityBaker<TEntity, TArgs>)}: Create() returned null.",
                        this);
                }
                else
                {
                    this.tagCapacity = _previewEntity.TagCount;
                    this.valueCapacity = _previewEntity.ValueCount;
                    this.behaviourCapacity = _previewEntity.BehaviourCount;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"<color=#FF3C3C>{this.name} Compilation failed: {ex.Message}</color>\n{ex.StackTrace}",
                    this);
            }
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
        [HideInPlayMode]
#endif
        protected virtual void Reset()
        {
#if UNITY_EDITOR
            this.tagCapacity = 0;
            this.valueCapacity = 0;
            this.behaviourCapacity = 0;
#endif
        }
    }
}
#endif