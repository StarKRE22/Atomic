using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class GameEntitiesUseCase
    {
        public static IUnitEntity Spawn(
            IGameContext context,
            string name,
            Vector3 position,
            Quaternion rotation,
            TeamType team
        )
        {
            IMultiEntityPool<string, IUnitEntity> pool = context.GetEntityPool();
            IUnitEntity entity = pool.Rent(name);
            entity.GetPosition().Value = position;
            entity.GetRotation().Value = rotation;
            entity.GetTeam().Value = team;
            context.GetEntityWorld().Add(entity);
            return entity;
        }

        public static bool Despawn(IGameContext gameContext, IUnitEntity entity)
        {
            if (!gameContext.GetEntityWorld().Remove(entity))
                return false;

            gameContext.GetEntityPool().Return(entity);
            return true;
        }

        public static IUnitEntity FindFreeEnemyFor(IGameContext context, IUnitEntity entity)
        {
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(context, entity);
            EntityFilter<IUnitEntity> enemyFilter = playerContext.GetFreeEnemyFilter();
            Vector3 center = entity.GetPosition().Value;
            return FindClosest(enemyFilter, center);
        }

        public static IUnitEntity FindClosest(EntityFilter<IUnitEntity> entities, Vector3 center)
        {
            IUnitEntity result = null;
        
            float minDistance = float.MaxValue;
            foreach (IUnitEntity entity in entities)
            {
                Vector3 position = entity.GetPosition().Value;
                float distance = Vector3.SqrMagnitude(position - center);
                if (distance < minDistance)
                {
                    result = entity;
                    minDistance = distance;
                }
            }
        
            return result;
        }
    }
}