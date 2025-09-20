#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.EntityUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Internal MonoBehaviour singleton that manages and dispatches Unity update callbacks
    /// (<see cref="Update"/>, <see cref="FixedUpdate"/>, <see cref="LateUpdate"/>)
    /// to all registered <see cref="ITickLifecycle"/> instances.
    /// </summary>
    /// <remarks>
    /// The UpdateManager is instantiated automatically and hidden from the hierarchy. 
    /// It persists across scene loads and ensures centralized update handling.
    /// </remarks>
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class TickableLoop : MonoBehaviour
    {
        private static readonly IEqualityComparer<ITickLifecycle> s_comparer = EqualityComparer<ITickLifecycle>.Default;

        private static TickableLoop _instance;
        private static bool _spawned;

        internal ITickLifecycle[] _updatables;
        private int _count;

        /// <summary>
        /// Gets the singleton instance of the UpdateManager.
        /// Automatically creates and registers itself if needed.
        /// </summary>
        internal static TickableLoop Instance
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

        /// <summary>
        /// Registers an IUpdatable instance for update callbacks.
        /// </summary>
        /// <param name="tickSource">The instance to register.</param>
        internal void Register(ITickLifecycle tickSource)
        {
            if (tickSource == null)
                return;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickableLoop instance = Instance;
            AddIfAbsent(ref instance._updatables, ref instance._count, tickSource, s_comparer);
        }

        /// <summary>
        /// Unregisters a previously registered IUpdatable instance.
        /// </summary>
        /// <param name="tickSource">The instance to unregister.</param>
        internal void Unregister(ITickLifecycle tickSource)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            TickableLoop instance = Instance;
            Remove(ref instance._updatables, ref instance._count, tickSource, s_comparer);
        }
        
        /// <summary>
        /// Invokes <see crefIUpdateSourceteeOnUpdate"/> on all registered instances.
        /// </summary>
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].Tick(deltaTime);
        }

        /// <summary>
        /// Invokes <see crefIUpdateSourceteeOnFixedUpdate"/> on all registered instances.
        /// </summary>
        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].FixedTick(deltaTime);
        }

        /// <summary>
        /// Invokes <see crefIUpdateSourceteeOnLateUpdate"/> on all registered instances.
        /// </summary>
        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].LateTick(deltaTime);
        }

        /// <summary>
        /// Creates the hidden singleton GameObject and attaches UpdateManager component.
        /// </summary>
        /// <returns>The created UpdateManager instance.</returns>
        private static TickableLoop CreateInstance()
        {
            GameObject go = new GameObject("Update Manager");
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<TickableLoop>();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Resets internal spawn flag when entering play mode in the Editor.
        /// Prevents stale singleton state between sessions.
        /// </summary>
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            _spawned = false;
        }
#endif
    }
}
#endif