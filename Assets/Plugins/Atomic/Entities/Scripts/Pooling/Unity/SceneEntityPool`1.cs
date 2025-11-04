#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity MonoBehaviour-based entity pool for scene-bound entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by this pool. Must inherit from <see cref="SceneEntity"/>.</typeparam>
    /// <remarks>
    /// This pool uses a prefab to instantiate entities and manages their reuse via a stack.
    /// Entities are activated/deactivated on rent/return, and can be preloaded using <see cref="Init(int)"/>.
    /// </remarks>
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Pooling/SceneEntityPool%601.md")]
    public abstract class SceneEntityPool<E> : MonoBehaviour, IEntityPool<E> where E : SceneEntity
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [SerializeField]
        [Tooltip("Whether to automatically initialize the pool in Awake().")]
        private bool _initOnAwake = true;

#if ODIN_INSPECTOR
        [ShowIf(nameof(_initOnAwake))]
        [MinValue(0)]
#else
        [Min(0)]
#endif
        [SerializeField]
        [Tooltip("Initial number of entities to pre-instantiate in the pool on Awake.")]
        private int _initialCount;

        [Space]
        [SerializeField]
        [Tooltip("The prefab used to create pooled entity instances.")]
        private E _prefab;

        [SerializeField]
        [Tooltip("Optional container transform under which pooled entities are parented. Defaults to this GameObject.")]
        private Transform _container;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly Stack<E> _pooledEntities = new();

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly HashSet<E> _rentEntities = new();

        /// <summary>
        /// Initializes the pool when the GameObject is activated, if <see cref="_initOnAwake"/> is <c>true</c>.
        /// </summary>
        protected virtual void Awake()
        {
            if (_container == null)
                _container = this.transform;

            if (_initOnAwake)
                this.Init(_initialCount);
        }

        protected virtual void Reset()
        {
            _container = this.transform;
        }

        /// <summary>
        /// Initializes the pool by pre-instantiating the specified number of entities.
        /// </summary>
        /// <param name="initialCount">The number of entities to create and store in the pool.</param>
        public void Init(int initialCount)
        {
            for (int i = 0; i < initialCount; i++)
                _pooledEntities.Push(this.CreateEntity());
        }

        /// <summary>
        /// Rents (activates) an entity from the pool. If the pool is empty, a new instance is created.
        /// </summary>
        /// <returns>The rented entity.</returns>
        public E Rent()
        {
            if (!_pooledEntities.TryPop(out E entity))
                entity = this.CreateEntity();

            _rentEntities.Add(entity);
            this.OnRent(entity);
            return entity;
        }

        /// <summary>
        /// Returns (deactivates) an entity to the pool.
        /// </summary>
        /// <param name="entity">The entity to return. Must have been previously rented.</param>
        public void Return(E entity)
        {
            if (_rentEntities.Remove(entity))
            {
                this.OnReturn(entity);
                _pooledEntities.Push(entity);
            }
            else
            {
                Debug.LogWarning($"[EntityPool] Attempted to return untracked entity: {entity}", entity);
            }
        }

        /// <summary>
        /// Disposes all pooled and rent entities by destroying them and clearing the internal pool.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (E entity in _pooledEntities)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            foreach (E entity in _rentEntities)
            {
                this.OnDispose(entity);
                Destroy(entity);
            }

            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Called when a new entity instance is created.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(E entity) =>
            entity.gameObject.SetActive(false);

        /// <summary>
        /// Called when a pooled entity is being permanently destroyed during disposal.
        /// </summary>
        /// <param name="entity">The entity being destroyed.</param>
        protected virtual void OnDispose(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// </summary>
        /// <param name="entity">The entity being rented.</param>
        protected virtual void OnRent(E entity) =>
            entity.gameObject.SetActive(true);

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// </summary>
        /// <param name="entity">The entity being returned.</param>
        protected virtual void OnReturn(E entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(_container);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private E CreateEntity()
        {
            E entity = SceneEntity.Create(_prefab, _container);
            this.OnCreate(entity);
            return entity;
        }

        #region Static

        /// <summary>
        /// Arguments used to create a new <see cref="SceneEntityPool{E}"/> instance.
        /// </summary>
        [Serializable]
        public struct CreateArgs
        {
            [Tooltip("The name of the GameObject that will host the pool")]
            public string name;

            [Tooltip("The prefab used to instantiate pooled entities")]
            public E prefab;

            [Tooltip(
                "Optional transform under which pooled entities will be parented. Defaults to the pool's GameObject if null")]
            public Transform container;

            [Tooltip("Whether the pool should automatically initialize in Awake()")]
            public bool initOnAwake;

            [Tooltip("Number of entities to pre-instantiate in the pool")]
            public int initialCount;
        }

        /// <summary>
        /// Creates a new instance of <typeparamref name="T"/> (a <see cref="SceneEntityPool{E}"/>) in the scene.
        /// </summary>
        /// <typeparam name="T">The type of scene entity pool to create.</typeparam>
        /// <param name="args">Initialization parameters encapsulated in <see cref="CreateArgs"/>.</param>
        /// <returns>A new instance of <typeparamref name="T"/> added to a new GameObject in the scene.</returns>
        /// <example>
        /// <code>
        /// var poolArgs = new SceneEntityPool<E>.CreateArgs
        /// {
        /// name = "EnemyPool",
        /// prefab = enemyPrefab,
        /// container = parentTransform,
        /// initOnAwake = true,
        /// initialCount = 10
        /// };
        /// SceneEntityPool<EnemyEntity> pool = SceneEntityPool<EnemyEntity>.Create<SceneEntityPool<EnemyEntity>>(poolArgs);
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Create<T>(in CreateArgs args) where T : SceneEntityPool<E>
        {
            var gameObject = new GameObject(args.name);
            gameObject.SetActive(false);
            T pool = gameObject.AddComponent<T>();
            pool._prefab = args.prefab;
            pool._container = args.container;
            pool._initOnAwake = args.initOnAwake;
            pool._initialCount = args.initialCount;
            gameObject.SetActive(true);
            return pool;
        }

        /// <summary>
        /// Disposes a scene entity pool and destroys its GameObject after an optional delay.
        /// </summary>
        /// <param name="pool">The pool instance to dispose and destroy.</param>
        /// <param name="t">Optional delay (in seconds) before destroying the pool's GameObject. Defaults to 0.</param>
        /// <example>
        /// <code>
        /// SceneEntityPool<EnemyEntity> pool = ...;
        /// SceneEntityPool<EnemyEntity>.Destroy(pool, 1.0f); // Dispose and destroy after 1 second
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(SceneEntityPool<E> pool, float t = 0)
        {
            if (pool)
            {
                pool.Dispose();
                Destroy(pool.gameObject, t);
            }
        }

        #endregion
    }
}
#endif