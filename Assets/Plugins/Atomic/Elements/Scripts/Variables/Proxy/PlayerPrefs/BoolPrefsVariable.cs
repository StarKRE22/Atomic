#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A variable that persists its bool value in Unity's <see cref="PlayerPrefs"/>.
    /// Simply implements <see cref="IVariable{bool}"/> without reactive subscriptions.
    /// </summary>
    [Serializable]
    public sealed class BoolPrefsVariable : IVariable<bool>
    {
        /// <summary>
        /// Gets or sets the value stored in <see cref="PlayerPrefs"/> for the specified key.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public bool Value
        {
            get => PlayerPrefs.GetInt(this.key, this.defaultValue ? 1 : 0) != 0;
            set => PlayerPrefs.SetInt(this.key, value ? 1 : 0);
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
        /// Initializes a new instance of the <see cref="BoolPrefsVariable"/> class.
        /// </summary>
        /// <param name="key">The key to store and retrieve the value in <see cref="PlayerPrefs"/>.</param>
        /// <param name="defaultValue">The default value returned if the key does not exist.</param>
        public BoolPrefsVariable(string key, bool defaultValue = false)
        {
            this.key = key;
            this.defaultValue = defaultValue;
        }

        /// <summary>
        /// Returns the current value (from PlayerPrefs).
        /// </summary>
        public bool Invoke() => this.Value;
    }
}
#endif