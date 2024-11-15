using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

[assembly: InternalsVisibleTo("Atomic.Entities.Tests")]

namespace Atomic.Entities
{
    internal sealed class SceneEntityUpdater : MonoBehaviour
    {
        private const string OBJECT_NAME = "SceneEntityUpdater";

        private static SceneEntityUpdater _instance;
        private static bool installed;

        private static SceneEntityUpdater instance
        {
            get
            {
                if (_instance == null && !installed)
                {
                    GameObject go = new GameObject(OBJECT_NAME);
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<SceneEntityUpdater>();
                    installed = true;
                }

                return _instance;
            }
        }

        private readonly List<IEntity> entities = new();
        private readonly List<IEntity> _entities = new();

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

            _entities.Clear();
            _entities.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _entities[i];
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

            _entities.Clear();
            _entities.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _entities[i];
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

            _entities.Clear();
            _entities.AddRange(this.entities);

            for (int i = 0; i < count; i++)
            {
                IEntity entity = _entities[i];
                entity.OnLateUpdate(deltaTime);
            }
        }

        #endregion
    }
}