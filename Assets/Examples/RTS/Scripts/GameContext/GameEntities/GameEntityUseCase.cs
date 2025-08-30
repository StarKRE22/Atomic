using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class GameEntityUseCase
    {
        public static IGameEntity Spawn(
            IGameContext context,
            string name,
            Vector3 position,
            Quaternion rotation,
            TeamType team
        )
        {
            IMultiEntityPool<string, IGameEntity> pool = context.GetEntityPool();
            IGameEntity entity = pool.Rent(name);
            entity.GetPosition().Value = position;
            entity.GetRotation().Value = rotation;
            entity.GetTeam().Value = team;
            context.GetEntityWorld().Add(entity);
            return entity;
        }

        public static bool Despawn(IGameContext gameContext, IGameEntity entity)
        {
            if (!gameContext.GetEntityWorld().Remove(entity))
                return false;

            gameContext.GetEntityPool().Return(entity);
            return true;
        }

        public static IGameEntity FindFreeEnemyFor(IGameContext context, IGameEntity entity)
        {
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(context, entity);
            EntityFilter<IGameEntity> enemyFilter = playerContext.GetFreeEnemyFilter();
            Vector3 center = entity.GetPosition().Value;
            return FindClosest(enemyFilter, center);
        }

        public static IGameEntity FindClosest(EntityFilter<IGameEntity> entities, Vector3 center)
        {
            IGameEntity result = null;
        
            float minDistance = float.MaxValue;
            foreach (IGameEntity entity in entities)
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