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
                {TeamType.BLUE, _playerFactory.Create(new Args<TeamType, IGameContext>(TeamType.BLUE, context))},
                {TeamType.RED, _playerFactory.Create(new Args<TeamType, IGameContext>(TeamType.RED, context))}
            });
        }
    }
}