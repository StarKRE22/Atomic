using System;
using Atomic.Entities;
using Modules.SpatialStructures;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class EntitySystemInstaller : IEntityInstaller<IGameContext>
    {
        [SerializeField]
        private GameEntityCatalog _entityCatalog;

        [SerializeField]
        private UnitPrioritySystem _unitPrioritySystem;

        [SerializeField]
        private AttackTargetSystem _attackTargetSystem;
        
        [SerializeField]
        private DetectTargetSystem _detectTargetSystem;

        [SerializeField]
        private MoveUnitSystem _moveUnitSystem;

        [SerializeField]
        private FireUnitsSystem _fireUnitsSystem;

        [SerializeField]
        private ProjectileMoveSystem _projectileMoveSystem;

        [SerializeField]
        private ProjectileLifetimeSystem _projectileLifetimeSystem;
        
        [SerializeField]
        private int sizeX = 2000;

        [SerializeField]
        private int sizeY = 100;
        
        [SerializeField]
        private float cellSize = 20;
        
        public void Install(IGameContext context)
        {
            EntityWorld<IGameEntity> entityWorld = new EntityWorld<IGameEntity>();
            context.AddEntityPool(new MultiEntityPool<GameEntityType, IGameEntity, Args<IGameContext>>(
                _entityCatalog,
                new Args<IGameContext>(context))
            );
            context.AddEntityWorld(entityWorld);
            context.AddEntitySpace(new SpatialGrid2D<IGameEntity>(sizeX, sizeY, cellSize));
            
            // context.AddBehaviour<SpatialGridGizmos>();
            // context.AddBehaviour<SpatialHashGizmos>();
            
            context.AddBehaviour(_unitPrioritySystem);
            context.AddBehaviour(_detectTargetSystem);
            context.AddBehaviour(_attackTargetSystem);
            context.AddBehaviour(_moveUnitSystem);
            context.AddBehaviour(_fireUnitsSystem);
            context.AddBehaviour(_projectileMoveSystem);
            context.AddBehaviour(_projectileLifetimeSystem);
            
            entityWorld.BindTo(context);
        }
    }
}