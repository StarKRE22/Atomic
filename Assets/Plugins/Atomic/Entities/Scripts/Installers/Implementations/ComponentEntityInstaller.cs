#if ODIN_INSPECTOR
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Entities
{
    [MovedFrom(true, null, null, "ComponentEntityInstaller")] 
    [Serializable]
    public sealed class ComponentEntityInstaller : ValueEntityInstaller<Component>
    {
    }
}
#endif