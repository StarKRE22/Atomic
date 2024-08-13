
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a serialized reference.
    
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class Reference<T>
    {
        public ref T Value
        {
            get { return ref this.value; }
        }

#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private T value;
    }
}
