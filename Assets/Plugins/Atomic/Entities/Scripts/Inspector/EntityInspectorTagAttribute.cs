using System;

namespace Atomic.Entities
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class EntityInspectorTagAttribute : Attribute
    {
        public readonly Type entityType;

        public EntityInspectorTagAttribute(Type entityType) => 
            this.entityType = entityType;

        public EntityInspectorTagAttribute() => 
            this.entityType = typeof(IEntity);
    }
}