#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "TimerEntityInstaller")]
    [Serializable]
    public sealed class Timer_EntityInstaller : ValueEntityInstaller<Timer>
    {
        [SerializeReference]
        private IEntityActionAsset[] completeActions = default;

        [SerializeField]
        private UpdateMode updateMode;
        
        private enum UpdateMode
        {
            UPDATE = 0,
            FIXED_UPDATE = 1,
            LATE_UPDATE = 2,
            MANUAL = 3
        }
        
        public override void Install(IEntity entity)
        {
            this.value.SubscribeOnCompleteBy(this.completeActions, entity);
            this.ControlByUpdate(entity);
            base.Install(entity);
        }

        private void ControlByUpdate(IEntity entity)
        {
            if (this.updateMode == UpdateMode.UPDATE)
            {
                entity.WhenUpdate(this.value.Tick);
            }

            if (this.updateMode == UpdateMode.FIXED_UPDATE)
            {
                entity.WhenFixedUpdate(this.value.Tick);
            }

            if (this.updateMode == UpdateMode.LATE_UPDATE)
            {
                entity.WhenLateUpdate(this.value.Tick);
            }
        }
    }
}
#endif