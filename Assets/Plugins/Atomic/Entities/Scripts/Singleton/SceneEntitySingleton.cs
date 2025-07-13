using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    public abstract class SceneEntitySingleton<T> : SceneEntity where T : SceneEntitySingleton<T>
    {
        [SerializeField]
        private bool _dontDestroyOnLoad;
        
        #region Instance

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                _instance = FindObjectOfType<T>();

                return _instance == null
                    ? throw new Exception($"Scene Entity Sigleton of type {typeof(T).Name} is not found!")
                    : _instance;
            }
        }

        private static T _instance;

        protected override void Awake()
        {
            _instance = this as T;
            base.Awake();

            if (_dontDestroyOnLoad) 
                DontDestroyOnLoad(this.gameObject);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _instance = null;
        }

        #endregion

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
            throw new Exception($"Scene Entity Sigleton of type {typeof(T).Name} is not found!");
        }

        #endregion
    }
}