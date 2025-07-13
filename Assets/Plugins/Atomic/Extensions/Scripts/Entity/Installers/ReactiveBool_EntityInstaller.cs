#if ODIN_INSPECTOR
using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine.Scripting.APIUpdating;

namespace Atomic.Extensions
{
    [MovedFrom(true, null, null, "ReactiveBoolEntityInstaller")]
    [Serializable]
    public sealed class ReactiveBool_EntityInstaller : ValueEntityInstaller<ReactiveBool>
    {
    }
}
#endif