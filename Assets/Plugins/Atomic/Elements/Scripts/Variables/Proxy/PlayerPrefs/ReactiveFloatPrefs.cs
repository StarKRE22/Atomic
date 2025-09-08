#if UNITY_5_3_OR_NEWER
using System;
using Atomic.Elements;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive variable that persists its float value in Unity's <see cref="PlayerPrefs"/>.
    /// Notifies subscribers whenever the value changes.
    /// </summary>
    public sealed class ReactiveFloatPrefs : IReactiveVariable<float>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<float> OnValueChanged;

        /// <summary>
        /// Gets or sets the value stored in <see cref="PlayerPrefs"/> for the specified key.
        /// Triggers <see cref="OnValueChanged"/> when the value changes.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public float Value
        {
            get => PlayerPrefs.GetFloat(this.key, this.defaultValue);
            set
            {
                if (Mathf.Abs(this.Value - value) > float.Epsilon)
                {
                    PlayerPrefs.SetFloat(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// The key used to store the value in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The default value returned when no key exists in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly float defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveFloatPrefs"/> class.
        /// </summary>
        /// <param name="key">The key to store and retrieve the value in <see cref="PlayerPrefs"/>.</param>
        /// <param name="defaultValue">The default value returned if the key does not exist.</param>
        public ReactiveFloatPrefs(string key, float defaultValue = default)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Subscribes to value change events.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription object that can be used to unsubscribe.</returns>
        public Subscription<float> Subscribe(Action<float> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<float>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from value change notifications.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<float> listener) => this.OnValueChanged -= listener;
    }
}
#endif