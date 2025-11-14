using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class UnitsUseCase
    {
        public static IUnit Spawn(
            IGameContext context,
            string name,
            Vector3 position,
            Quaternion rotation,
            TeamType team
        )
        {
            IMultiEntityPool<string, IUnit> pool = context.GetEntityPool();
            IUnit entity = pool.Rent(name);
            entity.GetPosition().Value = position;
            entity.GetRotation().Value = rotation;
            entity.GetTeam().Value = team;
            context.GetEntityWorld().Add(entity);
            return entity;
        }

        public static bool Despawn(IGameContext gameContext, IUnit entity)
        {
            if (!gameContext.GetEntityWorld().Remove(entity))
                return false;

            gameContext.GetEntityPool().Return(entity);
            return true;
        }

        public static IUnit FindClosestEnemy(IUnit self, IGameContext gameContext, IUnit[] buffer)
        {
            IUnit closestEnemy = null;
            float closestSqr = float.MaxValue;

            Vector3 selfPos = self.GetPosition().Value;
            TeamType teamType = self.GetTeam().Value;
            float detectionRadius = self.GetDetectionRadius().Value;

            // Получаем SpatialHash
            var spatialHash = gameContext.GetSpatialHash();

            // Заполняем буфер найденными юнитами в радиусе
            int foundCount = spatialHash.Query(selfPos, detectionRadius, buffer);

            // Проходим через массив (минимум аллокаций)
            for (int i = 0; i < foundCount; i++)
            {
                IUnit unit = buffer[i];
                if (unit.Equals(self) || unit.GetTeam().Value == teamType)
                    continue;

                Vector3 dir = unit.GetPosition().Value - selfPos;
                float sqrDist = dir.sqrMagnitude;

                if (sqrDist < closestSqr)
                {
                    closestSqr = sqrDist;
                    closestEnemy = unit;
                }
            }

            return closestEnemy;
        }
    }
}