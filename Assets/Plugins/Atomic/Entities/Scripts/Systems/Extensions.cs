namespace Atomic.Entities
{
    public static partial class Extensions
    {
        public static void AddTickSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenTick(system.Update);
            entity.WhenDisable(system.Disable);
            entity.WhenDispose(system.Dispose);
        }

        public static void AddFixedTickSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenFixedTick(system.Update);
            entity.WhenDisable(system.Disable);
            entity.WhenDispose(system.Dispose);
        }

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