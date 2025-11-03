using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
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