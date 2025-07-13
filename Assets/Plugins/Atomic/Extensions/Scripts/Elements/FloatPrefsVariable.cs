using System;
using Atomic.Elements;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    public sealed class FloatPrefsVariable : IReactiveVariable<float>
    {
        public event Action<float> OnValueChanged;
        
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif

        public float Value
        {
            get
            {
                return PlayerPrefs.GetFloat(this.key, this.defaultValue);
            }
            set
            {
                if (Mathf.Abs(this.Value - value) > float.Epsilon)
                {
                    PlayerPrefs.SetFloat(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        private readonly string key;
        private readonly float defaultValue;

        public FloatPrefsVariable(string key, float defaultValue = default)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public void Subscribe(Action<float> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<float> listener) => this.OnValueChanged -= listener;
    }
}