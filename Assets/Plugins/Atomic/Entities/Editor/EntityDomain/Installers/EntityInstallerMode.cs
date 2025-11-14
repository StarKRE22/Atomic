using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityInstallerMode
    {
        None = 0,
        IEntityInstaller = 1,
        ScriptableEntityInstaller = 2,
        SceneEntityInstaller = 4
    }
}