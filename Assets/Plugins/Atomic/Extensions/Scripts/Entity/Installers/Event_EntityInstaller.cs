#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "EventEntityInstaller")] 
    [Serializable]
    public sealed class Event_EntityInstaller : ValueEntityInstaller<BaseEvent>
    {
        [SerializeReference]
        private IEntityActionAsset[] actions = default;
        
        public override void Install(IEntity entity)
        {
            base.Install(entity);
            this.value.SubscribeAllBy(this.actions, entity);
        }
    }
}
#endif