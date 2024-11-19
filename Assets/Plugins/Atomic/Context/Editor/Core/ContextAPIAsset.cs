#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Atomic.Contexts
{
    internal sealed class ContextAPIAsset
    {
        private readonly string _filePath;

        public bool IsValid => File.Exists(_filePath);

        public ContextAPIAsset(string filePath)
        {
            _filePath = filePath;
        }

        public IContextAPIConfiguration GetConfiguration()
        {
            var snapshot = new Snapshot();
            if (!File.Exists(_filePath))
                return snapshot;

            bool readImports = false;
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
                else if (trimmed.StartsWith("contextType:"))
                    snapshot.ContextType = trimmed["contextType:".Length..].Trim();
                else if (trimmed.StartsWith("aggressiveInlining:"))
                    snapshot.AggressiveInlining = trimmed["aggressiveInlining:".Length..].Trim() == "true";

                else if (trimmed.StartsWith("imports:"))
                {
                    readImports = true;
                    readValues = false;
                }
                else if (trimmed.StartsWith("values:"))
                {
                    readImports = false;
                    readValues = true;
                }
                else if (trimmed.StartsWith("-"))
                {
                    string item = trimmed[1..].Trim();
                    if (readImports)
                        snapshot.imports.Add(item);

                    if (readValues && Regex.IsMatch(item,
                            @"^([a-zA-Z_][a-zA-Z0-9_]*)\s*:\s*((?:[a-zA-Z_][\w]*\.)*[a-zA-Z_][\w]*)(\?|\*|(\[,*\])|(<[^<>]+>))?$"))
                    {
                        string[] keyValue = item.Split(new[] {':'}, 2);
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        snapshot.values.Add(key, value);
                    }
                }
            }

            return snapshot;
        }

        private sealed class Snapshot : IContextAPIConfiguration
        {
            public string Namespace { get; set; }
            public string ClassName { get; set; }
            public string Directory { get; set; }
            
            public string ContextType { get; set; } = "IContext";
            public bool AggressiveInlining { get; set; } = true;

            public readonly HashSet<string> imports = new();
            public readonly Dictionary<string, string> values = new();

            public IReadOnlyCollection<string> GetImports() => imports;
            public IDictionary<string, string> GetValues() => values;
        }
    }
}

#endif