using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IWorld<E>
    {
        bool GetWithTag(int tag, out E result);
        
        IReadOnlyList<E> GetAllWithTag(int tag);
        
        int GetAllWithTag(int tag, E[] results);
    }
}