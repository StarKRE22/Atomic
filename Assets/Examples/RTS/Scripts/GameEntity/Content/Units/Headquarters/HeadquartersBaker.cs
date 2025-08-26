using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersBaker : GameEntityBaker
    {
        [SerializeField]
        private LifeEntityBaker _lifeBaker;

        [SerializeField]
        private TeamEntityBaker _teamBaker;

        [SerializeField]
        private TransformEntityBaker _transformBaker;

        protected override void Install(IGameEntity entity)
        {
            entity.Install(_lifeBaker);
            entity.Install(_teamBaker);
            entity.Install(_transformBaker);
        } 
    }
}