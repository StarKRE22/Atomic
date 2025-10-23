namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="StateChangedEntityTrigger{IEntity}"/>.
    /// Subscribes to state change events (<c>OnStateChanged</c>) on basic <see cref="IEntity"/> instances.
    /// </summary>
    public class StateChangedEntityTrigger : StateChangedEntityTrigger<IEntity>
    {
    }
}