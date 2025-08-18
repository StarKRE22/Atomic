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

            IGameEntityWorld entityWorld = new GameEntityWorld();
            context.AddEntityWorld(entityWorld);
            
            context.WhenSpawn(entityWorld.Spawn);
            context.WhenActivate(entityWorld.Activate);
            context.WhenUpdate(entityWorld.OnUpdate);
            context.WhenFixedUpdate(entityWorld.OnFixedUpdate);
            context.WhenLateUpdate(entityWorld.OnLateUpdate);
            context.WhenDeactivate(entityWorld.Deactivate);
            context.WhenDespawn(entityWorld.Despawn);
        }
    }
}