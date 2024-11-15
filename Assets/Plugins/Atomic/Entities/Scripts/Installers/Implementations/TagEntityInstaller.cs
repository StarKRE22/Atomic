#if ODIN_INSPECTOR
using System;
using Atomic.Entities;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public sealed class TagEntityInstaller : IEntityInstaller
    {
        [EntityTag]
        [SerializeField]
        private int[] tags;

        public int[] Tag => this.tags;

        public void Install(IEntity entity)
        {
            if (this.tags != null)
            {
                for (int i = 0, count = this.tags.Length; i < count; i++)
                {
                    entity.AddTag(this.tags[i]);
                }
            }
            
        }
    }
}
#endif