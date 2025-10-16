using System;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a unified interface for converting between string-based entity names and unique integer identifiers.
    /// </summary>
    /// <remarks>
    /// The <see cref="EntityNames"/> class acts as a static facade over an <see cref="IEntityNameStrategy"/> implementation.
    /// <para>
    /// By default, it uses the <see cref="SequentialEntityNameStrategy"/>, which assigns IDs sequentially.
    /// However, you can replace this behavior by supplying a custom strategy,
    /// such as <see cref="HashEntityNameStrategy"/> or <see cref="Fnv1aEntityNameStrategy"/>, using the <see cref="SetStrategy"/> method.
    /// </para>
    /// <para>
    /// This mechanism allows systems to efficiently identify entities by numeric ID at runtime,
    /// while still supporting reverse lookups for debugging or editor visualization.
    /// </para>
    /// </remarks>
    public static class EntityNames
    {
        /// <summary>
        /// The current name-to-ID conversion strategy in use.
        /// Defaults to <see cref="SequentialEntityNameStrategy"/>.
        /// </summary>
        private static IEntityNameStrategy _strategy = new SequentialEntityNameStrategy();

        /// <summary>
        /// Sets the strategy used for mapping entity names to integer identifiers.
        /// </summary>
        /// <param name="strategy">The new name-to-ID strategy to use.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if the provided <paramref name="strategy"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method allows switching between different ID generation approaches at runtime.
        /// Example usage:
        /// <code>
        /// EntityNames.SetStrategy(new HashEntityNameStrategy());
        /// EntityNames.SetStrategy(new Fnv1aEntityNameStrategy());
        /// </code>
        /// </remarks>
        public static void SetStrategy(IEntityNameStrategy strategy) =>
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));

        /// <summary>
        /// Converts a string entity name into a unique integer identifier.
        /// </summary>
        /// <param name="name">The string name to convert to an ID.</param>
        /// <returns>
        /// A unique integer identifier corresponding to the provided name.
        /// The exact behavior depends on the active <see cref="IEntityNameStrategy"/>:
        /// <list type="bullet">
        /// <item><see cref="SequentialEntityNameStrategy"/> assigns sequential IDs.</item>
        /// <item><see cref="HashEntityNameStrategy"/> or <see cref="Fnv1aEntityNameStrategy"/> generate deterministic hash-based IDs.</item>
        /// </list>
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(string name) => _strategy.NameToId(name);

        /// <summary>
        /// Retrieves the original entity name associated with a specific integer identifier.
        /// </summary>
        /// <param name="id">The integer ID to convert back into a string name.</param>
        /// <returns>
        /// The original entity name if it was registered,
        /// or a fallback string in the format <c>#Unknown:{id}</c> if no name is associated with this ID.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(int id) => _strategy.IdToName(id);

        /// <summary>
        /// Clears all mappings and resets the internal state of the active strategy.
        /// </summary>
        /// <remarks>
        /// Typically invoked automatically when entering Play Mode in the Unity Editor
        /// to ensure a clean registry of entity names before runtime initialization.
        /// </remarks>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void Reset() => _strategy.Reset();
    }
}