using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A lightweight, inline implementation of <see cref="IEntityFactory{T}"/> that wraps a creation delegate.
    /// </summary>
    /// <typeparam name="E">The type of <see cref="IEntity"/> to produce.</typeparam>
    public class InlineEntityFactory<E> : IEntityFactory<E> where E : IEntity
    {
        private readonly Func<E> _createFunc;

        /// <summary>
        /// Creates a new inline factory that uses the specified creation function.
        /// </summary>
        /// <param name="createFunc">The function used to instantiate the entity.</param>
        public InlineEntityFactory(Func<E> createFunc) =>
            _createFunc = createFunc ?? throw new ArgumentNullException(nameof(createFunc));

        /// <inheritdoc />
        public E Create() => _createFunc.Invoke();
    }
}