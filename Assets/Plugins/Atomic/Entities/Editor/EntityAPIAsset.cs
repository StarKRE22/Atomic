#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Atomic.Entities
{
    //TODO: Добавлять комментарии!
    internal sealed class EntityAPIAsset
    {
        internal struct Settings
        {
            public string Namespace;
            public string ClassName;
            public string Directory;

            public string EntityType;
            public bool AggressiveInlining;
            public bool UnsafeAccess;

            public HashSet<string> Imports;
            public HashSet<string> Tags;
            public Dictionary<string, string> Values;

            //TODO: Сделать валидацию!
            public void Validate()
            {
                if (this.Namespace == null)
                    throw new ArgumentNullException(nameof(Namespace));

                if (this.ClassName == null)
                    throw new ArgumentNullException(nameof(ClassName));
                
                if (this.Directory == null)
                    throw new ArgumentNullException(nameof(Directory));
                
                if (this.EntityType == null)
                    throw new ArgumentNullException(nameof(EntityType));
            }
        }
        
        private readonly string _filePath;

        public bool IsValid => File.Exists(_filePath);

        public EntityAPIAsset(string filePath)
        {
            _filePath = filePath;
        }

        public Settings GetSettings()
        {
            if (!this.IsValid)
                throw new Exception($"EntityAPIAsset {_filePath} is not valid!");
                    
            Settings settings = new Settings
            {
                Imports = new HashSet<string>(),
                Tags = new HashSet<string>(),
                Values = new Dictionary<string, string>()
            };

            bool readImports = false;
            bool readTags = false;
            bool readValues = false;

            foreach (string line in File.ReadLines(_filePath))
            {
                string trimmed = RemoveComments(line);

                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                if (trimmed.StartsWith("namespace:"))
                    settings.Namespace = trimmed["namespace:".Length..].Trim();
                else if (trimmed.StartsWith("className:"))
                    settings.ClassName = trimmed["className:".Length..].Trim();
                else if (trimmed.StartsWith("directory:"))
                    settings.Directory = trimmed["directory:".Length..].Trim();
                else if (trimmed.StartsWith("entityType:"))
                    settings.EntityType = trimmed["entityType:".Length..].Trim();
                else if (trimmed.StartsWith("aggressiveInlining:"))
                    settings.AggressiveInlining = trimmed["aggressiveInlining:".Length..].Trim() == "true";
                else if (trimmed.StartsWith("unsafeAccess:"))
                    settings.UnsafeAccess = trimmed["unsafeAccess:".Length..].Trim() == "true";

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
                        settings.Imports.Add(item);

                    if (readTags)
                        settings.Tags.Add(item);

                    if (readValues && Regex.IsMatch(item,
                            @"^([a-zA-Z_][a-zA-Z0-9_]*)\s*:\s*((?:[a-zA-Z_][\w]*\.)*[a-zA-Z_][\w]*)(\?|\*|(\[,*\])|(<[^<>]+>))?$"))
                    {
                        string[] keyValue = item.Split(new[] {':'}, 2);
                        string key = keyValue[0].Trim();
                        string value = keyValue[1].Trim();
                        settings.Values.Add(key, value);
                    }
                }
            }

            settings.Validate();
            return settings;
        }

        private static string RemoveComments(string input)
        {
            return Regex.Replace(input, @"#.*?$", "", RegexOptions.Multiline).Trim();
        }
    }
}
#endif