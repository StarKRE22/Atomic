using System;
using Atomic.Elements;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Extensions
{
    public sealed class StringPrefsVariable : IReactiveVariable<string>
    {
        public event Action<string> OnValueChanged;
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Value
        {
            get { return PlayerPrefs.GetString(this.key, this.defaultValue); }
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetString(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        private readonly string key;
        private readonly string defaultValue;

        public StringPrefsVariable(string key, string defaultValue = null)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public void Subscribe(Action<string> listener) => this.OnValueChanged += listener;

        public void Unsubscribe(Action<string> listener) => this.OnValueChanged -= listener;
    }
}