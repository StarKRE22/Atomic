using System;

namespace Atomic.Entities
{
    public class MultiEntityFactoryMock : IMultiEntityFactory
    {
        public delegate bool TryCreateHandler(string key, out IEntity entity);

        public Action<string, IEntityFactory<IEntity>> AddMethod;
        public Action<string> RemoveMethod;
        public Func<string, IEntity> CreateMethod;
        public TryCreateHandler TryCreateMethod;
        public Func<string, bool> ContainsMethod;

        public void Register(string key, IEntityFactory<IEntity> factory) => this.AddMethod?.Invoke(key, factory);

        public void Unregister(string key) => this.RemoveMethod?.Invoke(key);

        public IEntity Create(string key) => this.CreateMethod?.Invoke(key);

        public bool TryCreate(string key, out IEntity entity) => TryCreateMethod.Invoke(key, out entity);

        public bool Contains(string key) => ContainsMethod.Invoke(key);
    }
}