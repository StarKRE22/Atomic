using System;

namespace Atomic.Entities
{
    public sealed class EntityPoolMock : IEntityPool
    {
        public Action DisposeMethod;
        public Func<IEntity> RentMethod;
        public Action<IEntity> ReturnMethod;
        public Action<int> InitMethod;
        
        public void Dispose() => this.DisposeMethod.Invoke();

        public IEntity Rent() => this.RentMethod.Invoke();

        public void Return(IEntity entity) => this.ReturnMethod.Invoke(entity);

        public void Init(int initialCount) => this.InitMethod.Invoke(initialCount);
    }
    
    public sealed class EntityPoolMock<T> : IEntityPool<T> where T : IEntity
    {
        public Action DisposeMethod;
        public Func<T> RentMethod;
        public Action<T> ReturnMethod;
        public Action<int> InitMethod;
        
        public void Dispose() => this.DisposeMethod.Invoke();

        public T Rent() => this.RentMethod.Invoke();

        public void Return(T entity) => this.ReturnMethod.Invoke(entity);

        public void Init(int initialCount) => this.InitMethod.Invoke(initialCount);
    }
}