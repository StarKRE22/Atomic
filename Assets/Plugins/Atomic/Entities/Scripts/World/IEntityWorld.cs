namespace Atomic.Entities
{
    public interface IEntityWorld : IEntityWorld<IEntity>, IEntityRunner
    {
    }

    public interface IEntityWorld<E> : IEntityRunner<E> where E : IEntity
    {
        string Name { get; }

        bool FindWithTag(int tag, out E result);
        bool FindWithValue(int valueKey, out E result);

        E[] FindAllWithTag(int tag);
        E[] FindAllWithValue(int valueKey);

        int CopyWithTag(int tag, E[] results);
        int CopyWithValue(int valueKey, E[] results);
    }
}