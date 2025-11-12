using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityBakerMode
    {
        None = 0,
        SceneEntityBaker = 1,
        SceneEntityBakerOptimized = 2
    }
}