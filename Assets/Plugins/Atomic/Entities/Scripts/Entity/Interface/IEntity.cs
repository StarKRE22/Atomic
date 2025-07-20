namespace Atomic.Entities
{
    /// <summary>
    /// Represents the entity based on the Entity-State-Behaviour pattern.
    /// 
    /// An entity encapsulates:
    /// - key-value storage for data
    /// - tag identifiers for categorization
    /// - a collection of behaviours (<see cref="IBehaviour"/>) for modular logic
    /// - lifecycle management (initialization, update, disposal)
    ///
    /// Designed for flexible state-driven architecture and modular logic composition.
    /// </summary>
    public partial interface IEntity
    {
        /// <summary>
        /// Unique instance identifier of the entity. Don't use for save! (Use custom GUID)
        /// </summary>
        int InstanceID { get; }

        /// <summary>
        /// Optional user-defined name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Clears all data from the entity (tags, values, behaviours).
        /// </summary>
        void Clear();
    }
}