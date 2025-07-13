using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveQuaternion : IReactiveVariable<Quaternion>, IDisposable
    {
        public event Action<Quaternion> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Quaternion value;

        public Quaternion Value
        {
            get { return this.value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        public ReactiveQuaternion() => this.value = default;

        public ReactiveQuaternion(Quaternion value) => this.value = value;
        
        public ReactiveQuaternion(float x, float y, float z, float w) => this.value = new Quaternion(x, y, z, w);

        public static implicit operator ReactiveQuaternion(Quaternion value) => new(value);

        public void Subscribe(Action<Quaternion> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<Quaternion> listener) => this.OnValueChanged -= listener;

        private void InvokeEvent(Quaternion value) => this.OnValueChanged?.Invoke(value);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnValueChanged);

        public override string ToString() => this.Value.ToString();
    }
}