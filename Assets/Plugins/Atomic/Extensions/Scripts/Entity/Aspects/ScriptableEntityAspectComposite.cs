using Atomic.Entities;
using UnityEngine;

namespace Atomic.Extensions
{
    [CreateAssetMenu(
        fileName = "Scriptable Aspect Composite",
        menuName = "Atomic/Entities/New ScriptableAspectComposite"
    )]
    public sealed class ScriptableEntityAspectComposite : ScriptableEntityAspect
    {
        [SerializeField]
        private ScriptableEntityAspect[] _aspects;
        
        public override void Apply(IEntity entity)
        {
            for (int i = 0, count = _aspects.Length; i < count; i++) 
                _aspects[i].Apply(entity);
        }

        public override void Discard(IEntity entity)
        {
            for (int i = 0, count = _aspects.Length; i < count; i++) 
                _aspects[i].Discard(entity);
        }
    }
}