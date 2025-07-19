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
    public partial interface IEntity<E> where E : IEntity<E>
    {
        /// <summary>
        /// Unique identifier of the entity.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Optional user-defined name of the entity.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Clears all data from the entity (tags, values, behaviours).
        /// </summary>
        void Clear();
    }
    
    // public interface IEntity : IEntity<IEntity>
    // {
    // }
}