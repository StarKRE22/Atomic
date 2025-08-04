using System;

namespace Atomic.Entities
{
    public class MultiEntityFactoryMock : IMultiEntityFactory
    {
        public Action<string, IEntityFactory<IEntity>> add;
        public Action<string> remove;
        public Func<string, IEntity> create;
            
        public void Add(string key, IEntityFactory<IEntity> factory) => this.add?.Invoke(key, factory);

        public void Remove(string key) => this.remove?.Invoke(key);

        public IEntity Create(string key) => this.create?.Invoke(key);
    }
}