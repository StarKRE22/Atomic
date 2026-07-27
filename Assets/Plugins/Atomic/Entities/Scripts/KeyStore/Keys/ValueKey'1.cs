using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a strongly-typed key for a value bound to a specific entity type.
    /// </summary>
    /// <typeparam name="E">The entity type implementing <see cref="IEntity"/>.</typeparam>
    /// <typeparam name="T">The type of the value associated with this key.</typeparam>
    /// <remarks>
    /// Ensures both entity-level and value-level type safety,
    /// preventing accidental misuse across different entity types.
    /// </remarks>
    public readonly struct ValueKey<E, T> : IEquatable<ValueKey<E, T>> where E : IEntity
    {
        /// <summary>
        /// Internal identifier of the key.
        /// </summary>
        public readonly int Id;

        /// <summary>
        /// Creates a value key from a string name.
        /// </summary>
        /// <param name="name">The name of the value.</param>
        public ValueKey(string name) => Id = EntityKeyStore.NameToId(name);

        /// <summary>
        /// Creates a value key from an existing identifier.
        /// </summary>
        /// <param name="id">The numeric identifier.</param>
        public ValueKey(int id) => this.Id = id;

        /// <summary>
        /// Returns the string representation of the key.
        /// </summary>
        /// <returns>The name associated with this key.</returns>
        public override string ToString() => EntityKeyStore.IdToName(Id);

        /// <summary>
        /// Determines whether the specified key is equal to the current key.
        /// </summary>
        public bool Equals(ValueKey<E, T> other) => Id == other.Id;

        /// <summary>
        /// Determines whether the specified object is equal to the current key.
        /// </summary>
        public override bool Equals(object obj) => obj is ValueKey<E, T> other && Equals(other);

        /// <summary>
        /// Returns the hash code for this key.
        /// </summary>
        public override int GetHashCode() => Id;
    }
}