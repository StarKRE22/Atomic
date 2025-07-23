#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class UpdateManager : MonoBehaviour
    {
        private static readonly IEqualityComparer<IUpdatable> s_comparer = EqualityComparer<IUpdatable>.Default;

        private static UpdateManager _instance;
        private static bool _spawned;

        private IUpdatable[] _updatables;
        private int _count;
        
        public static UpdateManager Instance
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
        
        public void Add(IUpdatable updatable)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            UpdateManager instance = Instance;
            AddIfAbsent(ref instance._updatables, ref instance._count, updatable, s_comparer);
        }
        
        public void Del(IUpdatable updatable)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            UpdateManager instance = Instance;
            Remove(ref instance._updatables, ref instance._count, updatable, s_comparer);
        }
        
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnUpdate(deltaTime);
        }
        
        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnFixedUpdate(deltaTime);
        }
        
        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnLateUpdate(deltaTime);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Resets internal spawn state when entering play mode to ensure clean singleton instantiation.
        /// </summary>
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            _spawned = false;
        }
#endif
        private static UpdateManager CreateInstance()
        {
            GameObject go = new GameObject();
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<UpdateManager>();
        }
    }
}
#endif