using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Base trigger for working with entities using subscriptions.
    /// Provides infrastructure for tracking entities and managing subscription resources.
    /// </summary>
    /// <typeparam name="S">The type of subscription.</typeparam>
    public abstract class SubscriptionEntityTrigger<S> : SubscriptionEntityTrigger<IEntity, S> where S : IDisposable
    {
    }
}