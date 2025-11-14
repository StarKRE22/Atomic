using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityBakerMode
    {
        None = 0,
        Standard = 1,
        Optimized = 2
    }
}