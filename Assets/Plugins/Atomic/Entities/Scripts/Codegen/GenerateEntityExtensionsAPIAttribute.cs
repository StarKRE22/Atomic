using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marks a static class as an Entity API definition for source generation.
    /// The source generator reads static fields of type <c>ValueKey&lt;,&gt;</c>
    /// or <c>TagKey&lt;&gt;</c> and produces extension methods for the entity type
    /// declared in each key's first generic argument.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GenerateEntityExtensionsAPIAttribute : Attribute
    {
        /// <summary>
        /// When <c>true</c>, all value fields generate
        /// <c>GetValueUnsafe&lt;T&gt;</c> and <c>RefXxx()</c> methods
        /// instead of <c>GetValue&lt;T&gt;</c>.
        /// Individual fields can override this with <c>[Unsafe]</c>.
        /// </summary>
        public bool Unsafe { get; set; }

        /// <summary>
        /// When <c>true</c> (default), generated extension methods are annotated
        /// with <c>[MethodImpl(MethodImplOptions.AggressiveInlining)]</c>.
        /// Set to <c>false</c> to omit the attribute (e.g. for debugging).
        /// </summary>
        public bool AggressiveInlining { get; set; } = true;

        public GenerateEntityExtensionsAPIAttribute()
        {
        }
    }
}