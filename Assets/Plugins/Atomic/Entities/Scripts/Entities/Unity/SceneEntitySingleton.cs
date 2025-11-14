#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    /// <typeparam name="E">The concrete type of the singleton scene entity.</typeparam>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Entities/SceneEntitySingleton.md")]
    public abstract class SceneEntitySingleton<E> : SceneEntity where E : SceneEntitySingleton<E>
    {
        private static IEntityFactory<E> _factory;

#if ODIN_INSPECTOR
        [PropertyOrder(-10)]
        [DisableInPlayMode]
#endif
        [Header("Singleton")]
        [Tooltip("Is it possible to contact via SceneEntitySingleton<T>.Instance?")]
        [SerializeField]
        private bool _isGlobal = true;

#if ODIN_INSPECTOR
        [PropertyOrder(-10)]
        [DisableInPlayMode]
        [PropertySpace(SpaceBefore = 0)]
#endif
        [Tooltip("Do not destroy the target Object when loading a new Scene?")]
        [SerializeField]
        private bool _dontDestroyOnLoad;

        #region Instance

        private static E _instance;

        /// <summary>
        /// Gets the singleton instance of type <typeparamref name="E"/> in the current scene or globally.
        /// Throws an exception if no instance is found.
        /// </summary>
        public static E Instance
        {
            get
            {
                if (_instance)
                    return _instance;

#if UNITY_2023_1_OR_NEWER
                _instance = FindObjectsByType<E>(FindObjectsSortMode.None).FirstOrDefault(it => it._isGlobal);
#else
                _instance = FindObjectsOfType<E>().FirstOrDefault(it => it._isGlobal);
#endif
                if (_instance)
                    return _instance;

                _instance = _factory?.Create();

                return _instance
                    ? _instance
                    : throw new Exception($"Scene Entity Singleton of type {typeof(E).Name} is not found!");
            }
        }

        /// <summary>
        /// Tries to get the singleton instance of type <typeparamref name="E"/> in the current scene or globally.
        /// </summary>
        /// <param name="instance">The retrieved singleton instance</param>
        /// <returns>True if the instance was retrieved</returns>
        public static bool TryGetInstance(out E instance)
        {
            instance = _instance;
            if (instance)
                return true;

#if UNITY_2023_1_OR_NEWER
            instance = _instance = FindObjectsByType<E>(FindObjectsSortMode.None).FirstOrDefault(it => it._isGlobal);
#else
            instance = _instance = FindObjectsOfType<E>().FirstOrDefault(it => it._isGlobal);
#endif
            if (instance)
                return true;

            instance = _instance = _factory?.Create();
            return instance;
        }

        /// <summary>
        /// Registers a custom factory method for creating the singleton instance.
        /// <para>
        /// This method must be called before the first access to <see cref="Instance"/>.
        /// </para>
        /// </summary>
        /// <param name="factory">
        /// The factory that will be used to create new instances of <typeparamref name="E"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="factory"/> is <c>null</c>.</exception>
        public static void SetFactory(IEntityFactory<E> factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        #endregion

        #region UnityLifecycle

        /// <summary>
        /// Assigns the singleton instance and optionally makes it persistent across scenes.
        /// </summary>
        private protected override void Awake()
        {
            if (_instance == null && _isGlobal)
            {
                _instance = (E) this;
            }
            else if (_instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            base.Awake();

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Clears the singleton reference when destroyed.
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
            _isGlobal = true;
            _dontDestroyOnLoad = false;
        }
#endif

        #endregion

        #region Resolve

        private static readonly Dictionary<Scene, E> _singletons = new();

        /// <summary>
        /// Resolves the singleton instance for the scene containing the given component.
        /// </summary>
        /// <param name="component">The component whose scene will be used for lookup.</param>
        /// <returns>The singleton instance found in the component's scene.</returns>
        public static E Resolve(Component component) => Resolve(component.gameObject);

        /// <summary>
        /// Resolves the singleton instance for the scene containing the given GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject whose scene will be used for lookup.</param>
        /// <returns>The singleton instance found in the GameObject's scene.</returns>
        public static E Resolve(GameObject gameObject) => Resolve(gameObject.scene);

        /// <summary>
        /// Resolves the singleton instance for the given scene.
        /// </summary>
        /// <param name="scene">The scene to search for the singleton.</param>
        /// <returns>The singleton instance if found; otherwise, throws an exception.</returns>
        public static E Resolve(Scene scene)
        {
            if (_singletons.TryGetValue(scene, out E singleton) && singleton)
                return singleton;

            List<GameObject> gameObjects = ListPool<GameObject>.Get();
            scene.GetRootGameObjects(gameObjects);
            for (int i = 0, count = gameObjects.Count; i < count; i++)
            {
                GameObject go = gameObjects[i];
                singleton = go.GetComponentInChildren<E>();
                if (!singleton)
                    continue;

                _singletons[scene] = singleton;
                return singleton;
            }

            ListPool<GameObject>.Release(gameObjects);
            throw new Exception($"Scene Entity Singleton of type {typeof(E).Name} is not found!");
        }

        #endregion
    }
}
#endif