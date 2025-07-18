#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides optimization hints for <see cref="SceneEntity"/> by configuring expected capacities
    /// for tags, values, and behaviours to minimize runtime allocations.
    /// </summary>
    public partial class SceneEntity<E>
    {
        /// <summary>
        /// Initial tag capacity used to optimize tag allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        private int _tagCapacity;

        /// <summary>
        /// Initial value capacity used to optimize value allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        private int _valueCapacity;

        /// <summary>
        /// Initial behaviour capacity used to optimize behaviour allocation.
        /// </summary>
#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization")]
        [ReadOnly]
#endif
        [SerializeField]
        private int _behaviourCapacity;
        
        /// <summary>
        /// Precompiles current capacities from the entity and stores them into serialized fields
        /// for inspection and editor-time optimization.
        /// </summary>
        private void Precompile()
        {
            _tagCapacity = _entity.TagCount;
            _valueCapacity = _entity.ValueCount;
            _behaviourCapacity = _entity.BehaviourCount;
        }
    }
}