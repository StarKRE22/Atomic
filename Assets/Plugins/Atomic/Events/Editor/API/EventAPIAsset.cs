#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Atomic.Events
{
    internal sealed class EventAPIAsset
    {
        private readonly string _filePath;

        public bool IsValid => File.Exists(_filePath);

        public EventAPIAsset(string filePath)
        {
            _filePath = filePath;
        }

        public IEventAPIConfig GetConfiguration()
        {
            var snapshot = new Snapshot();
            if (!File.Exists(_filePath))
                return snapshot;

            bool readImports = false;
            bool readEvents = false;

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
                else if (trimmed.StartsWith("eventBusType:"))
                    snapshot.EventBusType = trimmed["eventBusType:".Length..].Trim();
                else if (trimmed.StartsWith("aggressiveInlining:"))
                    snapshot.AggressiveInlining = trimmed["aggressiveInlining:".Length..].Trim() == "true";

                else if (trimmed.StartsWith("imports:"))
                {
                    readImports = true;
                    readEvents = false;
                }
                else if (trimmed.StartsWith("events:"))
                {
                    readImports = false;
                    readEvents = true;
                }
                else if (trimmed.StartsWith("-"))
                {
                    string item = trimmed[1..].Trim();
                    if (readImports)
                        snapshot._imports.Add(item);

                    if (readEvents && Regex.IsMatch(item, @"^[A-Za-z_][A-Za-z0-9_]*\(\s*(?:[A-Za-z_][A-Za-z0-9_]*\s+[A-Za-z_][A-Za-z0-9_]*(?:,\s*)?)*\)$"))
                    {

                        if (ParseMethodSignature(item, out EventDefinition definition))
                            snapshot._events.Add(definition.name, definition);

                    }
                }
            }

            return snapshot;
        }
        
        private static bool ParseMethodSignature(string input, out EventDefinition definition)
        {
            const string pattern = @"^(?<methodName>[A-Za-z_][A-Za-z0-9_]*)\s*\((?<args>.*)\)$";
            Match match = Regex.Match(input, pattern);

            if (!match.Success)
            {
                definition = default;
                return false;
                // throw new Exception("Invalid method signature format");
            }

            string methodName = match.Groups["methodName"].Value;
            string argsString = match.Groups["args"].Value;


            List<EventDefinition.Arg> args = new();

            if (!string.IsNullOrWhiteSpace(argsString))
            {
                const string argPattern = @"(?<type>[A-Za-z_][A-Za-z0-9_]*)\s+(?<name>[A-Za-z_][A-Za-z0-9_]*)";
                MatchCollection argMatches = Regex.Matches(argsString, argPattern);

                foreach (Match argMatch in argMatches)
                {
                    string type = argMatch.Groups["type"].Value;
                    string name = argMatch.Groups["name"].Value;
                    args.Add(new EventDefinition.Arg(type, name));
                }
            }

            definition = new EventDefinition(methodName, args);
            return true;
        }

        
        private static string RemoveComments(string input)
        {
            return Regex.Replace(input, @"#.*?$", "", RegexOptions.Multiline).Trim();
        }

        //TODO: Добавлять комментарии!
        private sealed class Snapshot : IEventAPIConfig
        {
            public string Namespace { get; set; }
            public string ClassName { get; set; }
            public string Directory { get; set; }

            public string EventBusType { get; set; } = "IEventBus";
            public bool AggressiveInlining { get; set; } = true;

            public readonly HashSet<string> _imports = new();
            public readonly Dictionary<string, EventDefinition> _events = new();

            public IReadOnlyCollection<string> GetImports() => _imports;
            public IReadOnlyCollection<EventDefinition> GetEvents() => _events.Values;
        }
    }
}
#endif