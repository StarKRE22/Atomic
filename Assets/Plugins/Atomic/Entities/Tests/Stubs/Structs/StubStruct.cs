using System;

namespace Atomic.Entities
{
    public struct StubStruct
    {
        public int A, B;

        public override bool Equals(object obj)
        {
            return obj is StubStruct other && A == other.A && B == other.B;
        }

        public override int GetHashCode() => HashCode.Combine(A, B);
    }
}