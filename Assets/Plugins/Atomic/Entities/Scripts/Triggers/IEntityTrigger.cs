namespace Atomic.Entities
{
    /// <summary>
    /// Represents a trigger that reacts to an <see cref="IEntity"/> interaction or condition.
    /// This is a shorthand for <see cref="IEntityTrigger{IEntity}"/> with the default entity type.
    /// </summary>
    public interface IEntityTrigger : IEntityTrigger<IEntity>
    {
    }
}