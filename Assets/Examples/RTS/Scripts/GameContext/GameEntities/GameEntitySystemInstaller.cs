using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class GameEntitySystemInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private GameEntityFactoryCatalog factoryCatalog;

        public void Install(IGameContext context)
        {
            context.AddEntityPool(new MultiEntityPool<string, IGameEntity>(
                new MultiEntityFactory<string, IGameEntity>(factoryCatalog)
            ));

            EntityWorld<IGameEntity> entityWorld = new EntityWorld<IGameEntity>();
            context.AddEntityWorld(entityWorld);
            
            context.WhenInit(entityWorld.InitEntities);
            context.WhenEnable(entityWorld.Enable);
            context.WhenTick(entityWorld.Tick);
            context.WhenFixedTick(entityWorld.FixedTick);
            context.WhenLateTick(entityWorld.LateTick);
            context.WhenDisable(entityWorld.Disable);
            context.WhenDispose(entityWorld.DisposeEntities);
            context.WhenDispose(entityWorld.Dispose);
        }
    }
}