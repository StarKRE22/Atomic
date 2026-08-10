using System;

namespace Atomic.Events
{
    /// <summary>
    /// Marks an individual event field in a <c>[EventExtensionsAPI]</c> class as unsafe,
    /// causing the source generator to emit <c>SubscribeUnsafe</c>, <c>UnsubscribeUnsafe</c>,
    /// and <c>InvokeUnsafe</c> calls instead of the safe variants.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnsafeAttribute : Attribute
    {
    }
}
