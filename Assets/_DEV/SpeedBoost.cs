using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace _DEV
{
    public sealed class SpeedBoost : SceneEntityAspect
    {
        [SerializeField]
        private float _multiplier = 1.5f;

        public override void Apply(IEntity entity) =>
            entity.GetValue<IVariable<float>>("Speed").Value *= _multiplier;

        public override void Discard(IEntity entity) => 
            entity.GetValue<IVariable<float>>("Speed").Value /= _multiplier;
    }
}