using System.Buffers;
using Atomic.Entities;
using Modules.SpatialStructures;
using Unity.Profiling;
using UnityEngine;

namespace RTSGame
{
    public static class UnitsUseCase
    {
        private static readonly IGameEntity[] s_buffer = new IGameEntity[512];
        
#if ENABLE_PROFILER
        private static readonly ProfilerMarker FindClosestEnemyMarker = new("UnitUseCase.FindClosestEnemy");
        private static readonly ProfilerMarker SpawnMarker = new("UnitUseCase.Spawn");
        private static readonly ProfilerMarker DespawnMarker = new("UnitUseCase.Despawn");
#endif

        public static IGameEntity Spawn(
            this IGameContext context,
            GameEntityType name,
            Vector3 position,
            Quaternion rotation,
            TeamType team
        )
        {
#if ENABLE_PROFILER
            using (SpawnMarker.Auto())
#endif
            {
                IMultiEntityPool<GameEntityType, IGameEntity> pool = context.GetEntityPool();
                IGameEntity entity = pool.Rent(name);
                entity.GetPosition().Value = position;
                entity.GetRotation().Value = rotation;
                entity.GetTeam().Value = team;
                context.GetEntityWorld().Add(entity);
                return entity;
            }
        }

        public static bool Despawn(this IGameContext gameContext, IGameEntity entity)
        {
#if ENABLE_PROFILER
            using (DespawnMarker.Auto())
#endif
            {
                if (!gameContext.GetEntityWorld().Remove(entity))
                    return false;

                gameContext.GetEntityPool().Return(entity);
                return true;
            }
        }

        public static IGameEntity FindClosestEnemy(this IGameContext gameContext, IGameEntity self)
        {
#if ENABLE_PROFILER
            using (FindClosestEnemyMarker.Auto())
#endif
            {
                IGameEntity closestEnemy = null;
                float closestSqr = float.MaxValue;

                Vector3 selfPos = self.GetPosition().Value;
                TeamType teamType = self.GetTeam().Value;
                float radius = self.GetDetectionRadius().Value;

                SpatialGrid2D<IGameEntity> spatialHash = gameContext.GetEntitySpace();

                Vector2 center = new Vector2(selfPos.x, selfPos.z);
                int enemyCount = spatialHash.QueryRadius(center, radius, s_buffer);
                
                for (int i = 0; i < enemyCount; i++)
                {
                    IGameEntity enemy = s_buffer[i];
                    if (enemy.Equals(self) || !enemy.HasUnitTag() || enemy.GetTeam().Value == teamType)
                        continue;

                    Vector3 dir = enemy.GetPosition().Value - selfPos;
                    float sqrDist = dir.sqrMagnitude;

                    if (sqrDist < closestSqr)
                    {
                        closestSqr = sqrDist;
                        closestEnemy = enemy;
                    }
                }
                
                return closestEnemy;
            }
        }
    }
}

// // ArrayPool<IGameEntity> arrayPool = ArrayPool<IGameEntity>.Shared;
// // IGameEntity[] buffer = arrayPool.Rent(128);
//
// try
// {
// // float detectionRadius = self.GetDetectionRadius().Value;
//     
//     // SpatialGrid2D<IGameEntity> spatialGrid = gameContext.GetSpatialGrid();
//     // int foundCount = spatialGrid.QueryRadius(selfPos, detectionRadius, buffer);
//     
// }
// finally
// {
//     arrayPool.Return(buffer);
// }