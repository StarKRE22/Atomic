using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using GameExample.Engine;
using UnityEngine;

namespace Walkthrough
{
    public sealed class CoinSystemInstaller : SceneContextInstallerBase
    {
        [SerializeField]
        private SceneEntity coinPrefab;

        [SerializeField]
        private int initialPoolCount;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Transform poolTransform;

        [SerializeField]
        private float spawnPeriod = 2;

        [SerializeField]
        private Bounds spawnArea = new(Vector3.zero, new Vector3(5, 0, 5));

        public override void Install(IContext context)
        {
            var coinData = new CoinSystemData
            {
                pool = new SceneEntityPool(
                    this.coinPrefab, this.poolTransform, this.worldTransform, this.initialPoolCount
                ),
                spawnArea = this.spawnArea,
                spawnCycle = new Cycle(this.spawnPeriod)
            };

            context.AddCoinSystemData(coinData);
            context.AddSystem<CoinSpawnSystem>();
        }
    }
}