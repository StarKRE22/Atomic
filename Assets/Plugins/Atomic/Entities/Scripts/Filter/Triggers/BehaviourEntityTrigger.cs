namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="BehaviourEntityTrigger{IEntity}"/>.
    /// Subscribes to behaviour-related events (<c>OnBehaviourAdded</c>, <c>OnBehaviourRemoved</c>) on basic <see cref="IEntity"/> instances.
    /// </summary>
    public class BehaviourEntityTrigger : BehaviourEntityTrigger<IEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BehaviourEntityTrigger"/> class.
        /// </summary>
        /// <param name="added">Whether to react to behaviour additions via <c>OnBehaviourAdded</c>.</param>
        /// <param name="removed">Whether to react to behaviour removals via <c>OnBehaviourRemoved</c>.</param>
        public BehaviourEntityTrigger(bool added = true, bool removed = true) : base(added, removed)
        {
        }
    }
}