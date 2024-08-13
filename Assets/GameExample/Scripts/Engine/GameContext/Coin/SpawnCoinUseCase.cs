using Atomic.Contexts;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameExample.Engine
{
    public static class SpawnCoinUseCase
    {
        public static IEntity SpawnCoinInArea(this IContext gameContext)
        {
            float3 spawnPoint = gameContext.RandomCoinSpawnPoint();
            return gameContext.SpawnCoin(spawnPoint);
        }

        public static IEntity SpawnCoin(this IContext gameContext, float3 spawnPoint)
        {
            IEntityPool coinPool = gameContext.GetCoinSystemData().pool;
            IEntity coin = coinPool.Rent();
            coin.GetPosition().Value = spawnPoint;
            return coin;
        }

        private static float3 RandomCoinSpawnPoint(this IContext gameContext)
        {
            Bounds spawnArea = gameContext.GetCoinSystemData().spawnArea;
            float3 min = spawnArea.min;
            float3 max = spawnArea.max;
            float3 spawnPoint = new float3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
            return spawnPoint;
        }
    }
}