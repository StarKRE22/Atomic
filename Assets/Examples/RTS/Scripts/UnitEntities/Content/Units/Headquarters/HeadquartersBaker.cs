using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class HeadquartersBaker : UnitBaker
    {
        [SerializeField]
        private LifeEntityBaker _lifeBaker;

        [SerializeField]
        private TeamEntityBaker _teamBaker;

        [SerializeField]
        private TransformEntityBaker _transformBaker;

        protected override void Install(IUnitEntity entity)
        {
            entity.Install(_lifeBaker);
            entity.Install(_teamBaker);
            entity.Install(_transformBaker);
        }

        private void OnValidate()
        {
            _teamBaker.OnValidate();
        }
    }
}