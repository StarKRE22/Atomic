using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    /// <summary>
    /// A set of lightweight extension helpers for applying and discarding <see cref="IEntityAspect"/> instances
    /// against entities. These methods forward the call to the provided aspect and are marked for aggressive
    /// inlining to minimize call overhead.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Applies the specified <see cref="IEntityAspect"/> to the given entity.
        /// This is an extension convenience method that forwards to <see cref="IEntityAspect.Apply(IEntity)"/>.
        /// </summary>
        /// <param name="e">The entity to which the aspect will be applied.</param>
        /// <param name="entityAspect">The aspect instance that will be applied to the entity.</param>
        /// <remarks>
        /// The method is annotated with <see cref="MethodImplOptions.AggressiveInlining"/> to encourage
        /// the JIT to inline the call and reduce invocation overhead.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Apply(this IEntity e, IEntityAspect entityAspect) => entityAspect.Apply(e);

        /// <summary>
        /// Applies the specified generic <see cref="IEntityAspect{E}"/> to the given entity of type <typeparamref name="E"/>.
        /// This is a type-safe extension method that forwards to <see cref="IEntityAspect{E}.Apply(E)"/>.
        /// </summary>
        /// <typeparam name="E">The concrete entity type that implements <see cref="IEntity"/>.</typeparam>
        /// <param name="e">The entity to which the aspect will be applied.</param>
        /// <param name="entityAspect">The generic aspect instance that will be applied to the entity.</param>
        /// <remarks>
        /// Use this overload when you have aspects that operate on a concrete entity type for better type safety
        /// and to avoid runtime casts.
        /// The method is annotated with <see cref="MethodImplOptions.AggressiveInlining"/> to encourage inlining.
        /// </remarks>
        /// <example>
        /// <code language="csharp">
        /// MyEntity entity = ...; // MyEntity : IEntity
        /// IEntityAspect&lt;MyEntity&gt; aspect = new MyTypedAspect(...);
        /// entity.Apply(aspect);
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Apply<E>(this E e, IEntityAspect<E> entityAspect) where E : IEntity => entityAspect.Apply(e);

        /// <summary>
        /// Discards the specified <see cref="IEntityAspect"/> from the given entity.
        /// This is an extension convenience method that forwards to <see cref="IEntityAspect.Discard(IEntity)"/>.
        /// </summary>
        /// <param name="e">The entity from which the aspect will be discarded.</param>
        /// <param name="entityAspect">The aspect instance to discard from the entity.</param>
        /// <remarks>
        /// The method is annotated with <see cref="MethodImplOptions.AggressiveInlining"/> to encourage
        /// the JIT to inline the call and reduce invocation overhead.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discard(this IEntity e, IEntityAspect entityAspect) => entityAspect.Discard(e);

        /// <summary>
        /// Discards the specified generic <see cref="IEntityAspect{E}"/> from the given entity of type <typeparamref name="E"/>.
        /// This is a type-safe extension method that forwards to <see cref="IEntityAspect{E}.Discard(E)"/>.
        /// </summary>
        /// <typeparam name="E">The concrete entity type that implements <see cref="IEntity"/>.</typeparam>
        /// <param name="e">The entity from which the aspect will be discarded.</param>
        /// <param name="entityAspect">The generic aspect instance to discard from the entity.</param>
        /// <remarks>
        /// Use this overload when working with aspects that target a specific entity type for compile-time safety.
        /// The method is annotated with <see cref="MethodImplOptions.AggressiveInlining"/> to encourage inlining.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Discard<E>(this E e, IEntityAspect<E> entityAspect) where E : IEntity =>
            entityAspect.Discard(e);
    }
}