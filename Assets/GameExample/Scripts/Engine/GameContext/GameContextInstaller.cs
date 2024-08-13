using System.Collections.Generic;
using Atomic.Contexts;
using GameExample.Engine;
using UnityEngine;

namespace GameExample.Content
{
    public sealed class GameContextInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private GameCountdownInstaller gameCountdownInstaller;

        [SerializeField]
        private CoinSystemInstaller coinSystemInstaller;

        public override void Install(IContext context)
        {
            context.AddWorldTransform(this.worldTransform);
            context.AddPlayerMap(new Dictionary<TeamType, IContext>());
            context.AddSystem<GameOverController>();

            context.Install(this.gameCountdownInstaller);
            context.Install(this.coinSystemInstaller);
        }
    }
}