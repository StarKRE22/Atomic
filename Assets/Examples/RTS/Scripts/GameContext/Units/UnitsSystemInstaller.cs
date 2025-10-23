using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class UnitsSystemInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private UnitMultiFactory factoryCatalog;

        public void Install(IGameContext context)
        {
            context.AddEntityPool(new MultiEntityPool<string, IUnitEntity>(factoryCatalog));

            EntityWorld<IUnitEntity> entityWorld = new EntityWorld<IUnitEntity>();
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