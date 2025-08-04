using System;
// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Atomic.Entities
{
    public sealed class EntityFactoryMock : IEntityFactory
    {
        public Func<IEntity> create = () => new Entity();
        
        public IEntity Create() => create?.Invoke();
    }
}