using System.Collections.Generic;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a modifiable view of an entity collection. 
    /// Extends <see cref="IReadOnlyEntityCollectionView"/> by allowing adding, removing, and clearing entity views.
    /// </summary>
    public interface IEntityCollectionView : IReadOnlyEntityCollectionView, IDictionary<IEntity, IEntityView>
    {
        
    }
}
