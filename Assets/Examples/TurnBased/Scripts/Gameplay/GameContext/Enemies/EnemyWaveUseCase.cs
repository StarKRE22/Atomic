using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    public static class EnemyWaveUseCase
    {
        public static bool IsLastWave(this IGameContext context)
        {
            int currentTurn = context.GetValue(GameContextAPI.CurrentTurn).Value;
            IList<EntitySpawnInfo> waves = context.GetValue(GameContextAPI.EnemyWaves);
            return currentTurn >= waves.Count;
        }
        
        public static bool TryGetCurrentWave(this IGameContext context, out EntitySpawnInfo enemyWave)
        {
            IList<EntitySpawnInfo> waves = context.GetValue(GameContextAPI.EnemyWaves);
            int waveIndex = context.GetValue(GameContextAPI.CurrentTurn).Value - 1;
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
    
            GameEntityBoard gameBoard = context.GetValue(GameContextAPI.GameBoard);
            Vector2Int[] spawnPoints = wave.points;

            foreach (Vector2Int position in spawnPoints)
                if (!gameBoard.TryGetEntity(position, out IGameEntity _))
                    context.Spawn(wave.entityType, position, out _);
            
            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Flush();
        }
    }
}