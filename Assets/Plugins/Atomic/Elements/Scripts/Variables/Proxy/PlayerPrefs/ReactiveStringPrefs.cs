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
    /// A reactive string variable that persists its value using Unity's <see cref="PlayerPrefs"/>.
    /// Triggers <see cref="OnValueChanged"/> whenever the stored value changes.
    /// </summary>
    public sealed class ReactiveStringPrefs : IReactiveVariable<string>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<string> OnValueChanged;

        /// <summary>
        /// Gets or sets the string value stored in <see cref="PlayerPrefs"/>.
        /// Triggers <see cref="OnValueChanged"/> when changed.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Value
        {
            get => PlayerPrefs.GetString(this.key, this.defaultValue);
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetString(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// The key used to store and retrieve the value from <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The default value used if the key is not present in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveStringPrefs"/> class.
        /// </summary>
        /// <param name="key">The key used in <see cref="PlayerPrefs"/>.</param>
        /// <param name="defaultValue">The default value if the key does not exist.</param>
        public ReactiveStringPrefs(string key, string defaultValue = null)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Subscribes a listener to value change notifications.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription object for managing unsubscription.</returns>
        public Subscription<string> Subscribe(Action<string> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<string>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from value change notifications.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<string> listener) => this.OnValueChanged -= listener;
    }
}
#endif