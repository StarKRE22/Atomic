#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public sealed class EntityAndExpression_EntityInstaller : IEntityInstaller
    {
        [EntityValue, SerializeField]
        private int id = -1;

#if ODIN_INSPECTOR
        [FoldoutGroup("Conditions")]
#endif
        [Space, SerializeReference]
        private IEntityPredicate[] baseConditions = default;

#if ODIN_INSPECTOR
        [FoldoutGroup("Conditions")]
#endif
        [Space, SerializeReference]
        private IEntityPredicate[] targetCondtions = default;

#if ODIN_INSPECTOR
        [FoldoutGroup("Conditions")]
#endif
        [Space, SerializeReference]
        private IEntityPredicate_Entity[] interactionConditions = default;
        
        public void Install(IEntity entity)
        {
            AndExpression<IEntity> expression = new AndExpression<IEntity>();
            expression.AppendBy(this.baseConditions, entity);
            expression.AppendAll(this.targetCondtions);
            expression.AppendBy(this.interactionConditions, entity);

            entity.AddValue(this.id, expression);
        }  
    }
}
#endif