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
    /// A reactive integer variable that stores its value using Unity's <see cref="PlayerPrefs"/>.
    /// Notifies listeners when the value changes.
    /// </summary>
    public sealed class ReactiveIntPrefs : IReactiveVariable<int>
    {
        /// <summary>
        /// Invoked when the value changes.
        /// </summary>
        public event Action<int> OnValueChanged;

        /// <summary>
        /// Gets or sets the integer value stored in <see cref="PlayerPrefs"/>.
        /// Triggers <see cref="OnValueChanged"/> when the value is modified.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public int Value
        {
            get => PlayerPrefs.GetInt(this.key, this.defaultValue);
            set
            {
                if (this.Value != value)
                {
                    PlayerPrefs.SetInt(this.key, value);
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// The key used to store and retrieve the value from <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The default value returned when the key is not present in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly int defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveIntPrefs"/> class.
        /// </summary>
        /// <param name="key">The key used to store the value.</param>
        /// <param name="defaultValue">The default value used if the key is not found.</param>
        public ReactiveIntPrefs(string key, int defaultValue = default)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Subscribes a listener to value changes.
        /// </summary>
        /// <param name="listener">The callback to invoke when the value changes.</param>
        /// <returns>A subscription object for managing unsubscription.</returns>
        public Subscription<int> Subscribe(Action<int> listener)
        {
            this.OnValueChanged += listener;
            return new Subscription<int>(this, listener);
        }

        /// <summary>
        /// Unsubscribes a listener from value changes.
        /// </summary>
        /// <param name="listener">The callback to remove.</param>
        public void Unsubscribe(Action<int> listener) => this.OnValueChanged -= listener;
    }
}
#endif