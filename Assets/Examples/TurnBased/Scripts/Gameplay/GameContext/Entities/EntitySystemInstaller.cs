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
            context.AddInitialEntities( _initialEntities);
            context.AddEntityWorld( entityWorld);
            context.AddEntityPool(
                new MultiEntityPool<GameEntityType, IGameEntity, Args<IGameContext>>(_entityCatalog,
                    new Args<IGameContext>(context)
                ));

            context.WhenInit(context.SpawnInitialUnits);
            entityWorld.BindTo(context);
        }
    }
}