using System;

namespace Atomic.Entities
{
    public struct SampleStruct
    {
        public int A, B;

        public override bool Equals(object obj)
        {
            return obj is SampleStruct other && A == other.A && B == other.B;
        }

        public override int GetHashCode() => HashCode.Combine(A, B);
    }
}