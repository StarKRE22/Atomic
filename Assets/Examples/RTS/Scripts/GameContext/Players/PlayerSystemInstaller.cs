using System;
using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class PlayerSystemInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private PlayerContextFactory _playerFactory;
        
        public void Install(IGameContext context)
        {
            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, this.CreatePlayerContext(TeamType.BLUE, context)},
                {TeamType.RED, this.CreatePlayerContext(TeamType.RED, context)}
            });
        }

        private IPlayerContext CreatePlayerContext(TeamType teamType, IGameContext context)
        {
            _playerFactory.EntityWorld = context.GetEntityWorld(); 
            _playerFactory.TeamType = teamType;
            return _playerFactory.Create();
        }
    }
}