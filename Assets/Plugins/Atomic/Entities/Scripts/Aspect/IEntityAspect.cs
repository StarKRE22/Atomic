namespace Atomic.Entities
{
    public interface IEntityAspect : IEntityAspect<IEntity>
    {
    }

     public interface IEntityAspect<in E>  where E : IEntity
    {
        void Apply(E entity);
        
        void Discard(E entity);
    }
}