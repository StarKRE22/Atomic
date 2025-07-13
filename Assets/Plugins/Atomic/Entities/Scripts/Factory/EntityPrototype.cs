using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public abstract class EntityPrototype : ScriptableObject, IEntityFactory
    {
        public virtual string Name => this.name;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _tagCount;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _valueCount;

#if ODIN_INSPECTOR
        [ReadOnly]
        [FoldoutGroup("Advanced")]
#endif
        [SerializeField]
        private int _behaviourCount;

        public IEntity Create()
        {
            var entity = new Entity(this.Name, _tagCount, _valueCount, _behaviourCount);
            this.Install(entity);
            return entity;
        }

        protected abstract void Install(IEntity entity);

        private void OnValidate()
        {
            try
            {
                this.Precompile();
            }
            catch (Exception e)
            {
                // ignored
            }
        }

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
}