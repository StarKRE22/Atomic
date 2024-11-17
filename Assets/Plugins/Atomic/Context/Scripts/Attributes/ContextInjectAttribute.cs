using System;
using JetBrains.Annotations;

namespace Atomic.Contexts
{
    [MeansImplicitUse]
    [AttributeUsage(
        AttributeTargets.Field |
        AttributeTargets.Property |
        AttributeTargets.Parameter |
        AttributeTargets.Method
    )]
    public sealed class ContextInjectAttribute : Attribute
    {
        public readonly int key;

        public ContextInjectAttribute()
        {
        }

        public ContextInjectAttribute(int key = default)
        {
            this.key = key;
        }
        
        public ContextInjectAttribute(string key = default)
        {
            this.key = ContextUtils.NameToId(key);
        }
    }
}