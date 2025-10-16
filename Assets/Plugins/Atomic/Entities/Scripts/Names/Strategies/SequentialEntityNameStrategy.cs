using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class SequentialEntityNameStrategy : IEntityNameStrategy
    {
        /// <summary>
        /// Internal dictionary mapping string names to integer IDs.
        /// </summary>
        private readonly Dictionary<string, int> _nameToId = new();

        /// <summary>
        /// Internal dictionary mapping integer IDs back to string names.
        /// </summary>
        private readonly Dictionary<int, string> _idToName = new();
        
        /// <summary>
        /// The next ID to assign to a new name.
        /// </summary>
        private int _nextId = 1;
        
        public int NameToId(string name)
        {
            if (_nameToId.TryGetValue(name, out int id))
                return id;

            id = _nextId++;
            _nameToId[name] = id;
            _idToName[id] = name;
            return id;
        }

        public string IdToName(int id)
        {
            return _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";
        }

        public void Clear()
        {
            _nameToId.Clear();
            _idToName.Clear();
            _nextId = 1;
        }
    }
}