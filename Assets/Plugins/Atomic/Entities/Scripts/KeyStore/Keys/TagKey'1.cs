using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Type-safe tag key bound to a specific entity type.
    /// </summary>
    /// <typeparam name="E">Entity type implementing <see cref="IEntity"/>.</typeparam>
    /// <remarks>
    /// Prevents accidental mixing of keys between different entity types.
    /// </remarks>
    public readonly struct TagKey<E> : IEquatable<TagKey<E>> where E : IEntity
    {
        /// <summary>
        /// Unique identifier of the tag.
        /// </summary>
        internal readonly int Id;

        /// <summary>
        /// Creates a tag key from a string name.
        /// </summary>
        /// <param name="name">Tag name.</param>
        public TagKey(string name) => Id = EntityKeyStore.NameToId(name);

        /// <summary>
        /// Creates a tag key from an existing identifier.
        /// </summary>
        /// <param name="id">Numeric tag identifier.</param>
        public TagKey(int id) => this.Id = id;

        /// <summary>
        /// Returns the string representation of the tag.
        /// </summary>
        /// <returns>Tag name.</returns>
        public override string ToString() => EntityKeyStore.IdToName(Id);
        
        public bool Equals(TagKey<E> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is TagKey<E> other && Equals(other);

        public override int GetHashCode() => Id;
    }
}