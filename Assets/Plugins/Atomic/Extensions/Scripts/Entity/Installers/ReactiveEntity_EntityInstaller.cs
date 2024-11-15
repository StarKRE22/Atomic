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
    public sealed class ReactiveEntity_EntityInstaller : IEntityInstaller
    {
        [EntityValue]
#if ODIN_INSPECTOR
        [HorizontalGroup]
#endif
        [SerializeField]
        private int id = -1;

#if ODIN_INSPECTOR
        [HorizontalGroup, HideLabel]
#endif
        [SerializeField]
        private SceneEntity initialEntity;
        
        public void Install(IEntity entity)
        {
            entity.AddValue(this.id, new ReactiveVariable<IEntity>(this.initialEntity));
        }
    }
}
#endif