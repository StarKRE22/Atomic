using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntityWorld
    {
        bool GetWithTag(in int tag, out IEntity result);
        IReadOnlyList<IEntity> GetAllWithTag(in int tag);
        int GetAllWithTag(in int tag, in IEntity[] results);
    }
}