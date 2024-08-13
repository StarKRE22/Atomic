#if UNITY_MATHEMATICS
using System;
using Atomic.Elements;
using Unity.Mathematics;
using UnityEngine;
    
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
    
namespace Atomic.Extensions
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class float3Reactive : IReactiveVariable<float3>
    {
        public event System.Action<float3> OnValueChanged;
            
#if ODIN_INSPECTOR
        [HideLabel, OnValueChanged(nameof(InvokeEvent))]
#endif
        [SerializeField]
        private float3 value;
    
        public float3 Value
        {
            get { return this.value; }
            set
            {
                if (!this.value.Equals(value))
                {
                    this.value = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }
    
        public float3Reactive()
        {
            this.value = default;
        }
    
        public float3Reactive(float3 value)
        {
            this.value = value;
        }
    
        public static implicit operator float3Reactive(float3 value)
        {
            return new float3Reactive(value);
        }
    
        public Action<float3> Subscribe(Action<float3> listener)
        {
            this.OnValueChanged += listener;
            return listener;
        }
    
        public void Unsubscribe(Action<float3> listener)
        {
            this.OnValueChanged -= listener;
        }
    
        private void InvokeEvent(float3 value)
        {
            this.OnValueChanged?.Invoke(value);
        }
    }
}
#endif