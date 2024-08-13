using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    [CreateAssetMenu(
        fileName = "Scriptable Aspect",
        menuName = "Atomic/Entities/New ScriptableAspect"
    )]
    public sealed class ScriptableEntityAspect : ScriptableEntityAspectBase
    {
        [SerializeReference]
        private IEntityAspect[] aspects;
        
        public override void Apply(IEntity entity)
        {
            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    this.aspects[i].Apply(entity);
                }
            }
        }

        public override void Discard(IEntity entity)
        {
            if (this.aspects != null)
            {
                for (int i = 0, count = this.aspects.Length; i < count; i++)
                {
                    this.aspects[i].Discard(entity);
                }
            }
        }
    }
}