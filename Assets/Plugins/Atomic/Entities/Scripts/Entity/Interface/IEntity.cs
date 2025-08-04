using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents an entity following the Entity–State–Behaviour pattern,
    /// designed for modular logic composition and flexible state-driven architecture.
    /// </summary>
    /// <remarks>
    /// An entity encapsulates:
    /// <list type="bullet">
    ///   <item><description>A key-value data store for dynamic state</description></item>
    ///   <item><description>Tag-based identifiers for categorization or filtering</description></item>
    ///   <item><description>A collection of <see cref="IEntityBehaviour"/> components that define runtime behavior</description></item>
    ///   <item><description>Lifecycle management including spawn, enable, update, disable, and despawn phases</description></item>
    /// </list>
    /// Behaviors are invoked automatically during the corresponding lifecycle events via interfaces such as:
    /// <see cref="IEntitySpawn"/>, <see cref="IEntityActive"/>, <see cref="IEntityUpdate"/>, and others.
    /// </remarks>
    public partial interface IEntity : ISpawnable, IActivatable, IUpdatable
    {
        /// <summary>
        /// Raised when the internal state of the entity changes.
        /// Useful for tracking structural or dynamic modifications.
        /// </summary>
        event Action OnStateChanged;

        /// <summary>
        /// The runtime-generated unique identifier for this entity instance.
        /// This value is valid only during runtime and should not be used for persistence or serialization.
        /// </summary>
        int InstanceID { get; }

        /// <summary>
        /// Optional user-defined name of the entity.
        /// Typically used for editor tooling, debugging, or runtime labeling.
        /// </summary>
        string Name { get; set; }
    }
}