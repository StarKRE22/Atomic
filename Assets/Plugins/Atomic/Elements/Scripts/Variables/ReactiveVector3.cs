using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public class ReactiveVector3 : IReactiveVariable<Vector3>, IDisposable
    {
        public event Action<Vector3> OnValueChanged;
        
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private Vector3 value;

        public Vector3 Value
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

        public ReactiveVector3() => this.value = default;

        public ReactiveVector3(Vector3 value) => this.value = value;

        public ReactiveVector3(float x, float y, float z) => this.value = new Vector3(x, y, z);

        public static implicit operator ReactiveVector3(Vector3 value) => new(value);

        public void Subscribe(Action<Vector3> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<Vector3> listener) => this.OnValueChanged -= listener;

        private void InvokeEvent(Vector3 value) => this.OnValueChanged?.Invoke(value);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnValueChanged);

        public override string ToString() => this.Value.ToString();
    }
}