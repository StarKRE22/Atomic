#if ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    [Serializable]
    public sealed class EntityReactiveList_EntityInstaller : IEntityInstaller
    {
        [EntityValue]
        [SerializeField]
        private int id = -1;

        [SerializeField]
        private SceneEntity[] initialEntities;
        
        public void Install(IEntity entity)
        {
            entity.AddValue(this.id, new ReactiveList<IEntity>((IEnumerable<IEntity>) this.initialEntities));
        }
    }
}
#endif