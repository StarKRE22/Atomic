#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class TickableManager : MonoBehaviour
    {
        private static readonly IEqualityComparer<ITickSource> s_comparer = EqualityComparer<ITickSource>.Default;

        private static TickableManager _instance;
        private static bool _spawned;

        internal ITickSource[] _sources;
        private int _count;

        internal static TickableManager Instance
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

        internal void Register(ITickSource tickSource)
        {
            if (tickSource == null)
                return;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickableManager instance = Instance;
            AddIfAbsent(ref instance._sources, ref instance._count, tickSource, s_comparer);
        }

        internal void Unregister(ITickSource tickSource)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickableManager instance = Instance;
            Remove(ref instance._sources, ref instance._count, tickSource, s_comparer);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _sources[i].Tick(deltaTime);
        }

        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _count; i++)
                _sources[i].FixedTick(deltaTime);
        }

        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _sources[i].LateTick(deltaTime);
        }

        private static TickableManager CreateInstance()
        {
            GameObject go = new GameObject(nameof(TickableManager));
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<TickableManager>();
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