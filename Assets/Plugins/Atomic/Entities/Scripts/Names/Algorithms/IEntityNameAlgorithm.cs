namespace Atomic.Entities
{
    /// <summary>
    /// Defines a deterministic, stateless algorithm for converting string-based entity names into integer identifiers.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This interface represents the core of an entity naming system where string names are converted to numeric IDs.
    /// It is intentionally stateless: the algorithm itself does not store mappings or caching. 
    /// Caching and reverse lookup are typically handled externally, for example in a manager class like <c>EntityNames</c>.
    /// </para>
    /// <para>
    /// Implementations can range from simple sequential counters to hash-based algorithms
    /// such as SHA-256 or FNV-1a. The key requirement is that the algorithm produces a deterministic integer ID
    /// for a given name.
    /// </para>
    /// <para>
    /// The optional <see cref="Reset"/> method allows resetting internal state for stateful algorithms,
    /// such as sequential ID generators. Stateless algorithms may leave <see cref="Reset"/> as a no-op.
    /// </para>
    /// </remarks>
    public interface IEntityNameAlgorithm
    {
        /// <summary>
        /// Converts a string entity name into a deterministic integer identifier.
        /// </summary>
        /// <param name="name">The entity name to convert. Must not be <c>null</c>.</param>
        /// <returns>
        /// A deterministic integer identifier corresponding to the provided entity name.
        /// Implementations must ensure that the same input string always produces the same integer output.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        int NameToId(string name);

        /// <summary>
        /// Resets the internal state of the algorithm.
        /// </summary>
        /// <remarks>
        /// This method is primarily useful for stateful algorithms, such as sequential ID generators.
        /// Stateless algorithms may leave this method as a no-op.
        /// </remarks>
        void Reset();
    }
}