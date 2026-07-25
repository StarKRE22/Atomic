using System;

namespace Atomic.Events
{
    public readonly struct EventKey<TBus, TArg1, TArg2> : IEquatable<EventKey<TBus, TArg1, TArg2>>
        where TBus : IEventBus
    {
        internal readonly int Id;

        public EventKey(string name) => Id = EventKeyStore.NameToId(name);

        public EventKey(int id) => Id = id;

        public override string ToString() => EventKeyStore.IdToName(Id);

        public bool Equals(EventKey<TBus, TArg1, TArg2> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is EventKey<TBus, TArg1, TArg2> other && Equals(other);

        public override int GetHashCode() => Id;

        public static bool operator ==(EventKey<TBus, TArg1, TArg2> left, EventKey<TBus, TArg1, TArg2> right) =>
            left.Equals(right);

        public static bool operator !=(EventKey<TBus, TArg1, TArg2> left, EventKey<TBus, TArg1, TArg2> right) =>
            !left.Equals(right);
    }
}