using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marker type for tag fields in an <c>[EntityAPI]</c> class.
    /// The source generator recognizes <c>Tag</c> fields and produces
    /// <c>HasXxxTag</c>, <c>AddXxxTag</c>, <c>DelXxxTag</c> extension methods.
    /// </summary>
    public readonly struct Tag : IEquatable<Tag>
    {
        public bool Equals(Tag other) => true;

        public override bool Equals(object obj) =>
            obj is Tag other && Equals(other);

        public override int GetHashCode() => 0;

        public static bool operator ==(Tag left, Tag right) => true;
        public static bool operator !=(Tag left, Tag right) => false;
    }
}
