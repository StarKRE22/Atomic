using System;
using Atomic.Entities;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityBaker : IEntityInstaller<IUnit>
    {
        [SerializeField]
        private bool _active;
        
        [Space]
#if ODIN_INSPECTOR
        [EnableIf(nameof(_active))]
#endif
        [SerializeField]
        private int _current;

#if ODIN_INSPECTOR
        [EnableIf(nameof(_active))]
#endif
        [SerializeField]
        private int _max;

        public void Install(IUnit entity)
        {
            if (_active)
            {
                entity.GetHealth().SetCurrent(_current);
                entity.GetHealth().SetMax(_max);
            }
        }
    }
}