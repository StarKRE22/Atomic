using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marks a class as a domain behaviour definition for source generation.
    /// The source generator reads the entity type passed to the constructor
    /// and generates strongly-typed behaviour interfaces for that entity type,
    /// one for each lifecycle variant in <see cref="Atomic.Entities"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EntityDomainBehavioursAttribute : Attribute
    {
        /// <summary>
        /// The entity type (interface or concrete class) for which domain behaviour
        /// interfaces will be generated.
        /// </summary>
        public Type EntityType { get; }

        public EntityDomainBehavioursAttribute(Type entityType)
        {
            this.EntityType = entityType;
        }
    }
}
