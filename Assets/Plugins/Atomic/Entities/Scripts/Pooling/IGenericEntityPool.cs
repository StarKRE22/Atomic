namespace Atomic.Entities
{
    public interface IGenericEntityPool : IGenericEntityPool<string>
    {
    }

    public interface IGenericEntityPool<in TKey>
    {
        void Initialize(TKey key, in int count);
        IEntity Rent(TKey key);
        void Return(IEntity entity);
    }
}