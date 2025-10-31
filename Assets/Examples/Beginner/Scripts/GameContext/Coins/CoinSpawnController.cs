using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CoinSpawnController : IGameContextFixedTick, IGameContextGizmos
    {
        private readonly SceneEntity _coinPrefab;
        private readonly Transform _spawnTransform;
        private readonly Bounds _spawnArea;
        private readonly Cooldown _spawnPeriod;

        public CoinSpawnController(
            SceneEntity coinPrefab,
            Transform spawnTransform,
            Bounds spawnArea, Cooldown spawnPeriod)
        {
            _coinPrefab = coinPrefab;
            _spawnArea = spawnArea;
            _spawnTransform = spawnTransform;
            _spawnPeriod = spawnPeriod;
        }

        public void FixedTick(GameContext gameContext, float deltaTime)
        {
            _spawnPeriod.Tick(deltaTime);
            if (_spawnPeriod.IsCompleted())
            {
                this.SpawnCoin();
                _spawnPeriod.ResetTime();
            }
        }

        private void SpawnCoin()
        {
            Vector3 min = _spawnArea.min;
            Vector3 max = _spawnArea.max;
            Vector3 position = new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
            GameEntity.Create(_coinPrefab, position, Quaternion.identity, _spawnTransform);
        }

        public void DrawGizmos(GameContext entity)
        {
            Color prevColor = Gizmos.color;
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(_spawnArea.center, _spawnArea.size);
            Gizmos.color = prevColor;
        }
    }
}