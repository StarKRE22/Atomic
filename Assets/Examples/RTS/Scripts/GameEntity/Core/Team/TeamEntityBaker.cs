using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TeamEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Optional<TeamType> _teamType;
        
        public void Install(IGameEntity entity)
        {
            if (_teamType) entity.GetTeam().Value = _teamType;
        }
    }
}