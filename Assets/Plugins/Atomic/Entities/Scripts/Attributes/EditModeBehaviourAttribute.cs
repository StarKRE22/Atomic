using System;

namespace Atomic.Entities
{
    /**
     * Used only for Entity Behaviors. Invokes callbacks Init(), Enable(), Disable(), Dispose() in editor mode
     */
    
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class EditModeBehaviourAttribute : Attribute
    {
    }
}