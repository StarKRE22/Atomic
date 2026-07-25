#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A singleton-based implementation of <see cref="EntityWorldView{E, V}"/>.
    /// Ensures that only one instance exists in the scene (or globally if marked as persistent).
    /// </summary>
    /// <typeparam name="E">Entity type.</typeparam>
    /// <typeparam name="V">View type.</typeparam>
    public abstract class EntityWorldViewSingleton<K, E, V> : EntityWorldView<K, E, V>
        where E : class, IEntity
        where V : EntityView<E>
    {
#if ODIN_INSPECTOR
        [PropertyOrder(-10)]
        [DisableInPlayMode]
        [PropertySpace(SpaceBefore = 0)]
#endif
        [Tooltip("If enabled, this object will not be destroyed when loading a new scene.")]
        [SerializeField]
        private bool dontDestroyOnLoad;

        private static EntityWorldViewSingleton<K, E, V> _instance;

        /// <summary>
        /// Gets the singleton instance.
        /// Throws an exception if no instance is found in the scene.
        /// </summary>
        public static EntityWorldViewSingleton<K, E, V> Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindAnyObjectByType<EntityWorldViewSingleton<K, E, V>>(FindObjectsInactive.Exclude);

                if (_instance == null)
                    throw new Exception(
                        $"Singleton of type {typeof(EntityWorldViewSingleton<K, E, V>).Name} was not found in the scene.");

                return _instance;
            }
        }

        /// <summary>
        /// Attempts to get the singleton instance without throwing.
        /// </summary>
        public static bool TryGetInstance(out EntityWorldViewSingleton<K, E, V> instance)
        {
            if (_instance != null)
            {
                instance = _instance;
                return true;
            }

            instance = _instance = FindAnyObjectByType<EntityWorldViewSingleton<K, E, V>>(FindObjectsInactive.Exclude);
            return instance != null;
        }

        /// <summary>
        /// Unity Awake lifecycle method.
        /// Initializes the singleton instance.
        /// </summary>
        private void Awake()
        {
            this.InitializeSingleton();
        }

        /// <summary>
        /// Ensures only one instance exists and optionally makes it persistent.
        /// </summary>
        private void InitializeSingleton()
        {
            if (_instance != null && _instance != this)
            {
                Debug.LogError(
                    $"Duplicate singleton of type {typeof(EntityWorldViewSingleton<K, E, V>).Name} detected on '{name}'. Destroying this instance.",
                    this);

                Destroy(gameObject);
                return;
            }

            _instance = this;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Clears the singleton reference when the object is destroyed.
        /// </summary>
        private protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
#endif