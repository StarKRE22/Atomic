using System;
using Atomic.Elements;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    public sealed class IntPrefsVariable : IReactiveVariable<int>
    {
        public event Action<int> OnValueChanged;

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public int Value
        {
            get
            {
                return PlayerPrefs.GetInt(this.key, this.defaultValue);
            }
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetInt(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        private readonly string key;
        private readonly int defaultValue;

        public IntPrefsVariable(string key, int defaultValue = default)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public void Subscribe(Action<int> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<int> listener) => this.OnValueChanged -= listener;
    }
}