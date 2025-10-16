namespace Atomic.Entities
{
    public interface IEntityNameStrategy
    {
        /// <summary>
        /// Converts a string entity name to a unique integer ID.
        /// If the name has already been registered, the existing ID is returned.
        /// Otherwise, a new ID is assigned and stored.
        /// </summary>
        /// <param name="name">The string name to convert to an ID.</param>
        /// <returns>A unique integer identifier corresponding to the provided name.</returns>
        public int NameToId(string name);
        
        /// <summary>
        /// Retrieves the original entity name from a given integer ID.
        /// </summary>
        /// <param name="id">The ID to convert back to a string name.</param>
        /// <returns>
        /// The original string name associated with the given ID,
        /// or a fallback string in the format <c>#Unknown:{id}</c> if the ID was not registered.
        /// </returns>
        public string IdToName(int id);

        /// <summary>
        /// Clears all name-to-ID mappings and resets the ID counter.
        /// Automatically called when entering play mode in the Unity Editor.
        /// </summary>
        public void Clear();
    }
}