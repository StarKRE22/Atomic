using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A lightweight, inline implementation of non-generic <see cref="IEntityFactory"/>.
    /// </summary>
    public class InlineEntityFactory : InlineEntityFactory<IEntity>, IEntityFactory
    {
        public InlineEntityFactory(Func<IEntity> creator) : base(creator)
        {
        }
    }

    /// <summary>
    /// A lightweight, inline implementation of <see cref="IEntityFactory{T}"/> that wraps a creation delegate.
    /// </summary>
    /// <typeparam name="T">The type of <see cref="IEntity"/> to produce.</typeparam>
    public class InlineEntityFactory<T> : IEntityFactory<T> where T : IEntity
    {
        private readonly Func<T> _creator;

        /// <summary>
        /// Creates a new inline factory that uses the specified creation function.
        /// </summary>
        /// <param name="creator">The function used to instantiate the entity.</param>
        public InlineEntityFactory(Func<T> creator) =>
            _creator = creator ?? throw new ArgumentNullException(nameof(creator));

        /// <inheritdoc />
        public T Create() => _creator.Invoke();
    }
}