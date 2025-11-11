using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum AspectMode
    {
        None = 0,
        ScriptableEntityAspect = 1,
        SceneEntityAspect = 2
    }
}