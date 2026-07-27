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
            EntitySpawnInfo[] spawnDataSet = gameContext.GetInitialEntities();
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

            GameEntityBoard entityBoard = context.GetGameBoard();
            if (!entityBoard.IsFreePosition(position))
                return false;

            IMultiEntityPool<GameEntityType, IGameEntity> pool = context.GetEntityPool();
            entity = pool.Rent(type);
            entityBoard.PlaceEntity(entity, position);

            IEntityWorld<IGameEntity> entityWorld = context.GetEntityWorld();
            entityWorld.Add(entity);

            entity.GetRespawnAction().Invoke();

            if (notify)
            {
                IGameEventBus eventBus = context.GetEventBus();
                eventBus.InvokeEntitySpawned( new SpawnEventArgs(entity, position));
            }

            return true;
        }

        public static bool Despawn(this IGameContext context, IGameEntity entity)
        {
            GameEntityBoard entityBoard = context.GetGameBoard();
            IEntityWorld<IGameEntity> entityWorld = context.GetEntityWorld();
            if (!entityWorld.Remove(entity))
                return false;

            entityBoard.RemoveEntity(entity);
            IMultiEntityPool<GameEntityType, IGameEntity> pool = context.GetEntityPool();
            pool.Return(entity);

            IGameEventBus eventBus = context.GetEventBus();
            eventBus.InvokeEntityDespawned( entity);
            return true;
        }

        public static void DespawnDeadEntities(this IGameContext context)
        {
            GameEntityBoard gameBoard = context.GetGameBoard();
            IGameEventBus eventBus = context.GetEventBus();

            foreach (IGameEntity entity in gameBoard.Entities.Keys.ToArray())
            {
                if (!entity.HealthExists())
                {
                    eventBus.InvokeEntityDied( entity);
                    context.Despawn(entity);
                }
            }
        }

        public static bool DespawnIfDead(this IGameContext context, IGameEntity entity)
        {
            if (entity.HealthExists())
                return false;

            IGameEventBus eventBus = context.GetEventBus();
            eventBus.InvokeEntityDied( entity);
            return context.Despawn(entity);
        }
    }
}