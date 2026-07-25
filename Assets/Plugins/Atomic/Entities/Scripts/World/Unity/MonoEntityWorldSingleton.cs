#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A singleton implementation of <see cref="MonoEntityWorld{E}"/>.
    /// Ensures that only one instance exists in the scene or globally.
    /// </summary>
    /// <typeparam name="E">Entity type.</typeparam>
    public abstract class MonoEntityWorldSingleton : MonoEntityWorld
    {
        [Tooltip("If enabled, this object will persist across scene loads.")]
        [SerializeField]
        private bool dontDestroyOnLoad;

        private static MonoEntityWorldSingleton _instance;

        /// <summary>
        /// Gets the singleton instance.
        /// Throws an exception if not found.
        /// </summary>
        public static MonoEntityWorldSingleton Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindAnyObjectByType<MonoEntityWorldSingleton>(FindObjectsInactive.Exclude);

                if (_instance == null)
                    throw new Exception(
                        $"MonoEntityWorldSingleton was not found in the scene.");

                return _instance;
            }
        }

        /// <summary>
        /// Attempts to get the singleton instance.
        /// </summary>
        public static bool TryGetInstance(out MonoEntityWorldSingleton instance)
        {
            if (_instance != null)
            {
                instance = _instance;
                return true;
            }

            instance = _instance = FindAnyObjectByType<MonoEntityWorldSingleton>(FindObjectsInactive.Exclude);
            return instance != null;
        }

        /// <summary>
        /// Initializes singleton instance.
        /// </summary>
        private protected override void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError(
                    $"Duplicate MonoEntityWorldSingleton detected on '{name}'. Destroying this instance.",
                    this);

                Destroy(gameObject);
                return;
            }

            _instance = this;

            base.Awake();

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Clears singleton reference when destroyed.
        /// </summary>
        private protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_instance == this)
                _instance = null;
        }
    }
}
#endif