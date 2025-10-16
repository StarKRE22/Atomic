using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a sequential implementation of the <see cref="IEntityNameStrategy"/> interface.
    /// </summary>
    /// <remarks>
    /// This strategy assigns unique integer identifiers to entity names sequentially,
    /// starting from <c>1</c>. Each new, previously unseen name is assigned the next
    /// available ID in ascending order.
    /// <para>
    /// This simple mapping approach is efficient for runtime entity management,
    /// debugging, and editor tools that require name-to-ID conversion.
    /// </para>
    /// </remarks>
    public sealed class SequentialEntityNameStrategy : IEntityNameStrategy
    {
        /// <summary>
        /// Internal dictionary mapping string names to their assigned integer IDs.
        /// </summary>
        private readonly Dictionary<string, int> _nameToId = new();

        /// <summary>
        /// Internal dictionary mapping integer IDs back to their original string names.
        /// </summary>
        private readonly Dictionary<int, string> _idToName = new();
        
        /// <summary>
        /// The next available ID to assign to a new, previously unregistered name.
        /// Starts at <c>1</c>.
        /// </summary>
        private int _nextId = 1;
        
        /// <summary>
        /// Converts a string entity name into a unique integer identifier.
        /// </summary>
        /// <param name="name">The entity name to convert to an ID.</param>
        /// <returns>
        /// The unique integer identifier associated with the specified name.
        /// If the name has already been registered, returns the existing ID;
        /// otherwise, assigns and stores a new sequential ID.
        /// </returns>
        public int NameToId(string name)
        {
            if (_nameToId.TryGetValue(name, out int id))
                return id;

            id = _nextId++;
            _nameToId[name] = id;
            _idToName[id] = name;
            
            return id;
        }

        /// <summary>
        /// Retrieves the original entity name associated with the specified integer ID.
        /// </summary>
        /// <param name="id">The integer identifier to resolve back to its original name.</param>
        /// <returns>
        /// The original entity name corresponding to the given ID,
        /// or a fallback string in the format <c>#Unknown:{id}</c> if the ID was not registered.
        /// </returns>
        public string IdToName(int id)
        {
            return _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";
        }

        /// <summary>
        /// Clears all name-to-ID and ID-to-name mappings and resets the sequential ID counter.
        /// </summary>
        /// <remarks>
        /// After calling this method, the next assigned ID will start again from <c>1</c>.
        /// Typically, invoked when resetting entity systems or entering Play Mode in Unity.
        /// </remarks>
        public void Reset()
        {
            _nameToId.Clear();
            _idToName.Clear();
            _nextId = 1;
        }
    }
}
