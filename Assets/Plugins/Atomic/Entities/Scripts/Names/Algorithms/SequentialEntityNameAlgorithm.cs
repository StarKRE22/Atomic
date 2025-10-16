namespace Atomic.Entities
{
    /// <summary>
    /// Provides a sequential algorithm for generating unique integer IDs for entity names.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Each call to <see cref="NameToId"/> returns the next integer in sequence, starting from an initial value.
    /// This algorithm is stateful: it maintains the current ID internally.
    /// </para>
    /// <para>
    /// Typically used when deterministic hash-based IDs are not required and a simple sequential mapping suffices.
    /// </para>
    /// </remarks>
    public sealed class SequentialEntityNameAlgorithm : IEntityNameAlgorithm
    {
        private const int INITIAL_ID = 1;

        private int _nextId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SequentialEntityNameAlgorithm"/> class.
        /// </summary>
        /// <param name="nextId">
        /// Optional starting value for the sequence.
        /// Defaults to <c>1</c> if not provided.
        /// </param>
        public SequentialEntityNameAlgorithm(int nextId = INITIAL_ID) => _nextId = nextId;

        /// <summary>
        /// Returns the next sequential integer ID for the given entity name.
        /// </summary>
        /// <param name="name">The entity name to convert. Ignored in this algorithm.</param>
        /// <returns>
        /// A unique integer ID in sequential order.
        /// Each call increments the internal counter by 1.
        /// </returns>
        public int NameToId(string name) => _nextId++;

        /// <summary>
        /// Resets the internal counter back to the initial value.
        /// </summary>
        public void Reset() => _nextId = INITIAL_ID;
    }
}