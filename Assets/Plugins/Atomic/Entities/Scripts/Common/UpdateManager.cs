#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Internal MonoBehaviour singleton that manages and dispatches Unity update callbacks
    /// (<see cref="Update"/>, <see cref="FixedUpdate"/>, <see cref="LateUpdate"/>)
    /// to all registered <see cref="IUpdatable"/> instances.
    /// </summary>
    /// <remarks>
    /// The UpdateManager is instantiated automatically and hidden from the hierarchy. 
    /// It persists across scene loads and ensures centralized update handling.
    /// </remarks>
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    internal sealed class UpdateManager : MonoBehaviour
    {
        private static readonly IEqualityComparer<IUpdatable> s_comparer = EqualityComparer<IUpdatable>.Default;

        private static UpdateManager _instance;
        private static bool _spawned;

        private IUpdatable[] _updatables;
        private int _count;

        /// <summary>
        /// Gets the singleton instance of the UpdateManager.
        /// Automatically creates and registers itself if needed.
        /// </summary>
        internal static UpdateManager Instance
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
        /// <param name="updatable">The instance to register.</param>
        internal void Add(IUpdatable updatable)
        {
            if (updatable == null)
                return;

#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            UpdateManager instance = Instance;
            AddIfAbsent(ref instance._updatables, ref instance._count, updatable, s_comparer);
        }

        /// <summary>
        /// Unregisters a previously registered IUpdatable instance.
        /// </summary>
        /// <param name="updatable">The instance to unregister.</param>
        internal void Del(IUpdatable updatable)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            UpdateManager instance = Instance;
            Remove(ref instance._updatables, ref instance._count, updatable, s_comparer);
        }

        /// <summary>
        /// Invokes <see cref="IUpdatable.OnUpdate"/> on all registered instances.
        /// </summary>
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnUpdate(deltaTime);
        }

        /// <summary>
        /// Invokes <see cref="IUpdatable.OnFixedUpdate"/> on all registered instances.
        /// </summary>
        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnFixedUpdate(deltaTime);
        }

        /// <summary>
        /// Invokes <see cref="IUpdatable.OnLateUpdate"/> on all registered instances.
        /// </summary>
        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _count; i++)
                _updatables[i].OnLateUpdate(deltaTime);
        }

        /// <summary>
        /// Creates the hidden singleton GameObject and attaches UpdateManager component.
        /// </summary>
        /// <returns>The created UpdateManager instance.</returns>
        private static UpdateManager CreateInstance()
        {
            GameObject go = new GameObject("Update Manager");
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<UpdateManager>();
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