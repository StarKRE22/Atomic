using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    /// <summary>
    /// Handles automatic spawning of coin entities within a defined area at fixed time intervals.
    /// </summary>
    /// <remarks>
    /// This behaviour retrieves spawn configuration data from the <see cref="SpawnInfo"/> value
    /// and periodically spawns new coin entities using <see cref="SceneEntity.Create"/>.  
    /// The spawn area and interval are fully configurable through the <see cref="SpawnInfo"/> asset.
    /// </remarks>
    /// <seealso cref="SpawnInfo"/>
    /// <seealso cref="SceneEntity.Create(SceneEntity, Vector3, Quaternion, Transform)"/>

    public sealed class CoinSpawnBehaviour : IEntityInit, IEntityFixedTick, IEntityGizmos
    {
        private SpawnInfo _spawnInfo;
        
        public void Init(IEntity entity)
        {
            _spawnInfo = entity.GetCoinSpawnInfo();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            Cooldown period = _spawnInfo.period;
            period.Tick(deltaTime);
           
            if (period.IsCompleted())
            {
                this.SpawnCoin();
                period.ResetTime();
            }
        }

        private void SpawnCoin()
        {
            Vector3 position = GetRandomPosition();
            SceneEntity.Create(_spawnInfo.prefab, position, Quaternion.identity, _spawnInfo.container);
        }

        private Vector3 GetRandomPosition()
        {
            Bounds spawnArea = _spawnInfo.area;
            Vector3 min = spawnArea.min;
            Vector3 max = spawnArea.max;
            return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
        }

        public void DrawGizmos(IEntity entity)
        {
            Bounds spawnArea = entity.GetCoinSpawnInfo().area;
            
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.black;
            Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
            Gizmos.color = prevColor;
        }
    }
}