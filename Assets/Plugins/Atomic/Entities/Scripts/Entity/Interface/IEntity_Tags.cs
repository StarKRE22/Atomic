using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public partial interface IEntity
    {
        event Action<IEntity, int> OnTagAdded;
        event Action<IEntity, int> OnTagDeleted;
        
        int TagCount { get; }
        
        bool HasTag(in int key);
        bool AddTag(in int key);
        bool DelTag(in int key);
        void ClearTags();
        
        int[] GetTags();
        int GetTags(int[] results);
        IEnumerator<int> TagEnumerator();
    }
}