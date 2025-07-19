using System;

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
    public partial interface IEntity<E> : IDisposable where E : IEntity<E>
    {
        int InstanceID { get; }

        string Name { get; set; }

        void Clear();
    }
}