using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    /// <summary>
    /// A base class for singleton scene entities. Ensures a single instance of the entity exists
    /// per scene or globally, depending on the <see cref="_dontDestroyOnLoad"/> flag.
    /// </summary>
    /// <typeparam name="T">The concrete type of the singleton scene entity.</typeparam>
    public abstract class SceneEntitySingleton<T> : SceneEntity where T : SceneEntitySingleton<T>
    {
        [SerializeField]
        private bool _dontDestroyOnLoad;

        #region Instance

        /// <summary>
        /// Gets the singleton instance of type <typeparamref name="T"/> in the current scene or globally.
        /// Throws an exception if no instance is found.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

#if UNITY_2023_1_OR_NEWER
                _instance = FindFirstObjectByType<T>();
#else
                _instance = FindObjectOfType<T>();
#endif

                return _instance == null
                    ? throw new Exception($"Scene Entity Sigleton of type {typeof(T).Name} is not found!")
                    : _instance;
            }
        }

        private static T _instance;

        /// <summary>
        /// Assigns the singleton instance and optionally makes it persistent across scenes.
        /// </summary>
        protected override void Awake()
        {
            if (_instance == null)
                _instance = (T) this;

            base.Awake();

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }

        /// <summary>
        /// Clears the singleton reference when destroyed.
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (_instance == this)
                _instance = null;
        }

        #endregion

        #region Resolve

        private static readonly Dictionary<Scene, T> _singletons = new();

        /// <summary>
        /// Resolves the singleton instance for the scene containing the given component.
        /// </summary>
        /// <param name="component">The component whose scene will be used for lookup.</param>
        /// <returns>The singleton instance found in the component's scene.</returns>
        public static T Resolve(in Component component) => Resolve(component.gameObject);

        /// <summary>
        /// Resolves the singleton instance for the scene containing the given GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject whose scene will be used for lookup.</param>
        /// <returns>The singleton instance found in the GameObject's scene.</returns>
        public static T Resolve(in GameObject gameObject) => Resolve(gameObject.scene);

        /// <summary>
        /// Resolves the singleton instance for the given scene.
        /// </summary>
        /// <param name="scene">The scene to search for the singleton.</param>
        /// <returns>The singleton instance if found; otherwise, throws an exception.</returns>
        public static T Resolve(Scene scene)
        {
            if (_singletons.TryGetValue(scene, out T singleton) && singleton)
                return singleton;

            List<GameObject> gameObjects = ListPool<GameObject>.Get();
            scene.GetRootGameObjects(gameObjects);
            for (int i = 0, count = gameObjects.Count; i < count; i++)
            {
                GameObject go = gameObjects[i];
                singleton = go.GetComponentInChildren<T>();
                if (!singleton)
                    continue;

                _singletons[scene] = singleton;
                return singleton;
            }

            ListPool<GameObject>.Release(gameObjects);
            throw new Exception($"Scene Entity Sigleton of type {typeof(T).Name} is not found!");
        }

        #endregion
    }
}