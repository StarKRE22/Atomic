#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A base class for singleton scene entities.
    /// Ensures that only one instance exists per scene or globally.
    /// </summary>
    public abstract class MonoEntitySingleton<E> : MonoEntity where E : MonoEntitySingleton<E>
    {
#if ODIN_INSPECTOR
        [PropertyOrder(-10)]
        [DisableInPlayMode]
        [PropertySpace(SpaceBefore = 0)]
#endif
        [Tooltip("If enabled, this object will persist across scene loads.")]
        [SerializeField]
        private bool dontDestroyOnLoad;

        private static E _instance;

        /// <summary>
        /// Gets the singleton instance.
        /// Throws if not found.
        /// </summary>
        public static E Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindAnyObjectByType<E>(FindObjectsInactive.Exclude);

                if (_instance == null)
                    throw new Exception($"Singleton of type {typeof(E).Name} was not found in the scene.");

                return _instance;
            }
        }

        /// <summary>
        /// Attempts to get the singleton instance.
        /// </summary>
        public static bool TryGetInstance(out E instance)
        {
            if (_instance != null)
            {
                instance = _instance;
                return true;
            }

            instance = _instance = FindAnyObjectByType<E>(FindObjectsInactive.Exclude);
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
                    $"Duplicate singleton of type {typeof(E).Name} detected on '{name}'. Destroying this instance.",
                    this);

                Destroy(gameObject);
                return;
            }

            _instance = (E) this;

            base.Awake();

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Clears singleton reference on destroy.
        /// </summary>
        private protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_instance == this)
                _instance = null;
        }

#if UNITY_EDITOR
        private protected override void Reset()
        {
            base.Reset();
            dontDestroyOnLoad = false;
        }
#endif
    }
}
#endif