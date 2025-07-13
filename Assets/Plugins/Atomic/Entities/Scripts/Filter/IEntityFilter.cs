using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /**
     * Experimental.
     */
    public interface IEntityFilter : IDisposable, IEnumerable<IEntity>
    {
        event Action<IEntity> OnAdded;
        event Action<IEntity> OnDeleted;

        int Count { get; }
        
        bool Has(IEntity entity);
        IEntity[] GetAll();
        int GetAll(IEntity[] results);
        void CopyTo(ICollection<IEntity> results);
        
        public interface ITrigger
        {
            void Subscribe(IEntity entity, Action<IEntity> callback);
            void Unsubscribe(IEntity entity, Action<IEntity> callback);
        }
    }
}