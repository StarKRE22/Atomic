using System.Collections.Generic;
using System.IO;

namespace Atomic.Entities
{
    public sealed class EntityAPIAsset
    {
        private readonly string _filePath;

        public bool IsValid => File.Exists(_filePath);

        public EntityAPIAsset(string filePath)
        {
            _filePath = filePath;
        }

        public IEntityAPIConfiguration GetConfiguration()
        {
            var snapshot = new Configuration();
            if (!File.Exists(_filePath))
                return snapshot;
            
            bool readImports = false;
            bool readTags = false;
            bool readValues = false;

            foreach (string line in File.ReadLines(_filePath))
            {
                string trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                if (trimmed.StartsWith("namespace:"))
                    snapshot.Namespace = trimmed["namespace:".Length..].Trim();
                else if (trimmed.StartsWith("className:"))
                    snapshot.ClassName = trimmed["className:".Length..].Trim();
                else if (trimmed.StartsWith("directory:"))
                    snapshot.Directory = trimmed["directory:".Length..].Trim();

                else if (trimmed.StartsWith("imports:"))
                {
                    readImports = true;
                    readTags = false;
                    readValues = false;
                }
                else if (trimmed.StartsWith("tags:"))
                {
                    readImports = false;
                    readTags = true;
                    readValues = false;
                }
                else if (trimmed.StartsWith("values:"))
                {
                    readImports = false;
                    readTags = false;
                    readValues = true;
                }
                else if (trimmed.StartsWith("-"))
                {
                    string item = trimmed[1..].Trim();
                    if (readImports)
                        snapshot._imports.Add(item);

                    if (readTags)
                        snapshot._tags.Add(item);

                    if (readValues)
                    {
                        string[] keyValue = item.Split(new[] {':'}, 2);
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        snapshot._values.Add(key, value);
                    }
                }
            }

            return snapshot;
        }
        
        private sealed class Configuration : IEntityAPIConfiguration
        {
            public string Namespace { get; set; }
            public string ClassName { get; set; }
            public string Directory { get; set; }

            public readonly HashSet<string> _imports = new();
            public readonly HashSet<string> _tags = new();
            public readonly Dictionary<string, string> _values = new();

            public IEnumerable<string> GetImports()
            {
                return _imports;
            }

            public IEnumerable<string> GetTags()
            {
                return _tags;
            }

            public IDictionary<string, string> GetValues()
            {
                return _values;
            }
        }
    }
}