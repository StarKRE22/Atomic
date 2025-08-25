using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TeamEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private TeamType _teamType;
        
        public void Install(IGameEntity entity)
        {
            entity.GetTeam().Value = _teamType;
        }
    }
}