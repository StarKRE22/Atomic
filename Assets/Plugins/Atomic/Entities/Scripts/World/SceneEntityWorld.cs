#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity-compatible world manager for scene-based entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The specific type of scene entity this world manages. Must inherit from <see cref="SceneEntity"/>.</typeparam>
    /// <remarks>
    /// This component integrates with Unity’s lifecycle events (Awake, Start, OnEnable, etc.) to automatically
    /// manage entity spawning, enabling, updating, and cleanup. It wraps a runtime <see cref="EntityWorld{E}"/> instance internally.
    /// </remarks>
    /// <example>
    /// Attach this component to a GameObject in the scene to automatically scan and manage entities of type <typeparamref name="E"/>.
    /// </example>
    [AddComponentMenu("Atomic/Entities/Entity World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public class SceneEntityWorld<E> : MonoBehaviour, IEntityWorld<E> where E : SceneEntity
    {
        /// <inheritdoc />
        public event Action OnStateChanged
        {
            add => _world.OnStateChanged += value;
            remove => _world.OnStateChanged -= value;
        }
        
        /// <inheritdoc />
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }
        
        private readonly EntityWorld<E> _world = new();

#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
        [HideInPlayMode]
#endif
        [Tooltip("If this option is enabled then EntityWorld add all Entities on a scene on Awake()")]
        [SerializeField]
        private bool scanOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(scanOnAwake))]
#endif
        [Tooltip("If this option is enabled then EntityWorld scan inactive Entities on a scene also")]
        [SerializeField]
        private bool includeInactiveOnScan = true;
        
        [SerializeField, Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle (Start/OnEnable/OnDisable).")]
        private bool useUnityLifecycle = true;

        private bool isStarted;
        
        protected virtual void Awake()
        {
            if (!this.scanOnAwake)
                return;

#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive includeInactive = this.includeInactiveOnScan
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;

            E[] entities = FindObjectsByType<E>(includeInactive, FindObjectsSortMode.None);
#else
            SceneEntity[] entities = FindObjectsOfType<SceneEntity>(this.includeInactiveOnScan);
#endif
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                E entity = entities[i];
                if (!entity.Installed)
                    entity.Install();

                this.Add(entity);
            }
        }

        protected virtual void OnEnable()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                this.Enable();
                UpdateManager.Instance.Add(this);
            }
        }

        protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Spawn();
                this.Enable();
                UpdateManager.Instance.Add(this);

                this.isStarted = true;   
            }
        }

        protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                UpdateManager.Instance.Del(this);
                this.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                this.Despawn();
                this.isStarted = false;
            }
            
            this.Dispose();
        }
        
        public void Dispose() => _world?.Dispose();

        #region Entities

        public event Action<E> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        public event Action<E> OnRemoved
        {
            add => _world.OnRemoved += value;
            remove => _world.OnRemoved -= value;
        }

        public bool IsReadOnly => _world.IsReadOnly;

        int IEntityCollection<E>.Count => _world.Count;
        int ICollection<E>.Count => _world.Count;
        int IReadOnlyCollection<E>.Count => _world.Count;

        void ICollection<E>.Add(E item) => _world.Add(item);

        void ICollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);
        void IEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);
        void IReadOnlyEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        public bool Contains(E entity) => _world.Contains(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(E entity) => _world.Add(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Remove(E entity) => _world.Remove(entity);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _world.GetEnumerator();

        #endregion

        #region Lifecycle

        public event Action OnSpawned
        {
            add => _world.OnSpawned += value;
            remove => _world.OnSpawned -= value;
        }

        public event Action OnDespawned
        {
            add => _world.OnDespawned += value;
            remove => _world.OnDespawned -= value;
        }

        public event Action OnEnabled
        {
            add => _world.OnEnabled += value;
            remove => _world.OnEnabled -= value;
        }

        public event Action OnDisabled
        {
            add => _world.OnDisabled += value;
            remove => _world.OnDisabled -= value;
        }

        public event Action<float> OnUpdated
        {
            add => _world.OnUpdated += value;
            remove => _world.OnUpdated -= value;
        }

        public event Action<float> OnFixedUpdated
        {
            add => _world.OnFixedUpdated += value;
            remove => _world.OnFixedUpdated -= value;
        }

        public event Action<float> OnLateUpdated
        {
            add => _world.OnLateUpdated += value;
            remove => _world.OnLateUpdated -= value;
        }

        public bool Spawned => _world.Spawned;

        public bool Enabled => _world.Enabled;

#if ODIN_INSPECTOR
        [Title("Lifecycle")]
        [Button, HideInEditorMode]
#endif
        public void Spawn() => _world.Spawn();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Enable() => _world.Enable();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Disable() => _world.Disable();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Despawn() => _world.Despawn();

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnUpdate(float deltaTime) => _world.OnUpdate(deltaTime);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnFixedUpdate(float deltaTime) => _world.OnFixedUpdate(deltaTime);

#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void OnLateUpdate(float deltaTime) => _world.OnLateUpdate(deltaTime);

        #endregion

        #region Static

        /// <summary>
        /// Creates a new inactive <see cref="GameObject"/> with an attached <see cref="SceneEntityWorld{E}"/> component.
        /// </summary>
        /// <param name="name">The name of the GameObject and world instance.</param>
        /// <param name="scanEntities">If true, the world will scan the scene for entities on Awake.</param>
        /// <param name="entities">Optional entities to add immediately after creation.</param>
        /// <returns>The initialized <see cref="SceneEntityWorld{E}"/> instance.</returns>
        public static SceneEntityWorld<E> Create(
            string name = null,
            bool scanEntities = false,
            params E[] entities
        )
        {
            GameObject go = new GameObject(name);
            go.SetActive(false);

            SceneEntityWorld<E> world = go.AddComponent<SceneEntityWorld<E>>();
            world.Name = name;
            world.scanOnAwake = scanEntities;

            go.SetActive(true);
            world.AddRange(entities);
            return world;
        }

        #endregion
    }
}
#endif