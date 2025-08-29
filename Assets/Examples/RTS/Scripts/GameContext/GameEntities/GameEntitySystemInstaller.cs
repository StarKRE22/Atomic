using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class GameEntitySystemInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private GameEntityCatalog _catalog;

        public void Install(IGameContext context)
        {
            context.AddEntityPool(new MultiEntityPool<string, IGameEntity>(
                new MultiEntityFactory<string, IGameEntity>(_catalog)
            ));

            EntityWorld<IGameEntity> entityWorld = new EntityWorld<IGameEntity>();
            context.AddEntityWorld(entityWorld);
            
            context.WhenInit(entityWorld.InitEntities);
            context.WhenEnable(entityWorld.Enable);
            context.WhenUpdate(entityWorld.OnUpdate);
            context.WhenFixedUpdate(entityWorld.OnFixedUpdate);
            context.WhenLateUpdate(entityWorld.OnLateUpdate);
            context.WhenDisable(entityWorld.Disable);
            context.WhenDispose(entityWorld.DisposeEntities);
        }
    }
}