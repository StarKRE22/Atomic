using System;
using JetBrains.Annotations;

namespace Atomic.Contexts
{
    [MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class ConstructAttribute : Attribute
    {
    }
}