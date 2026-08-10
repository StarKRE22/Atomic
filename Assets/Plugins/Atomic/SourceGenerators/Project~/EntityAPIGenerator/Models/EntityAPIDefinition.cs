using System;
using System.Collections.Generic;

namespace EntityAPIGenerator.Models
{
    /// <summary>
    /// Describes a single class decorated with <c>[EntityExtensionsAPI]</c>,
    /// including all value and tag fields to be generated.
    /// Implements <see cref="IEquatable{T}"/> for incremental caching.
    /// </summary>
    public readonly struct EntityAPIDefinition : IEquatable<EntityAPIDefinition>
    {
        /// <summary>Full namespace of the annotated class.</summary>
        public string Namespace { get; }

        /// <summary>Name of the annotated class.</summary>
        public string ClassName { get; }

        /// <summary>
        /// Class-level unsafe flag. When <c>true</c>, all value fields
        /// emit <c>GetValueUnsafe&lt;T&gt;</c> / <c>RefXxx()</c> unless
        /// overridden by a per-field <c>[Unsafe]</c>.
        /// </summary>
        public bool Unsafe { get; }

        /// <summary>
        /// Class-level inlining flag. When <c>true</c>, generated methods are
        /// annotated with <c>[MethodImpl(MethodImplOptions.AggressiveInlining)]</c>.
        /// </summary>
        public bool AggressiveInlining { get; }

        /// <summary>Value fields (from <c>ValueKey&lt;E,T&gt;</c> declarations).</summary>
        public IReadOnlyList<ValueField> Values { get; }

        /// <summary>Tag fields (from <c>TagKey&lt;E&gt;</c> declarations).</summary>
        public IReadOnlyList<TagField> Tags { get; }

        public EntityAPIDefinition(
            string ns,
            string className,
            bool unsafeFlag,
            bool aggressiveInlining,
            IReadOnlyList<ValueField> values,
            IReadOnlyList<TagField> tags)
        {
            Namespace = ns;
            ClassName = className;
            Unsafe = unsafeFlag;
            AggressiveInlining = aggressiveInlining;
            Values = values;
            Tags = tags;
        }

        public bool Equals(EntityAPIDefinition other) =>
            Namespace == other.Namespace &&
            ClassName == other.ClassName &&
            Unsafe == other.Unsafe &&
            AggressiveInlining == other.AggressiveInlining &&
            SequenceEqual(Values, other.Values) &&
            SequenceEqual(Tags, other.Tags);

        public override bool Equals(object obj) =>
            obj is EntityAPIDefinition other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Namespace?.GetHashCode() ?? 0);
            hash = hash * 31 + (ClassName?.GetHashCode() ?? 0);
            hash = hash * 31 + Unsafe.GetHashCode();
            hash = hash * 31 + AggressiveInlining.GetHashCode();
            foreach (var v in Values) hash = hash * 31 + v.GetHashCode();
            foreach (var t in Tags) hash = hash * 31 + t.GetHashCode();
            return hash;
        }

        private static bool SequenceEqual<T>(IReadOnlyList<T> a, IReadOnlyList<T> b)
            where T : IEquatable<T>
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
                if (!a[i].Equals(b[i])) return false;
            return true;
        }
    }
}
