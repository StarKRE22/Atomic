using System;

namespace Atomic.Entities
{
    public class MultiEntityFactoryMock : IMultiEntityFactory
    {
        public Action<string, IEntityFactory<IEntity>> AddMethod;
        public Action<string> RemoveMethod;
        public Func<string, IEntity> CreateMethod;
            
        public void Register(string key, IEntityFactory<IEntity> factory) => this.AddMethod?.Invoke(key, factory);

        public void Unregister(string key) => this.RemoveMethod?.Invoke(key);

        public IEntity Create(string key) => this.CreateMethod?.Invoke(key);
    }
}