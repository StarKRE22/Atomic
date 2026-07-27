using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marks an individual value field in a <c>[GenerateEntityExtensionsAPI]</c> class as unsafe,
    /// causing the source generator to emit <c>GetValueUnsafe&lt;T&gt;</c> and
    /// <c>RefXxx()</c> methods instead of <c>GetValue&lt;T&gt;</c>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public sealed class UnsafeAttribute : Attribute
    {
    }
}
