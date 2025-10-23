namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IEntityWorld{E}"/> specialized for <see cref="IEntity"/>.
    /// Provides a convenient base abstraction when a specific entity type is not required.
    /// </summary>
    public interface IEntityWorld : IEntityWorld<IEntity>
    {
    }
}