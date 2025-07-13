namespace Atomic.Entities
{
    public interface IGenericEntityFactory : IGenericEntityFactory<string>
    {
    }

    public interface IGenericEntityFactory<TKey>
    {
        GenericEntityFactory<TKey> Register(in TKey key, in IEntityFactory prototype);
        GenericEntityFactory<TKey> Unregister(in TKey key);
        IEntity Create(in TKey key);
    }
}