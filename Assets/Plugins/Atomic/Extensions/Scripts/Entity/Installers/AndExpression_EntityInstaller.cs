#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "AndExpressionEntityInstaller")] 
    [Serializable]
    public sealed class AndExpression_EntityInstaller : ValueEntityInstaller<AndExpression>
    {
        [SerializeReference]
        private IEntityConditionAsset[] conditions = default;

        public override void Install(IEntity entity)
        {
            base.Install(entity);
            this.value.AppendBy(this.conditions, entity);
        }
    }
}
#endif