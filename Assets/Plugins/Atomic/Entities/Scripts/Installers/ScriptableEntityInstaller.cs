using UnityEngine;
using UnityEngine.Assertions;

namespace Atomic.Entities
{
    public abstract class ScriptableEntityInstaller : ScriptableObject
    {
        public abstract void Install(IEntity entity);
    }
    
    public abstract class ScriptableEntityInstaller<T> : ScriptableEntityInstaller where T : class, IEntity
    {
        public sealed override void Install(IEntity entity)
        {
            T tEntity = entity as T;
            
            Assert.IsNotNull(tEntity, $"Mismatch Entity type! Expected: {typeof(T).Name}");
            this.Install(tEntity);
        }

        protected abstract void Install(T entity);
    }
}