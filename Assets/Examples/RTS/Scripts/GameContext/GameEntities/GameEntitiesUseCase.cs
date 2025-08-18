using System;
using System.Collections.Generic;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public static class GameEntitiesUseCase
    {
        private static readonly Predicate<IEntity> DEFAULT_PREDICATE = _ => true;

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

        public static bool DespawnEntity(IGameContext gameContext, IGameEntity entity)
        {
            if (!gameContext.GetEntityWorld().Remove(entity))
                return false;

            gameContext.GetEntityPool().Return(entity);
            return true;
        }

        public static IGameEntity FindClosestEnemyFor(IGameContext context, IGameEntity entity)
        {
            IPlayerContext playerContext = PlayersUseCase.GetPlayerFor(context, entity);
            IEnumerable<IGameEntity> enemyFilter = playerContext.GetEnemyFilter();
            return FindClosest(enemyFilter, entity.GetPosition().Value);
        }

        public static IGameEntity FindClosest(IEnumerable<IGameEntity> entities, Vector3 center)
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

        public static bool FindClosest(
            IGameContext context,
            Vector3 center,
            out IGameEntity result,
            Predicate<IGameEntity> predicate = null
        )
        {
            predicate ??= DEFAULT_PREDICATE;

            result = null;
            IGameEntityWorld world = context.GetEntityWorld();

            float minDistance = float.MaxValue;
            foreach (IGameEntity entity in world)
            {
                if (!predicate.Invoke(entity))
                    continue;

                Vector3 position = entity.GetPosition().Value;
                float distance = Vector3.SqrMagnitude(position - center);
                if (distance < minDistance)
                {
                    result = entity;
                    minDistance = distance;
                }
            }

            return result != null;
        }
    }
}