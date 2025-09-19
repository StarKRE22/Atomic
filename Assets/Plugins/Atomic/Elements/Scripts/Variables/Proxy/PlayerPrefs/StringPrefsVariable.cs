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
    /// A variable that persists its string value in Unity's <see cref="PlayerPrefs"/>.
    /// Implements <see cref="IVariable{string}"/> without reactive subscriptions.
    /// </summary>
    [Serializable]
    public sealed class StringPrefsVariable : IVariable<string>
    {
        /// <summary>
        /// Gets or sets the value stored in <see cref="PlayerPrefs"/> for the specified key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Value
        {
            get => PlayerPrefs.GetString(this.key, this.defaultValue);
            set => PlayerPrefs.SetString(this.key, value);
        }

        /// <summary>
        /// The key used to store the value in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string key;

        /// <summary>
        /// The default value returned when no key exists in <see cref="PlayerPrefs"/>.
        /// </summary>
        private readonly string defaultValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringPrefsVariable"/> class.
        /// </summary>
        /// <param name="key">The key to store and retrieve the value in <see cref="PlayerPrefs"/>.</param>
        /// <param name="defaultValue">The default value returned if the key does not exist.</param>
        public StringPrefsVariable(string key, string defaultValue = null)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Returns the current value (from PlayerPrefs).
        /// </summary>
        public string Invoke() => this.Value;
    }
}
#endif