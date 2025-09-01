using System;

namespace Atomic.Entities
{
    
    /// <summary>
    /// Represents the fundamental interface of entity in the framework.
    /// It follows the Entity-State-Behaviour pattern, serving as a container for data (values), identity (tags), and modular logic (behaviours).
    /// </summary>
    /// <remarks>
    /// Key Features:
    /// <list type="bullet">
    ///   <item>State Management<description>A key-value data store for dynamic state</description></item>
    ///   <item>Tag System<description>Lightweight categorization and filtering</description></item>
    ///   <item>Behaviour Composition<description> Attach/detach modular logic at runtime of <see cref="IEntityBehaviour"/> components</description></item>
    ///   <item>Lifecycle Control<description>Built-in init, enable, update, disable and disposal phases</description></item>
    ///   <item>Event-Driven<description>State change notifications for reactive programming</description></item>
    ///   <item>Unique Identity<description>Runtime instance ID for entity tracking</description></item>
    /// </list>
    /// </remarks>
    public partial interface IEntity : IInitSource, IEnableSource, IUpdateSource
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