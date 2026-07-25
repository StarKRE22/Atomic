namespace Atomic.Entities
{
    public static partial class Extensions
    {
        //TODO
        public static void AddFixedSystem<TContext, TEntity>(this TContext entity, EntitySystemBase<TEntity> system)
            where TContext : IEntity
            where TEntity : IEntity
        {
            entity.WhenEnable(system.Enable);
            entity.WhenFixedTick(system.Update);
            entity.WhenDisable(system.Disable);
        }
    }
}