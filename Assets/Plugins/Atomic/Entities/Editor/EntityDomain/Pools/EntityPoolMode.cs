using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityPoolMode
    {
        None = 0,
        SceneEntityPool = 1,
        PrefabEntityPool = 2
    }
}