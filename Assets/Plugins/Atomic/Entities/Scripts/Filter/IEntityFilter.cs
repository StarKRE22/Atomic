using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    /**
     * Experimental.
     */
    public interface IEntityFilter : IDisposable, IEnumerable<IEntity>
    {
        event Action<IEntity> OnEntityAdded;
        event Action<IEntity> OnEntityDeleted;
        
        List<IEntity> Entities { get; }
        
        int CopyTo(IEntity[] results);
        void CopyTo(List<IEntity> results);
        
        bool HasEntity(IEntity entity);
    }
}