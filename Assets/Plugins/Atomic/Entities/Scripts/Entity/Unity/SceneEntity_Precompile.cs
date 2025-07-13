#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        private int _tagCapacity;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Optimization")]
#endif
        [SerializeField]
        private int _valueCapacity;

#if ODIN_INSPECTOR
        [FoldoutGroup("Optimization")]
        [ReadOnly]
#endif
        [SerializeField]
        private int _behaviourCapacity;

        private void Precompile()
        {
            _tagCapacity = _entity.TagCount;
            _valueCapacity = _entity.ValueCount;
            _behaviourCapacity = _entity.BehaviourCount;
        }
    }
}