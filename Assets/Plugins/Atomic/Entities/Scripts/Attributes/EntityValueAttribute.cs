#if ODIN_INSPECTOR
using System;

namespace Atomic.Entities
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class EntityValueAttribute : Attribute
    {
    }
}
#endif