using System;

namespace Atomic.Entities
{
    [Flags]
    internal enum EntityInstallerMode
    {
        None = 0,
        ScriptableEntityInstaller = 1,
        SceneEntityInstaller = 2
    }
}