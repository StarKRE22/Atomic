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
        private PlayerContextBuilder _playerFactory;

        public void Install(IGameContext context)
        {
            _playerFactory.SetEntityWorld(context.GetEntityWorld());

            context.AddPlayers(new Dictionary<TeamType, IPlayerContext>
            {
                {TeamType.BLUE, this.CreatePlayerContext(TeamType.BLUE)},
                {TeamType.RED, this.CreatePlayerContext(TeamType.RED)}
            });
        }

        private IPlayerContext CreatePlayerContext(TeamType teamType) =>
            _playerFactory.SetTeamType(teamType).Create();
    }
}