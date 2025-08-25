using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersBaker : GameEntityBaker
    {
        [SerializeField]
        private LifeEntityBaker _lifeBaker;

        protected override void Install(IGameEntity entity)
        {
            entity.Install(_lifeBaker);
        } 
    }
}