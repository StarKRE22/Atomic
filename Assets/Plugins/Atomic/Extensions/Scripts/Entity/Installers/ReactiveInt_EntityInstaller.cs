#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "ReactiveIntEntityInstaller")]
    [Serializable]
    public sealed class ReactiveInt_EntityInstaller : ValueEntityInstaller<ReactiveInt>
    {
    }
}
#endif