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
    /// A reactive variable that persists its bool value in Unity's <see cref="PlayerPrefs"/>.
    /// Notifies subscribers whenever the value changes.
    /// </summary>
    public sealed class ReactiveBoolPrefs : IReactiveVariable<bool>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<bool> OnValueChanged;

        /// <summary>
        /// Gets or sets the value stored in <see cref="PlayerPrefs"/> for the specified key.
        /// Triggers <see cref="OnValueChanged"/> when the value changes.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public bool Value
        {
            get => PlayerPrefs.GetInt(this.key, this.defaultValue ? 1 : 0) != 0;
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetInt(this.key, value ? 1 : 0);
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
        private readonly bool defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveBoolPrefs"/> class.
        /// </summary>
        /// <param name="key">The key to store and retrieve the value in <see cref="PlayerPrefs"/>.</param>
        /// <param name="defaultValue">The default value returned if the key does not exist.</param>
        public ReactiveBoolPrefs(string key, bool defaultValue = false)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Subscribes to value change events.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription object that can be used to unsubscribe.</returns>
        public Subscription<bool> Subscribe(Action<bool> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<bool>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from value change notifications.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<bool> listener) => this.OnValueChanged -= listener;
    }
}
#endif