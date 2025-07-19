using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IWorld<E>
    {
        bool GetWithValue(int valueKey, out E result);
    
        IReadOnlyList<E> GetAllWithValue(int valueKey);
        
        int GetAllWithValue(int valueKey, E[] results);
    }
}