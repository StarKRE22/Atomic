using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Atomic.Events
{
    public abstract class SceneEventBusSingleton<T> : SceneEventBus where T : SceneEventBusSingleton<T>
    {
        [SerializeField]
        private bool _dontDestroyOnLoad;

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

                if (_instance == null)
                    throw new NullReferenceException(
                        $"Scene Event Bus Singleton of type {typeof(T).Name} is not found on scene!");

                return _instance;
            }
        }

        private static T _instance;

        protected virtual void Awake()
        {
            if (_instance == null) 
                _instance = (T) this;
            
            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            if (_instance == this) 
                _instance = null;
        }

        #region Resolve

        private static readonly Dictionary<Scene, T> _singletons = new();

        public static T Resolve(in Component component) => Resolve(component.gameObject);

        public static T Resolve(in GameObject gameObject) => Resolve(gameObject.scene);

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
            throw new Exception($"Scene Event Bus Sigleton of type {typeof(T).Name} is not found!");
        }

        #endregion
    }
}