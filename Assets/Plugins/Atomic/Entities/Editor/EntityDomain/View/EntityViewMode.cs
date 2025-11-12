using System;

namespace Atomic.Entities
{
    [Flags]
    public enum EntityViewMode
    {
        None = 0,
        EntityView = 1,
        EntityViewCatalog = 2,
        EntityViewPool = 4,
        EntityCollectionView = 8
    }
}