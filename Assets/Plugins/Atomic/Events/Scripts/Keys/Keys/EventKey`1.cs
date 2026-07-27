using System;

namespace Atomic.Events
{
    public readonly struct EventKey<TBus, TArg> : IEquatable<EventKey<TBus, TArg>> where TBus : IEventBus
    {
        public readonly int Id;

        public EventKey(string name) => Id = EventKeyStore.NameToId(name);

        public EventKey(int id) => Id = id;

        public override string ToString() => EventKeyStore.IdToName(Id);

        public bool Equals(EventKey<TBus, TArg> other) => Id == other.Id;

        public override bool Equals(object obj) => obj is EventKey<TBus, TArg> other && Equals(other);

        public override int GetHashCode() => Id;

        public static bool operator ==(EventKey<TBus, TArg> left, EventKey<TBus, TArg> right) => left.Equals(right);

        public static bool operator !=(EventKey<TBus, TArg> left, EventKey<TBus, TArg> right) =>
            !left.Equals(right);
    }
}