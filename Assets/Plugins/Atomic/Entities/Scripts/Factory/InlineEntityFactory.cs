using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A lightweight, inline implementation of non-generic <see cref="IEntityFactory"/>.
    /// </summary>
    public class InlineEntityFactory : InlineEntityFactory<IEntity>, IEntityFactory
    {
        public InlineEntityFactory(Func<IEntity> createFunc) : base(createFunc)
        {
        }
    }
}