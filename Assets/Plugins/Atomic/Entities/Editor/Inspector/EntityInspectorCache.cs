#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Atomic.Entities
{
    [InitializeOnLoad]
    internal static class EntityInspectorCache
    {
        private readonly struct ValueKey : IEquatable<ValueKey>
        {
            public readonly Type Entity;
            public readonly Type Value;

            public ValueKey(Type entity, Type value)
            {
                Entity = entity;
                Value = value;
            }

            public bool Equals(ValueKey other) =>
                Entity == other.Entity && Value == other.Value;

            public override bool Equals(object obj) =>
                obj is ValueKey other && Equals(other);

            public override int GetHashCode() =>
                HashCode.Combine(Entity, Value);
        }

        // =========================
        // RAW CACHE
        // =========================

        private static readonly Dictionary<ValueKey, List<string>> _valueCache = new();
        private static readonly Dictionary<Type, List<string>> _tagCache = new();

        // =========================
        // RESOLVED CACHE (🔥 главное ускорение)
        // =========================

        private static readonly Dictionary<ValueKey, string[]> _resolvedValueCache = new();
        private static readonly Dictionary<Type, string[]> _resolvedTagCache = new();

        static EntityInspectorCache()
        {
            BuildCache();
        }

        // =========================
        // PUBLIC API
        // =========================

        public static IList<string> GetValueKeys(Type entityType, Type valueType)
        {
            entityType ??= typeof(IEntity);
            valueType ??= typeof(object);

            var request = new ValueKey(entityType, valueType);

            if (_resolvedValueCache.TryGetValue(request, out var cached))
                return cached;

            var result = new HashSet<string>();

            foreach (var kvp in _valueCache)
            {
                var key = kvp.Key;

                if (Matches(key.Entity, entityType) &&
                    Matches(key.Value, valueType))
                {
                    foreach (var name in kvp.Value)
                        result.Add(name);
                }
            }

            var final = ToSortedArray(result);
            _resolvedValueCache[request] = final;

            return final;
        }

        public static IList<string> GetTagKeys(Type entityType)
        {
            entityType ??= typeof(IEntity);

            if (_resolvedTagCache.TryGetValue(entityType, out var cached))
                return cached;

            var result = new HashSet<string>();

            foreach (var kvp in _tagCache)
            {
                if (kvp.Key.IsAssignableFrom(entityType))
                {
                    foreach (var name in kvp.Value)
                        result.Add(name);
                }
            }

            var final = ToSortedArray(result);
            _resolvedTagCache[entityType] = final;

            return final;
        }

        // =========================
        // MATCHING (оптимизирован)
        // =========================

        private static bool Matches(Type stored, Type requested)
        {
            if (stored == requested || stored.IsAssignableFrom(requested))
                return true;

            if (stored.IsGenericTypeDefinition)
            {
                if (requested.IsGenericType &&
                    requested.GetGenericTypeDefinition() == stored)
                    return true;

                var interfaces = requested.GetInterfaces();
                for (int i = 0; i < interfaces.Length; i++)
                {
                    var iFace = interfaces[i];
                    if (iFace.IsGenericType &&
                        iFace.GetGenericTypeDefinition() == stored)
                        return true;
                }
            }

            return false;
        }

        // =========================
        // BUILD
        // =========================

        private static void BuildCache()
        {
            _valueCache.Clear();
            _tagCache.Clear();
            _resolvedValueCache.Clear();
            _resolvedTagCache.Clear();

            var apiTypes = TypeCache.GetTypesWithAttribute<EntityInspectorAPIAttribute>();

            foreach (var type in apiTypes)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static);

                foreach (var field in fields)
                    AddTagOrValue(field);
            }
        }

        private static void AddTagOrValue(FieldInfo field)
        {
            var fieldType = field.FieldType;

            if (!fieldType.IsGenericType)
            {
                if (fieldType == typeof(TagKey))
                    AddTag(typeof(IEntity), field.Name);

                return;
            }

            var def = fieldType.GetGenericTypeDefinition();

            if (def == typeof(TagKey<>))
            {
                AddTag(fieldType.GetGenericArguments()[0], field.Name);
                return;
            }

            if (def == typeof(ValueKey<,>))
            {
                var args = fieldType.GetGenericArguments();
                AddValue(args[0], args[1], field.Name);
                return;
            }

            if (def == typeof(ValueKey<>))
            {
                AddValue(typeof(IEntity), fieldType.GetGenericArguments()[0], field.Name);
            }
        }

        // =========================
        // ADD
        // =========================

        private static void AddValue(Type entityType, Type valueType, string name)
        {
            var key = new ValueKey(entityType, valueType);

            if (!_valueCache.TryGetValue(key, out var list))
                _valueCache[key] = list = new List<string>(4);

            list.Add(name);
        }

        private static void AddTag(Type entityType, string name)
        {
            if (!_tagCache.TryGetValue(entityType, out var list))
                _tagCache[entityType] = list = new List<string>(4);

            list.Add(name);
        }

        // =========================
        // UTILS
        // =========================

        private static string[] ToSortedArray(HashSet<string> set)
        {
            var array = new string[set.Count];
            set.CopyTo(array);
            Array.Sort(array);
            return array;
        }
    }
}
#endif