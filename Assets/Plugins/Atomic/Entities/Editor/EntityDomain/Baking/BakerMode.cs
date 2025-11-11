using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum BakerMode
    {
        None = 0,
        SceneEntityBaker = 1,
        SceneEntityBakerOptimized = 2
    }
}