using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;
using GameExample.Engine;
using UnityEngine;

namespace GameExample.Content
{
    [Serializable]
    public sealed class CoinSystemInstaller : IContextInstaller
    {
        [SerializeField]
        private SceneEntity coinPrefab;

        [SerializeField]
        private int initialPoolCount;
        
        [SerializeField]
        private Transform poolTransform;

        [SerializeField]
        private float spawnPeriod = 2;
        
        [SerializeField]
        private Bounds spawnArea = new(Vector3.zero, new Vector3(5, 0, 5));
        
        public void Install(IContext context)
        {
            var worldTransform = context.GetWorldTransform();
            var coinData = new CoinSystemData
            {
                pool = new SceneEntityPool(this.coinPrefab, this.poolTransform, worldTransform, this.initialPoolCount),
                spawnArea = this.spawnArea,
                spawnCycle = new Cycle(this.spawnPeriod)
            };
            
            context.AddCoinSystemData(coinData);
            context.AddSystem<CoinSpawnSystem>();
        }
    }
}