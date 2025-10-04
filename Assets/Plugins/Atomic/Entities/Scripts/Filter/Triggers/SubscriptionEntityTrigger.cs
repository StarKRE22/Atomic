namespace Atomic.Entities
{
    /// <summary>
    /// Base trigger for working with entities using subscriptions.
    /// Provides infrastructure for tracking entities and managing subscription resources.
    /// </summary>
    public abstract class SubscriptionEntityTrigger : SubscriptionEntityTrigger<IEntity>
    {
    }
}