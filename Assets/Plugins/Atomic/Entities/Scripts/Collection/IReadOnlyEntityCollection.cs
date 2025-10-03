namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="IReadOnlyEntityCollection{E}"/> for collections of <see cref="IEntity"/>.
    /// </summary>
    public interface IReadOnlyEntityCollection : IReadOnlyEntityCollection<IEntity>
    {
    }
}