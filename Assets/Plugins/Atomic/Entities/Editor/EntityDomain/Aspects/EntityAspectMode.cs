using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityAspectMode
    {
        None = 0,
        ScriptableEntityAspect = 1,
        SceneEntityAspect = 2
    }
}