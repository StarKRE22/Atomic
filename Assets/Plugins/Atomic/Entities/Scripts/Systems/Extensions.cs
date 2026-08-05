namespace Atomic.Entities
{
    public static partial class Extensions
    {
        /// <summary>
        /// Registers an entity system that is updated during the Tick phase of the context entity lifecycle.
        /// </summary>
        /// <typeparam name="TContext">Type of the context entity.</typeparam>
        /// <typeparam name="TEntity">Type of entities processed by the system.</typeparam>
        /// <param name="entity">The context entity.</param>
        /// <param name="system">The entity system to register.</param>    
        public static void AddTickSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenTick(system.Update);
            entity.WhenDisable(system.Disable);
            entity.WhenDispose(system.Dispose);
        }

        /// <summary>
        /// Registers an entity system that is updated during the FixedTick phase of the context entity lifecycle.
        /// </summary>
        /// <typeparam name="TContext">Type of the context entity.</typeparam>
        /// <typeparam name="TEntity">Type of entities processed by the system.</typeparam>
        /// <param name="entity">The context entity.</param>
        /// <param name="system">The entity system to register.</param>
        public static void AddFixedTickSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenFixedTick(system.Update);
            entity.WhenDisable(system.Disable);
            entity.WhenDispose(system.Dispose);
        }

        /// <summary>
        /// Registers an entity system that is updated during the LateTick phase of the context entity lifecycle.
        /// </summary>
        /// <typeparam name="TContext">Type of the context entity.</typeparam>
        /// <typeparam name="TEntity">Type of entities processed by the system.</typeparam>
        /// <param name="entity">The context entity.</param>
        /// <param name="system">The entity system to register.</param>
        public static void AddLateTickSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenLateTick(system.Update);
            entity.WhenDisable(system.Disable);
            entity.WhenDispose(system.Dispose);
        }
    }
}
