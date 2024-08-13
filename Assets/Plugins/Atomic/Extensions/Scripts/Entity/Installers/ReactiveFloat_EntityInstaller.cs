#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "ReactiveFloatEntityInstaller")]
    [Serializable]
    public sealed class ReactiveFloat_EntityInstaller : ValueEntityInstaller<ReactiveFloat>
    {
    }
}
#endif