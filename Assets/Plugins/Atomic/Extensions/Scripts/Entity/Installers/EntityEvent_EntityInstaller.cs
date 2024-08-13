#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityEvent_EntityInstaller : ValueEntityInstaller<Event<IEntity>>
    {
        [Header("Actions")]
        [SerializeReference]
        private IEntityActionAsset[] baseActions = default;

        [SerializeReference]
        private IEntityActionAsset_Entity[] targetActions = default;
        
        public override void Install(IEntity entity)
        {
            base.Install(entity);
            this.value.SubscribeAllBy(this.baseActions, entity);
            this.value.SubscribeAllBy(this.targetActions, entity);
        }
    }
}
#endif