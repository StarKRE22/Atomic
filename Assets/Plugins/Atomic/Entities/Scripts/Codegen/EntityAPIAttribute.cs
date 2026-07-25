using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Marks a static class as an Entity API definition for source generation.
    /// The source generator reads public static fields and produces extension
    /// methods for the specified entity type. <c>Tag</c> fields become tag
    /// methods (Has/Del/AddTag); all other types become value methods
    /// (Get/TryGet/Add/Has/Del/Set/Ref).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class EntityAPIAttribute : Attribute
    {
        /// <summary>
        /// The entity interface type (e.g., <c>IPlayerContext</c>, <c>IGameEntity</c>)
        /// that generated extension methods will extend.
        /// </summary>
        public Type EntityType { get; }

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

        public EntityAPIAttribute(Type entityType)
        {
            EntityType = entityType ?? throw new ArgumentNullException(nameof(entityType));
        }
    }
}
