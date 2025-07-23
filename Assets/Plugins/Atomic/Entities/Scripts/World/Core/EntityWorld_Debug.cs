#if UNITY_EDITOR && ODIN_INSPECTOR
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Atomic.Entities
{
    public partial class EntityWorld<E>
    {
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
                foreach (E entity in _entities.Values)
                    result.Add($"{entity.Name}: {entity.InstanceID}");

                return result;
            }
        }
        
        [Serializable]
        private struct EntityDebug
        {
            [HorizontalGroup, ShowInInspector]
            public string Name => _entity.ToString();

            private E _entity;

            public EntityDebug(E entity) => _entity = entity;
        }
    }
}
#endif