using System;

namespace Atomic.Entities
{
    public struct TestStruct
    {
        public int A, B;

        public override bool Equals(object obj) => 
            obj is TestStruct other && A == other.A && B == other.B;

        public override int GetHashCode() => HashCode.Combine(A, B);
    }
}