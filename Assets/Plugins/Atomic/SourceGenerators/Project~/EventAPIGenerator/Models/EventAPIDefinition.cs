using System;
using System.Collections.Generic;

namespace EventAPIGenerator.Models
{
    /// <summary>
    /// Describes a single class decorated with <c>[EventExtensionsAPI]</c>,
    /// including all event key fields to be generated.
    /// Implements <see cref="IEquatable{T}"/> for incremental caching.
    /// </summary>
    public readonly struct EventAPIDefinition : IEquatable<EventAPIDefinition>
    {
        /// <summary>Full namespace of the annotated class.</summary>
        public string Namespace { get; }

        /// <summary>Name of the annotated class.</summary>
        public string ClassName { get; }

        /// <summary>Class-level unsafe flag.</summary>
        public bool Unsafe { get; }

        /// <summary>Event fields parsed from the class.</summary>
        public IReadOnlyList<EventField> Events { get; }

        public EventAPIDefinition(string ns, string className, bool unsafeFlag, IReadOnlyList<EventField> events)
        {
            Namespace = ns;
            ClassName = className;
            Unsafe = unsafeFlag;
            Events = events;
        }

        public bool Equals(EventAPIDefinition other) =>
            Namespace == other.Namespace &&
            ClassName == other.ClassName &&
            Unsafe == other.Unsafe &&
            SequenceEqual(Events, other.Events);

        public override bool Equals(object obj) =>
            obj is EventAPIDefinition other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Namespace?.GetHashCode() ?? 0);
            hash = hash * 31 + (ClassName?.GetHashCode() ?? 0);
            hash = hash * 31 + Unsafe.GetHashCode();
            foreach (var e in Events)
                hash = hash * 31 + e.GetHashCode();
            return hash;
        }

        private static bool SequenceEqual(IReadOnlyList<EventField> a, IReadOnlyList<EventField> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
                if (!a[i].Equals(b[i])) return false;
            return true;
        }
    }
}
