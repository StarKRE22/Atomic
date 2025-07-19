using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
// ReSharper disable StaticMemberInGenericType

namespace Atomic.Entities
{
    [AddComponentMenu("Atomic/Entities/Entity Loop")]
    public sealed class ScenePlayerLoop<E> : MonoBehaviour where E : IEntity<E>
    {
        private static readonly IEqualityComparer<E> s_entityComparer = EqualityComparer<E>.Default;
        private static ScenePlayerLoop<E> _instance;
        private static bool _spawned;

        private E[] _entities;
        private int _entityCount;

        private static ScenePlayerLoop<E> instance
        {
            get
            {
                if (_instance == null && !_spawned)
                {
                    _instance = CreateInstance();
                    _spawned = true;
                }

                return _instance;
            }
        }

        public static void AddEntity(E entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                InternalUtils.AddIfAbsent(ref instance._entities, ref instance._entityCount, entity, s_entityComparer);
#else
                InternalUtils.AddIfAbsent(ref instance._entities, ref instance._entityCount, entity, s_entityComparer);
#endif
        }

        public static void DelEntity(E entity)
        {
#if UNITY_EDITOR
            if (EditorApplication.isPlaying)
                InternalUtils.Remove(ref instance._entities, ref _instance._entityCount, entity, s_entityComparer);
#else
                InternalUtils.Remove(ref instance._entities, ref _instance._entityCount, entity, s_entityComparer);
#endif
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _entityCount; i++) 
                _entities[i].OnUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _entityCount; i++) 
                _entities[i].OnFixedUpdate(deltaTime);
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _entityCount; i++) 
                _entities[i].OnLateUpdate(deltaTime);
        }

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            _spawned = false;
        }
#endif
        private static ScenePlayerLoop<E> CreateInstance()
        {
            GameObject go = new GameObject($"Player Loop ({typeof(E).Name})");
            DontDestroyOnLoad(go);
            return go.AddComponent<ScenePlayerLoop<E>>();
        }
    }
}