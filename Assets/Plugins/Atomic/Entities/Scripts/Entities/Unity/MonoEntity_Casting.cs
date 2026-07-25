#if UNITY_5_3_OR_NEWER
using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public partial class MonoEntity
    {
        /// <summary>
        /// Casts the specified <see cref="IEntity"/> to a <see cref="MonoEntity"/> if possible.
        /// </summary>
        /// <param name="entity">The entity to cast.</param>
        /// <returns>The entity cast to <see cref="MonoEntity"/>, or <c>null</c> if the input is <c>null</c>.</returns>
        /// <exception cref="InvalidCastException">Thrown if the entity cannot be cast to <see cref="MonoEntity"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MonoEntity Cast(IEntity entity) => Cast<MonoEntity>(entity);

        /// <summary>
        /// Casts the specified <see cref="IEntity"/> to the target type <typeparamref name="E"/> if possible.
        /// Supports direct MonoEntity instances and <see cref="MonoEntityProxy{E}"/> wrappers.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="MonoEntity"/> to cast to.</typeparam>
        /// <param name="entity">The entity to cast.</param>
        /// <returns>
        /// The entity cast to type <typeparamref name="E"/>, or <c>null</c> if the input is <c>null</c>.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if the entity cannot be cast to the target type <typeparamref name="E"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Cast<E>(IEntity entity) where E : MonoEntity => entity switch
        {
            null => null,
            E sceneEntity => sceneEntity,
            MonoEntityProxy<E> proxy => proxy.Source,
            _ => throw new InvalidCastException($"Can't cast {entity.Name} to {typeof(E).Name}")
        };

        /// <summary>
        /// Attempts to cast the specified <see cref="IEntity"/> to a <see cref="MonoEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to cast.</param>
        /// <param name="result">The cast result if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the cast was successful; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryCast(IEntity entity, out MonoEntity result) => TryCast<MonoEntity>(entity, out result);

        /// <summary>
        /// Attempts to cast the specified <see cref="IEntity"/> to the target type <typeparamref name="E"/>.
        /// Supports direct <see cref="MonoEntity"/> instances and <see cref="MonoEntityProxy{E}"/> wrappers.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="MonoEntity"/> to cast to.</typeparam>
        /// <param name="entity">The entity to cast.</param>
        /// <param name="result">The cast result if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the cast was successful; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryCast<E>(IEntity entity, out E result) where E : MonoEntity
        {
            if (entity is E sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is MonoEntityProxy<E> proxy)
            {
                result = proxy.Source;
                return true;
            }

            result = null;
            return false;
        }
    }
}
#endif