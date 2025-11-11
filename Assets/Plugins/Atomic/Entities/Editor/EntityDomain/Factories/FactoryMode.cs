using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum FactoryMode
    {
        None = 0,
        ScriptableEntityFactory = 1,
        SceneEntityFactory = 2,
    }
}