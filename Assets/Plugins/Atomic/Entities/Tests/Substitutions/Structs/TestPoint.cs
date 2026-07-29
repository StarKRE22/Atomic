using System;

namespace Atomic.Entities
{
    public struct TestPoint
    {
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            if (obj is TestPoint other)
                return this.X == other.X && this.Y == other.Y;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}