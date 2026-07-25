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
        private UnitPrioritySettings _unitPrioritySettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _attackTargetSettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _detectTargetSettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _moveUnitSettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _fireUnitsSettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _projectileMoveSettings;

        [SerializeField]
        private GamePriorityEntitySystemSettings _projectileLifetimeSettings;

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

            UnitPrioritySystem unitPrioritySystem = new(context, _unitPrioritySettings);
            AttackTargetSystem attackTargetSystem = new(context, _attackTargetSettings);
            DetectTargetSystem detectTargetSystem = new(context, _detectTargetSettings);
            MoveUnitSystem moveUnitSystem = new(context, _moveUnitSettings);
            FireUnitsSystem fireUnitsSystem = new(context, _fireUnitsSettings);
            ProjectileMoveSystem projectileMoveSystem = new(context, _projectileMoveSettings);
            ProjectileLifetimeSystem projectileLifetimeSystem = new(context, _projectileLifetimeSettings);

            context.AddFixedSystem(unitPrioritySystem);
            context.AddFixedSystem(attackTargetSystem);
            context.AddFixedSystem(detectTargetSystem);
            context.AddFixedSystem(moveUnitSystem);
            context.AddFixedSystem(fireUnitsSystem);
            context.AddFixedSystem(projectileMoveSystem);
            context.AddFixedSystem(projectileLifetimeSystem);

            context.WhenDispose(unitPrioritySystem.Dispose);
            context.WhenDispose(attackTargetSystem.Dispose);
            context.WhenDispose(detectTargetSystem.Dispose);
            context.WhenDispose(moveUnitSystem.Dispose);
            context.WhenDispose(fireUnitsSystem.Dispose);
            context.WhenDispose(projectileMoveSystem.Dispose);
            context.WhenDispose(projectileLifetimeSystem.Dispose);

            entityWorld.BindTo(context);
        }
    }
}
