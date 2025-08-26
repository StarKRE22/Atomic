using System;
using Atomic.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private bool _active;
        
        [Space]
        [EnableIf(nameof(_active))]
        [SerializeField]
        private int _current;

        [EnableIf(nameof(_active))]
        [SerializeField]
        private int _max;

        public void Install(IGameEntity entity)
        {
            if (_active)
            {
                entity.GetHealth().SetCurrent(_current);
                entity.GetHealth().SetMax(_max);
            }
        }
    }
}