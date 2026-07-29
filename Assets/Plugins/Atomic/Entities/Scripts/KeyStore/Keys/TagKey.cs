using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a tag key identified by an integer Id.
    /// Used for fast access to values via a name-to-id mapping.
    /// </summary>
    /// <remarks>
    /// Tag names are converted to Ids using <see cref="EntityKeyStore.NameToId(string)"/>.
    /// Reverse conversion is available via <see cref="EntityKeyStore.IdToName(int)"/>.
    /// </remarks>
    public readonly struct TagKey : IEquatable<TagKey>
    {
        /// <summary>
        /// Unique identifier of the tag.
        /// </summary>
        public readonly int Id;

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
        
        public bool Equals(TagKey other) => Id == other.Id;

        public override bool Equals(object obj) => obj is TagKey other && Equals(other);

        public override int GetHashCode() => Id;
    }
}