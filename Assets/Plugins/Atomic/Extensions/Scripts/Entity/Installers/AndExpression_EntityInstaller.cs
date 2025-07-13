#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class AndExpression_EntityInstaller : ValueEntityInstaller<AndExpression>
    {
        [SerializeReference]
        private IEntityPredicateAsset[] conditions = default;

        public override void Install(IEntity entity)
        {
            base.Install(entity);
            this.value.AppendBy(this.conditions, entity);
        }
    }
}
#endif