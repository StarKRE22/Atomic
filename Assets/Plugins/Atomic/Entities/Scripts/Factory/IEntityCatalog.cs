using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// A string-keyed specialization of <see cref="IEntityCatalog{TKey,E}"/> for generic <see cref="IEntity"/> factories.
    /// </summary>
    /// <remarks>
    /// Useful as a shorthand for the most common use case: mapping entity factories by name.
    /// </remarks>
    public interface IEntityCatalog : IEntityCatalog<string, IEntity>
    {
    }

    /// <summary>
    /// Represents a read-only catalog of entity factories, indexed by a key of type <typeparamref name="TKey"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key used to identify factories (e.g., string, enum).</typeparam>
    /// <typeparam name="E">The type of entity each factory creates.</typeparam>
    /// <remarks>
    /// This interface extends <see cref="IReadOnlyDictionary{TKey, TValue}"/> where each value is an <see cref="IEntityFactory{E}"/>.
    /// It provides access to entity creation logic in a structured and lookup-efficient manner.
    /// </remarks>
    public interface IEntityCatalog<TKey, E> : IReadOnlyDictionary<TKey, IEntityFactory<E>> where E : IEntity
    {
    }
}