using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntityWorld
    {
        bool GetWithValue(in int valueKey, out IEntity result);
        IReadOnlyList<IEntity> GetAllWithValue(in int valueKey);
        int GetAllWithValue(in int valueKey, in IEntity[] results);
    }
}