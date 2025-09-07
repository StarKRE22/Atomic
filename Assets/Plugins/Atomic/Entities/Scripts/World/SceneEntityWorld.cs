#if UNITY_5_3_OR_NEWER
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="SceneEntityWorld{SceneEntity}"/>.
    /// Represents a Unity scene-bound entity world operating on base <see cref="SceneEntity"/> types.
    /// </summary>
    /// <remarks>
    /// Use this when you don't need to specialize the world with a custom entity type.
    /// Useful for simple scenarios where only <see cref="SceneEntity"/> is involved.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity World")]
    [DisallowMultipleComponent]
    [DefaultExecutionOrder(-1000)]
    public class SceneEntityWorld : SceneEntityWorld<SceneEntity>
    {
        public static SceneEntityWorld Create(
            string name = null,
            bool scanEntities = true,
            bool useUnityLifecycle = true
        ) => Create<SceneEntityWorld>(name, scanEntities, useUnityLifecycle);
    }

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


#if ODIN_INSPECTOR
        [HideInPlayMode]
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [Tooltip("If this option is enabled then EntityWorld add all Entities on a scene on Awake()")]
        [SerializeField]
        private protected bool scanEntitiesOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(scanEntitiesOnAwake))]
#endif
        [Tooltip("If this option is enabled then EntityWorld scan inactive Entities on a scene also")]
        [SerializeField]
        private bool includeInactiveOnScan = true;

#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [Space]
        [SerializeField]
        [Tooltip("Enable automatic syncing with Unity MonoBehaviour lifecycle (Start/OnEnable/OnDisable).")]
        private protected bool useUnityLifecycle = true;
        
        [Tooltip("Should don't destroy if scene changed?")]
        [SerializeField]
        private bool _dontDestroyOnLoad;

        private readonly EntityWorld<E> _world = new();
        private bool isStarted;

        protected virtual void Awake()
        {
            if (this.scanEntitiesOnAwake)
                this.ScanEntities();
        }

        private void ScanEntities()
        {
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
                UpdateLoop.Instance.Register(this);
            }
        }

        protected virtual void Start()
        {
            if (this.useUnityLifecycle)
            {
                this.Enable();
                UpdateLoop.Instance.Register(this);
                this.isStarted = true;
            }
        }

        protected virtual void OnDisable()
        {
            if (this.useUnityLifecycle && this.isStarted)
            {
                UpdateLoop.Instance.Unregister(this);
                this.Disable();
            }
        }

        private void OnDestroy()
        {
            if (this.useUnityLifecycle) 
                this.Dispose();
        }

        /// <inheritdoc />
        public void Dispose() => _world?.Dispose();

        #region Entities

        /// <inheritdoc />
        public event Action<E> OnAdded
        {
            add => _world.OnAdded += value;
            remove => _world.OnAdded -= value;
        }

        /// <inheritdoc />
        public event Action<E> OnRemoved
        {
            add => _world.OnRemoved += value;
            remove => _world.OnRemoved -= value;
        }

        /// <inheritdoc />
        public bool IsReadOnly => _world.IsReadOnly;

        /// <inheritdoc />
        int IEntityCollection<E>.Count => _world.Count;

        /// <inheritdoc />
        int ICollection<E>.Count => _world.Count;

        /// <inheritdoc />
        int IReadOnlyCollection<E>.Count => _world.Count;

        /// <inheritdoc />
        void ICollection<E>.Add(E item) => _world.Add(item);

        /// <inheritdoc />
        void ICollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        void IEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc />
        void IReadOnlyEntityCollection<E>.CopyTo(E[] array, int arrayIndex) => _world.CopyTo(array, arrayIndex);

        /// <inheritdoc cref="IReadOnlyEntityCollection{E}.Contains" />
        public bool Contains(E entity) => _world.Contains(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Add(E entity) => _world.Add(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public bool Remove(E entity) => _world.Remove(entity);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Clear() => _world.Clear();

        /// <inheritdoc />
        public void CopyTo(ICollection<E> results) => _world.CopyTo(results);

        /// <inheritdoc />
        public IEnumerator<E> GetEnumerator() => _world.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => _world.GetEnumerator();

        #endregion

        #region Lifecycle
        
        /// <inheritdoc />
        public event Action OnEnabled
        {
            add => _world.OnEnabled += value;
            remove => _world.OnEnabled -= value;
        }

        /// <inheritdoc />
        public event Action OnDisabled
        {
            add => _world.OnDisabled += value;
            remove => _world.OnDisabled -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnUpdated
        {
            add => _world.OnUpdated += value;
            remove => _world.OnUpdated -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnFixedUpdated
        {
            add => _world.OnFixedUpdated += value;
            remove => _world.OnFixedUpdated -= value;
        }

        /// <inheritdoc />
        public event Action<float> OnLateUpdated
        {
            add => _world.OnLateUpdated += value;
            remove => _world.OnLateUpdated -= value;
        }

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public bool Enabled => _world.Enabled;
        
        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Enable() => _world.Enable();

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Disable() => _world.Disable();

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void Tick(float deltaTime) => _world.Tick(deltaTime);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void FixedTick(float deltaTime) => _world.FixedTick(deltaTime);

        /// <inheritdoc />
#if ODIN_INSPECTOR
        [Button, HideInEditorMode]
#endif
        public void LateTick(float deltaTime) => _world.LateTick(deltaTime);

        #endregion

        #region Static

        /// <summary>
        /// Creates a new inactive <see cref="GameObject"/> with an attached <see cref="SceneEntityWorld{E}"/> component.
        /// </summary>
        /// <param name="name">The name of the GameObject and world instance.</param>
        /// <param name="scanEntities">If true, the world will scan the scene for entities on Awake.</param>
        /// <param name="entities">Optional entities to add immediately after creation.</param>
        /// <returns>The initialized <see cref="SceneEntityWorld{E}"/> instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(
            string name = null,
            bool scanEntities = true,
            bool useUnityLifecycle = true
        )
            where T : SceneEntityWorld<E>
        {
            GameObject go = new GameObject();
            go.SetActive(false);

            T world = go.AddComponent<T>();
            world.Name = name;
            world.scanEntitiesOnAwake = scanEntities;
            world.useUnityLifecycle = useUnityLifecycle;

            go.SetActive(true);
            return world;
        }

        /// <summary>
        /// Destroys the <see cref="SceneEntityWorld{E}"/> and its associated GameObject after an optional delay.
        /// </summary>
        /// <typeparam name="E">The type of scene entity managed by the world.</typeparam>
        /// <param name="world">The world instance to destroy.</param>
        /// <param name="t">Optional delay in seconds before destruction. Default is 0.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(SceneEntityWorld<E> world, float t = 0)
        {
            if (world)
                GameObject.Destroy(world.gameObject, t);
        }

        #endregion
    }
}
#endif