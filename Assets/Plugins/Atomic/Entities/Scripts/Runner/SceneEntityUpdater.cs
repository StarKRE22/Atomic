#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Global MonoBehaviour-based updater that invokes update methods on all registered <see cref="IEntity"/> instances.
    /// </summary>
    /// <remarks>
    /// This class is instantiated lazily at runtime and persists across scene loads. It invokes <c>OnUpdate</c>,
    /// <c>OnFixedUpdate</c>, and <c>OnLateUpdate</c> for all tracked entities that are added via <see cref="Add"/>.
    /// Entities can be removed via <see cref="Del"/>. Only works during play mode.
    /// </remarks>
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    public sealed class SceneEntityUpdater : MonoBehaviour
    {
        private const string GAME_OBJECT_NAME = "Entity Updater";

        private static readonly IEqualityComparer<IEntity> s_entityComparer = EqualityComparer<IEntity>.Default;

        private static SceneEntityUpdater _instance;
        private static bool _spawned;

        private IEntity[] _entities;
        private int _entityCount;

        /// <summary>
        /// Gets the global singleton instance. Automatically created if accessed for the first time.
        /// </summary>
        public static SceneEntityUpdater Instance
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
        /// Gets the number of registered entities currently being updated.
        /// </summary>
        public int EntityCount => _entityCount;

        /// <summary>
        /// Adds an <see cref="IEntity"/> to the global updater, allowing it to receive update events.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <remarks>
        /// Duplicate entities are ignored. Only works during play mode in the Unity Editor.
        /// </remarks> 
        public void Add(IEntity entity)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            SceneEntityUpdater instance = Instance;
            AddIfAbsent(ref instance._entities, ref instance._entityCount, entity, s_entityComparer);
        }

        /// <summary>
        /// Removes an <see cref="IEntity"/> from the global updater, stopping further update invocations.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <remarks>
        /// Only works during play mode in the Unity Editor.
        /// </remarks>
        public void Del(IEntity entity)
        {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying)
                return;
#endif
            SceneEntityUpdater instance = Instance;
            Remove(ref instance._entities, ref instance._entityCount, entity, s_entityComparer);
        }
        
        /// <summary>
        /// Invokes <see cref="IEntity.OnUpdate"/> for all registered entities once per frame.
        /// </summary>
        private void Update()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnUpdate(deltaTime);
        }

        /// <summary>
        /// Invokes <see cref="IEntity.OnFixedUpdate"/> for all registered entities on the fixed timestep.
        /// </summary>
        private void FixedUpdate()
        {
            float deltaTime = Time.fixedDeltaTime;
            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnFixedUpdate(deltaTime);
        }

        /// <summary>
        /// Invokes <see cref="IEntity.OnLateUpdate"/> for all registered entities at the end of the frame.
        /// </summary>
        private void LateUpdate()
        {
            float deltaTime = Time.deltaTime;
            for (int i = 0; i < _entityCount; i++)
                _entities[i].OnLateUpdate(deltaTime);
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
        private static SceneEntityUpdater CreateInstance()
        {
            GameObject go = new GameObject(GAME_OBJECT_NAME);
            go.hideFlags = HideFlags.HideAndDontSave;
            DontDestroyOnLoad(go);
            return go.AddComponent<SceneEntityUpdater>();
        }
    }
}
#endif