using System;

namespace Atomic.Entities
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class EntityInspectorValueAttribute : Attribute
    {
        public readonly Type entityType;
        public readonly Type valueType;

        public EntityInspectorValueAttribute(Type valueType)
        {
            this.entityType = typeof(IEntity);
            this.valueType = valueType;
        }

        public EntityInspectorValueAttribute(Type entityType, Type valueType)
        {
            this.entityType = entityType;
            this.valueType = valueType;
        }
    }
}