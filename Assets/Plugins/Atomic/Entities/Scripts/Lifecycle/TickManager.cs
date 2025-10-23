#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class TickManager : MonoBehaviour
    {
        private static readonly IEqualityComparer<ITickLifecycle> s_comparer = EqualityComparer<ITickLifecycle>.Default;

        private static TickManager _instance;
        private static bool _spawned;

        internal ITickLifecycle[] _updatables;
        private int _count;

        internal static TickManager Instance
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

        internal void Register(ITickLifecycle tickSource)
        {
            if (tickSource == null)
                return;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickManager instance = Instance;
            AddIfAbsent(ref instance._updatables, ref instance._count, tickSource, s_comparer);
        }

        internal void Unregister(ITickLifecycle tickSource)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickManager instance = Instance;
            Remove(ref instance._updatables, ref instance._count, tickSource, s_comparer);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].Tick(deltaTime);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].FixedTick(deltaTime);
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].LateTick(deltaTime);
        }

        private static TickManager CreateInstance()
        {
            GameObject go = new GameObject(nameof(TickManager));
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<TickManager>();
        }

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            _spawned = false;
        }
#endif
    }
}
#endif