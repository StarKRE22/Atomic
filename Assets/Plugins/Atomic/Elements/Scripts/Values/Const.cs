using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a serialized read-only property.
    
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class Const<T> : IValue<T>
    {
        public T Invoke() => this.value;

        public T Value => this.value;

#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeField]
        private T value;

        public Const()
        {
        }

        public Const(T value) => this.value = value;

        public static implicit operator Const<T>(T value) => new Const<T>(value);

        public override string ToString() => this.value.ToString();
    }
}
