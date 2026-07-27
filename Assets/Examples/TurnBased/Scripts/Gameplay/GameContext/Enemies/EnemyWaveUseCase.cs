using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public static class EnemyWaveUseCase
    {
        public static bool IsLastWave(this IGameContext context)
        {
            int currentTurn = context.GetCurrentTurn().Value;
            IList<EntitySpawnInfo> waves = context.GetEnemyWaves();
            return currentTurn >= waves.Count;
        }
        
        public static bool TryGetCurrentWave(this IGameContext context, out EntitySpawnInfo enemyWave)
        {
            IList<EntitySpawnInfo> waves = context.GetEnemyWaves();
            int waveIndex = context.GetCurrentTurn().Value - 1;
            if (waveIndex < waves.Count)
            {
                enemyWave = waves[waveIndex];
                return true;
            }

            enemyWave = null;
            return false;
        }
        
        public static void SpawnEnemyWave(this IGameContext context)
        {
            if (!context.TryGetCurrentWave(out EntitySpawnInfo wave))
                return;
    
            GameEntityBoard gameBoard = context.GetGameBoard();
            Vector2Int[] spawnPoints = wave.points;

            foreach (Vector2Int position in spawnPoints)
                if (!gameBoard.TryGetEntity(position, out IGameEntity _))
                    context.Spawn(wave.entityType, position, out _);
            
            IGameEventBus eventBus = context.GetEventBus();
            eventBus.Flush();
        }
    }
}