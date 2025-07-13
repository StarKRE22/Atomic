using System;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public sealed class BaseVariable<T> : IVariable<T>
    {
        private static readonly IEqualityComparer<T> equalityComparer = EqualityComparer.GetDefault<T>();
        
        public T Value
        {
            get { return this.value; }
            set { if (!equalityComparer.Equals(this.value, value)) this.value = value; }
        }

        [SerializeField]
        private T value;

        public T Invoke() => this.value;

        public BaseVariable() => this.value = default;

        public BaseVariable(T value) => this.value = value;

        public static implicit operator BaseVariable<T>(T value) => new(value);

        public override string ToString() => this.value.ToString();
    }
}