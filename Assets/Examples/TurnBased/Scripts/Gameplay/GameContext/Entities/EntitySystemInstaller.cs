using System;
using Atomic.Entities;
using UnityEngine;

namespace Game.Gameplay
{
    [Serializable]
    public sealed class EntitySystemInstaller : IGameContextInstaller
    {
        [SerializeField]
        private GameEntityCatalog _entityCatalog;

        [SerializeField]
        private EntitySpawnInfo[] _initialEntities;

        public void Install(IGameContext context)
        {
            EntityWorld<IGameEntity> entityWorld = new EntityWorld<IGameEntity>();
            context.AddValue(GameContextAPI.InitialEntities, _initialEntities);
            context.AddValue(GameContextAPI.EntityWorld, entityWorld);
            context.AddValue(GameContextAPI.EntityPool,
                new MultiEntityPool<GameEntityType, IGameEntity, Args<IGameContext>>(_entityCatalog,
                    new Args<IGameContext>(context)
                ));

            context.WhenInit(context.SpawnInitialUnits);
            entityWorld.BindTo(context);
        }
    }
}