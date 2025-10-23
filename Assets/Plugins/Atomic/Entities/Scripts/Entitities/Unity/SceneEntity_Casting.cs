#if UNITY_5_3_OR_NEWER
using System;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        /// <summary>
        /// Casts the specified <see cref="IEntity"/> to a <see cref="SceneEntity"/> if possible.
        /// </summary>
        /// <param name="entity">The entity to cast.</param>
        /// <returns>The entity cast to <see cref="SceneEntity"/>, or <c>null</c> if the input is <c>null</c>.</returns>
        /// <exception cref="InvalidCastException">Thrown if the entity cannot be cast to <see cref="SceneEntity"/>.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SceneEntity Cast(IEntity entity) => Cast<SceneEntity>(entity);

        /// <summary>
        /// Casts the specified <see cref="IEntity"/> to the target type <typeparamref name="E"/> if possible.
        /// Supports direct SceneEntity instances and <see cref="SceneEntityProxy{E}"/> wrappers.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to cast to.</typeparam>
        /// <param name="entity">The entity to cast.</param>
        /// <returns>
        /// The entity cast to type <typeparamref name="E"/>, or <c>null</c> if the input is <c>null</c>.
        /// </returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if the entity cannot be cast to the target type <typeparamref name="E"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Cast<E>(IEntity entity) where E : SceneEntity => entity switch
        {
            null => null,
            E sceneEntity => sceneEntity,
            SceneEntityProxy<E> proxy => proxy.Source,
            _ => throw new InvalidCastException($"Can't cast {entity.Name} to {typeof(E).Name}")
        };

        /// <summary>
        /// Attempts to cast the specified <see cref="IEntity"/> to a <see cref="SceneEntity"/>.
        /// </summary>
        /// <param name="entity">The entity to cast.</param>
        /// <param name="result">The cast result if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the cast was successful; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryCast(IEntity entity, out SceneEntity result) => TryCast<SceneEntity>(entity, out result);

        /// <summary>
        /// Attempts to cast the specified <see cref="IEntity"/> to the target type <typeparamref name="E"/>.
        /// Supports direct <see cref="SceneEntity"/> instances and <see cref="SceneEntityProxy{E}"/> wrappers.
        /// </summary>
        /// <typeparam name="E">The type of <see cref="SceneEntity"/> to cast to.</typeparam>
        /// <param name="entity">The entity to cast.</param>
        /// <param name="result">The cast result if successful; otherwise, <c>null</c>.</param>
        /// <returns><c>true</c> if the cast was successful; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryCast<E>(IEntity entity, out E result) where E : SceneEntity
        {
            if (entity is E sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is SceneEntityProxy<E> proxy)
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