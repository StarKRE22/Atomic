using System;
using System.Collections.Generic;

namespace EventAPIGenerator.Models
{
    /// <summary>
    /// Represents an <c>EventKey&lt;TBus&gt;</c> or <c>EventKey&lt;TBus, T...&gt;</c>
    /// static field parsed from an <c>[EventExtensionsAPI]</c> class.
    /// </summary>
    public readonly struct EventField : IEquatable<EventField>
    {
        /// <summary>Field name, e.g. <c>PlayerTurnStarted</c>.</summary>
        public string Name { get; }

        /// <summary>Event bus type name, e.g. <c>Atomic.Events.IEventBus</c>.</summary>
        public string BusTypeName { get; }

        /// <summary>Event argument type names, e.g. <c>IGameEntity</c>.</summary>
        public IReadOnlyList<string> ArgTypeNames { get; }

        /// <summary>
        /// Whether this event should generate unsafe subscribe/unsubscribe/invoke calls.
        /// </summary>
        public bool IsUnsafe { get; }

        public EventField(string name, string busTypeName, IReadOnlyList<string> argTypeNames, bool isUnsafe = false)
        {
            Name = name;
            BusTypeName = busTypeName;
            ArgTypeNames = argTypeNames;
            IsUnsafe = isUnsafe;
        }

        public bool Equals(EventField other) =>
            Name == other.Name &&
            BusTypeName == other.BusTypeName &&
            IsUnsafe == other.IsUnsafe &&
            SequenceEqual(ArgTypeNames, other.ArgTypeNames);

        public override bool Equals(object obj) =>
            obj is EventField other && Equals(other);

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Name?.GetHashCode() ?? 0);
            hash = hash * 31 + (BusTypeName?.GetHashCode() ?? 0);
            hash = hash * 31 + IsUnsafe.GetHashCode();
            foreach (var arg in ArgTypeNames)
                hash = hash * 31 + (arg?.GetHashCode() ?? 0);
            return hash;
        }

        private static bool SequenceEqual(IReadOnlyList<string> a, IReadOnlyList<string> b)
        {
            if (a.Count != b.Count) return false;
            for (int i = 0; i < a.Count; i++)
                if (a[i] != b[i]) return false;
            return true;
        }
    }
}
