namespace Atomic.Entities
{
    /// <summary>
    /// Provides a base implementation of <see cref="EntityTriggerBase{IEntity}"/> for triggers that operate on <see cref="IEntity"/>.
    /// This serves as a convenient base class when no specific entity type is needed.
    /// </summary>
    public abstract class EntityTriggerBase : EntityTriggerBase<IEntity>
    {
    }
}