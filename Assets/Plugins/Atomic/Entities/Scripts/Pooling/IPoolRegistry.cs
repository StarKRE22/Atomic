namespace Atomic.Entities
{
    public interface IPoolRegistry<in TKey, E> where E : IEntity<E>
    {
        void Initialize(TKey key, int count);
        
        E Rent(TKey key);
        
        void Return(E entity);

        void Clear(TKey key);

        void Clear();
    }
}