namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic shortcut for <see cref="TagEntityTrigger{IEntity}"/>.
    /// Subscribes to tag-related events (<c>OnTagAdded</c>, <c>OnTagDeleted</c>) on basic <see cref="IEntity"/> instances.
    /// </summary>
    public class TagEntityTrigger : TagEntityTrigger<IEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagEntityTrigger"/> class.
        /// </summary>
        /// <param name="added">Whether to react to tag additions via <c>OnTagAdded</c>.</param>
        /// <param name="deleted">Whether to react to tag removals via <c>OnTagDeleted</c>.</param>
        public TagEntityTrigger(bool added = true, bool deleted = true) : base(added, deleted)
        {
        }
    }
}