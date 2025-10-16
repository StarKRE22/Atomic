// using System.Collections.Generic;
// using System.IO;
// using Unity.Plastic.Newtonsoft.Json;
//
// namespace Atomic.Entities
// {
//     public class PersistentEntityNameStrategy : IEntityNameStrategy
//     {
//         private readonly string _filePath;
//         private readonly Dictionary<string, int> _nameToId = new();
//         private readonly Dictionary<int, string> _idToName = new();
//         private int _nextId = 1;
//
//         public PersistentEntityNameStrategy(string filePath)
//         {
//             _filePath = filePath;
//             Load();
//         }
//
//         public int NameToId(string name)
//         {
//             if (_nameToId.TryGetValue(name, out var id))
//                 return id;
//
//             id = _nextId++;
//             _nameToId[name] = id;
//             _idToName[id] = name;
//             Save();
//             return id;
//         }
//
//         public string IdToName(int id) => _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";
//
//         public void Clear()
//         {
//             _nameToId.Clear();
//             _idToName.Clear();
//             _nextId = 1;
//             Save();
//         }
//
//         private void Save()
//         {
//             var data = new {Names = _nameToId, NextId = _nextId};
//             var json = JsonSerializer.Serialize(data, new JsonSerializerOptions {WriteIndented = true});
//             File.WriteAllText(_filePath, json);
//         }
//
//         private void Load()
//         {
//             if (!File.Exists(_filePath)) return;
//
//             var json = File.ReadAllText(_filePath);
//             var data = JsonSerializer.Deserialize<JsonData>(json);
//             if (data == null) return;
//
//             foreach (var kv in data.Names)
//             {
//                 _nameToId[kv.Key] = kv.Value;
//                 _idToName[kv.Value] = kv.Key;
//             }
//
//             _nextId = data.NextId;
//         }
//
//         private class JsonData
//         {
//             public Dictionary<string, int> Names { get; set; } = new();
//             public int NextId { get; set; } = 1;
//         }
//     }
// }