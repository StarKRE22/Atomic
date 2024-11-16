using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Atomic.Entities
{
    public sealed class YamlAPIConfiguration : IEntityAPIConfiguration
    {
        public string Namespace { get; }
        public string ClassName { get; }
        public string Directory { get; }

        private readonly HashSet<string> imports = new();
        private readonly HashSet<string> tags = new();
        private readonly Dictionary<string, string> values = new();

        public YamlAPIConfiguration(string filePath)
        {
            bool readImports = false;
            bool readTags = false;
            bool readValues = false;

            Debug.Log($"CREATE YAML {filePath}");
            foreach (string line in File.ReadLines(filePath))
            {
                string trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed)) 
                    continue;

                if (trimmed.StartsWith("namespace:"))
                    this.Namespace = trimmed["namespace:".Length..].Trim();
                else if (trimmed.StartsWith("className:"))
                    this.ClassName = trimmed["className:".Length..].Trim();
                else if (trimmed.StartsWith("directory:"))
                    this.Directory = trimmed["directory:".Length..].Trim();
                
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
                        this.imports.Add(item);

                    if (readTags)
                        this.tags.Add(item);

                    if (readValues)
                    {
                        string[] keyValue = item.Split(new[] {':'}, 2);
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        this.values.Add(key, value);
                    }
                }
            }
        }

        public IEnumerable<string> GetImports()
        {
            return this.imports;
        }

        public IEnumerable<string> GetTags()
        {
            return tags;
        }

        public IDictionary<string, string> GetValues()
        {
            return values;
        }
    }
}