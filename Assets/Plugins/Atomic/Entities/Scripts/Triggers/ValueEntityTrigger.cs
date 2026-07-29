namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="ValueEntityTrigger{IEntity}"/>.
    /// Provides value-based tracking behavior for basic <see cref="IEntity"/> instances,
    /// including reactions to value additions, removals, and changes.
    /// </summary>
    public class ValueEntityTrigger : ValueEntityTrigger<IEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueEntityTrigger"/> class
        /// with configurable listening behavior for value events.
        /// </summary>
        /// <param name="added">Whether to react to value additions via <c>OnValueAdded</c>.</param>
        /// <param name="deleted">Whether to react to value removals via <c>OnValueDeleted</c>.</param>
        /// <param name="changed">Whether to react to value changes via <c>OnValueChanged</c>.</param>
        public ValueEntityTrigger(bool added = true, bool deleted = true, bool changed = true) : base(added, deleted, changed)
        {
        }
    }
}