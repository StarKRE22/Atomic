using System;

namespace Atomic.Events
{
    /// <summary>
    /// Marks a static class as an Event API definition for source generation.
    /// The source generator reads static fields of type <c>EventKey&lt;TBus&gt;</c>,
    /// <c>EventKey&lt;TBus, T&gt;</c>, <c>EventKey&lt;TBus, T1, T2&gt;</c>, or
    /// <c>EventKey&lt;TBus, T1, T2, T3&gt;</c> and produces extension methods
    /// for the event bus type declared in each key's first generic argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EventExtensionsAPIAttribute : Attribute
    {
        /// <summary>
        /// When <c>true</c>, all event fields generate
        /// <c>SubscribeUnsafe</c>, <c>UnsubscribeUnsafe</c>, and <c>InvokeUnsafe</c>
        /// calls instead of the safe variants.
        /// Individual fields can override this with <c>[Unsafe]</c>.
        /// </summary>
        public bool Unsafe { get; set; }

        public EventExtensionsAPIAttribute()
        {
        }
    }
}
