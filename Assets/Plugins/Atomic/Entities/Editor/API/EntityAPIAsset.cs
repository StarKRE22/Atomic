#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Atomic.Entities
{
    internal sealed class EntityAPIAsset
    {
        private readonly string _filePath;

        public bool IsValid => File.Exists(_filePath);

        public EntityAPIAsset(string filePath)
        {
            _filePath = filePath;
        }

        public IEntityAPIConfiguration GetConfiguration()
        {
            var snapshot = new Snapshot();
            if (!File.Exists(_filePath))
                return snapshot;

            bool readImports = false;
            bool readTags = false;
            bool readValues = false;

            foreach (string line in File.ReadLines(_filePath))
            {
                string trimmed = RemoveComments(line);

                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                if (trimmed.StartsWith("namespace:"))
                    snapshot.Namespace = trimmed["namespace:".Length..].Trim();
                else if (trimmed.StartsWith("className:"))
                    snapshot.ClassName = trimmed["className:".Length..].Trim();
                else if (trimmed.StartsWith("directory:"))
                    snapshot.Directory = trimmed["directory:".Length..].Trim();
                else if (trimmed.StartsWith("entityType:"))
                    snapshot.EntityType = trimmed["entityType:".Length..].Trim();
                else if (trimmed.StartsWith("aggressiveInlining:"))
                    snapshot.AggressiveInlining = trimmed["aggressiveInlining:".Length..].Trim() == "true";
                else if (trimmed.StartsWith("unsafeAccess:"))
                    snapshot.UnsafeAccess = trimmed["unsafeAccess:".Length..].Trim() == "true";

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

                    if (readValues && Regex.IsMatch(item,
                            @"^([a-zA-Z_][a-zA-Z0-9_]*)\s*:\s*((?:[a-zA-Z_][\w]*\.)*[a-zA-Z_][\w]*)(\?|\*|(\[,*\])|(<[^<>]+>))?$"))
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

        
        private static string RemoveComments(string input)
        {
            return Regex.Replace(input, @"#.*?$", "", RegexOptions.Multiline).Trim();
        }

        //TODO: Добавлять комментарии!
        private sealed class Snapshot : IEntityAPIConfiguration
        {
            public string Namespace { get; set; }
            public string ClassName { get; set; }
            public string Directory { get; set; }

            public string EntityType { get; set; } = "IEntity";
            public bool AggressiveInlining { get; set; } = true;
            public bool UnsafeAccess { get; set; } = true;

            public readonly HashSet<string> _imports = new();
            public readonly HashSet<string> _tags = new();
            public readonly Dictionary<string, string> _values = new();

            public IReadOnlyCollection<string> GetImports() => _imports;
            public IReadOnlyCollection<string> GetTags() => _tags;
            public IDictionary<string, string> GetValues() => _values;
        }
    }
}
#endif