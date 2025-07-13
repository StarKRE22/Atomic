#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Entities
{
    public partial class EntityWorld
    {
        private static readonly List<IEntity> s_debugCache = new();

#if ODIN_INSPECTOR
        [ShowInInspector]
        [LabelText(nameof(Name))]
#endif
        private string DebugName
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

#if ODIN_INSPECTOR
        [Searchable]
        [ShowInInspector]
        [LabelText("Entities")]
#endif
        private List<string> DebugEntities
        {
            get
            {
                var result = new List<string>();
                foreach (IEntity entity in _entities.Values)
                    result.Add($"{entity.Name}: {entity.Id}");

                return result;
            }
        }
        
        [Serializable]
        private struct EntityDebug
        {
            [HorizontalGroup, ShowInInspector]
            public string Name => _entity.ToString();

            private IEntity _entity;

            public EntityDebug(IEntity entity) => _entity = entity;
        }
    }
}
#endif