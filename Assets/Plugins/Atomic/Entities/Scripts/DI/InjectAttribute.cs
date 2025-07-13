using System;
using JetBrains.Annotations;

namespace Atomic.Entities
{
    [MeansImplicitUse]
    [AttributeUsage(
        AttributeTargets.Field |
        AttributeTargets.Property |
        AttributeTargets.Parameter |
        AttributeTargets.Method
    )]
    public sealed class InjectAttribute : Attribute
    {
        public readonly int key;

        public InjectAttribute()
        {
        }

        public InjectAttribute(int key = default)
        {
            this.key = key;
        }
        
        public InjectAttribute(string key = default)
        {
            this.key = EntityUtils.NameToId(key);
        }
    }
}