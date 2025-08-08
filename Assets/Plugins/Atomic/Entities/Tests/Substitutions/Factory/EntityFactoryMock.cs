using System;
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Atomic.Entities
{
    public sealed class EntityFactoryMock : IEntityFactory
    {
        public Func<IEntity> CreateMethod = () => new Entity();
        
        public IEntity Create() => CreateMethod?.Invoke();
    }
}