using System.Linq;
using Atomic.Entities;
using Atomic.Events;
using UnityEngine;

namespace Game.Gameplay
{
    public static class EntityUseCase
    {
        public static void SpawnInitialUnits(this IGameContext gameContext)
        {
            EntitySpawnInfo[] spawnDataSet = gameContext.GetValue(GameContextAPI.InitialEntities);
            foreach (EntitySpawnInfo spawnInfo in spawnDataSet)
            {
                GameEntityType entityType = spawnInfo.entityType;
                foreach (Vector2Int point in spawnInfo.points)
                    gameContext.Spawn(entityType, point, out _, notify: false);
            }
        }

        public static bool Spawn(
            this IGameContext context,
            GameEntityType type,
            Vector2Int position,
            out IGameEntity entity,
            bool notify = true
        )
        {
            entity = null;

            GameEntityBoard entityBoard = context.GetValue(GameContextAPI.GameBoard);
            if (!entityBoard.IsFreePosition(position))
                return false;

            IMultiEntityPool<GameEntityType, IGameEntity> pool = context.GetValue(GameContextAPI.EntityPool);
            entity = pool.Rent(type);
            entityBoard.PlaceEntity(entity, position);

            IEntityWorld<IGameEntity> entityWorld = context.GetValue(GameContextAPI.EntityWorld);
            entityWorld.Add(entity);

            entity.GetValue(GameEntityAPI.RespawnAction).Invoke();

            if (notify)
            {
                IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
                eventBus.Invoke(GameEventAPI.EntitySpawned, new SpawnEventArgs(entity, position));
            }

            return true;
        }

        public static bool Despawn(this IGameContext context, IGameEntity entity)
        {
            GameEntityBoard entityBoard = context.GetValue(GameContextAPI.GameBoard);
            IEntityWorld<IGameEntity> entityWorld = context.GetValue(GameContextAPI.EntityWorld);
            if (!entityWorld.Remove(entity))
                return false;

            entityBoard.RemoveEntity(entity);
            IMultiEntityPool<GameEntityType, IGameEntity> pool = context.GetValue(GameContextAPI.EntityPool);
            pool.Return(entity);

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.EntityDespawned, entity);
            return true;
        }

        public static void DespawnDeadEntities(this IGameContext context)
        {
            GameEntityBoard gameBoard = context.GetValue(GameContextAPI.GameBoard);
            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);

            foreach (IGameEntity entity in gameBoard.Entities.Keys.ToArray())
            {
                if (!entity.HealthExists())
                {
                    eventBus.Invoke(GameEventAPI.EntityDied, entity);
                    context.Despawn(entity);
                }
            }
        }

        public static bool DespawnIfDead(this IGameContext context, IGameEntity entity)
        {
            if (entity.HealthExists())
                return false;

            IGameEventBus eventBus = context.GetValue(GameContextAPI.EventBus);
            eventBus.Invoke(GameEventAPI.EntityDied, entity);
            return context.Despawn(entity);
        }
    }
}