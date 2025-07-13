using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Atomic.Entities
{
    internal sealed class GlobalSceneEntityUpdater : MonoBehaviour
    {
        private static GlobalSceneEntityUpdater _instance;
        private static bool installed;

        private readonly HashSet<IEntity> entities = new();
        private readonly List<IEntity> _cache = new();

        private static GlobalSceneEntityUpdater instance
        {
            get
            {
                if (_instance == null && !installed)
                {
                    _instance = CreateInstance();
                    installed = true;
                }

                return _instance;
            }
        }
        
        public static void AddEntity(IEntity entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                instance.entities.Add(entity);
            }
#else
            instance.entities.Add(entity);
#endif
        }

        public static void DelEntity(IEntity entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
            {
                instance.entities.Remove(entity);
            }
#else
            instance.entities.Remove(entity);
#endif
        }

        #region Unity

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            int count = this.entities.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _cache[i];
                entity.OnUpdate(deltaTime);
            }
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            int count = this.entities.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _cache[i];
                entity.OnFixedUpdate(deltaTime);
            }
        }


        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            int count = this.entities.Count;
            if (count == 0)
            {
                return;
            }

            _cache.Clear();
            _cache.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _cache[i];
                entity.OnLateUpdate(deltaTime);
            }
        }

        #endregion

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            installed = false;
        }
#endif
        private static GlobalSceneEntityUpdater CreateInstance()
        {
            GameObject go = new GameObject(nameof(GlobalSceneEntityUpdater));
            DontDestroyOnLoad(go);
            return go.AddComponent<GlobalSceneEntityUpdater>();
        }
    }
}