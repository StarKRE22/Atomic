using System;

namespace Atomic.Entities
{
    public struct PointStub
    {
        public int X;
        public int Y;

        public override bool Equals(object obj)
        {
            if (obj is PointStub other)
                return this.X == other.X && this.Y == other.Y;
            return false;
        }

        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}