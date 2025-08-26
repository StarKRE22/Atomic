using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class LifeEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private bool _active;

        [SerializeField]
        private int _current;

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