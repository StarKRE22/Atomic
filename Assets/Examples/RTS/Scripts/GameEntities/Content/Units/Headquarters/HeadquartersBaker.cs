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

        protected void OnValidate()
        {
            _teamBaker.OnValidate();
        }

        protected override void Override(IGameEntity entity, Args<IGameContext> args)
        {
            entity.Install(_lifeBaker);
            entity.Install(_teamBaker);
            entity.Install(_transformBaker);
        }
    }
}