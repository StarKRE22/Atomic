#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A Unity MonoBehaviour-based entity pool for scene-bound entities of type <typeparamref name="E"/>.
    /// </summary>
    /// <typeparam name="E">The type of entity managed by this pool. Must inherit from <see cref="MonoEntity"/>.</typeparam>
    /// <typeparam name="P">The concrete prefab type used to instantiate entities. Must inherit from <see cref="MonoEntity"/> and implement <typeparamref name="E"/>.</typeparam>
    /// <remarks>
    /// This pool uses a prefab to instantiate entities and manages their reuse via a stack.
    /// Entities are activated/deactivated on rent/return, and can be preloaded using <see cref="Init(int)"/>.
    /// </remarks>
    //TODO: Add Strategies FixedPool
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Entities/Pooling/MonoEntityPool%601.md")]
    public abstract class MonoEntityPool<E, P> : MonoBehaviour, IEntityPool<E>
        where E : IEntity
        where P : MonoEntity, E
    {
#if ODIN_INSPECTOR
        [GUIColor(0f, 0.83f, 1f)]
#endif
        [SerializeField]
        [Tooltip("Whether to automatically initialize the pool in Awake().")]
        private bool _initOnAwake = true;

        [SerializeField]
        [Tooltip("Allow returning entities that were not rented from this pool.")]
        internal bool _acceptExternalReturns = true;

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
        private P _prefab;

        [SerializeField]
        [Tooltip("Optional container transform under which pooled entities are parented. Defaults to this GameObject.")]
        private Transform _container;

        [Tooltip("Should don't destroy if scene changed?")]
        [SerializeField]
        private bool _dontDestroyOnLoad;

        [Space]
        [Tooltip("Determines how the pool expands when empty.\n" +
                 "ExpandByOne: Creates one new entity per request.\n" +
                 "ExpandByDoubling: Doubles the current pooled count (e.g. 10 → 20).\n" +
                 "NoExpand: Throws an exception when the pool is empty.")]
        [SerializeField]
        private ExpandMode _expandMode = ExpandMode.ExpandByOne;

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly Stack<P> _pooledEntities = new();

#if ODIN_INSPECTOR
        [FoldoutGroup("Debug")]
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        internal readonly HashSet<P> _rentEntities = new();

        /// <summary>
        /// Initializes the pool when the GameObject is activated, if <see cref="_initOnAwake"/> is <c>true</c>.
        /// </summary>
        protected virtual void Awake()
        {
            if (_container == null)
                _container = this.transform;

            if (_initOnAwake)
                this.Init(_initialCount);

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(this.gameObject);
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
        
        public async ValueTask InitAsync(int initialCount)
        {
            if (_prefab == null)
                throw new NullReferenceException($"[EntityPool] Prefab is null in {name}");

            if (_container == null)
                _container = this.transform;

            if (initialCount <= 0)
                return;

            P[] entities = await MonoEntity.CreateAsync(_prefab, initialCount, _container);
            foreach (P entity in entities)
            {
                this.OnCreate(entity);
                _pooledEntities.Push(entity);
            }
        }

        /// <summary>
        /// Rents (activates) an entity from the pool. If the pool is empty, expansion behavior
        /// depends on <see cref="_expandMode"/>.
        /// </summary>
        /// <returns>The rented entity.</returns>
        /// <exception cref="InvalidOperationException">Thrown when <see cref="ExpandMode.NoExpand"/> is set and the pool is empty.</exception>
        public E Rent()
        {
            if (!_pooledEntities.TryPop(out P entity))
                entity = this.Expand();

            _rentEntities.Add(entity);
            this.OnRent(entity);
            return entity;
        }

        private P Expand()
        {
            switch (_expandMode)
            {
                case ExpandMode.NoExpand:
                    throw new InvalidOperationException(
                        $"[EntityPool] Pool '{name}' is empty and ExpandMode is NoExpand. " +
                        $"Pre-instantiate more entities via Init() or switch to ExpandByOne/ExpandByDoubling.");

                case ExpandMode.ExpandByDoubling:
                    int count = _rentEntities.Count > 0 ? _rentEntities.Count : 1;
                    this.CreateEntities(count);
                    _pooledEntities.TryPop(out P doubled);
                    return doubled;

                case ExpandMode.ExpandByOne:
                default:
                    return this.CreateEntity();
            }
        }

        private void CreateEntities(int count)
        {
            for (int i = 0; i < count; i++)
            {
                P entity = this.CreateEntity();
                _pooledEntities.Push(entity);
            }
        }

        /// <summary>
        /// Returns (deactivates) an entity to the pool.
        /// </summary>
        /// <param name="entity">The entity to return. Must have been previously rented.</param>
        public void Return(E entity)
        {
            P sceneEntity = MonoEntity.Cast<P>(entity);
            if (_rentEntities.Remove(sceneEntity) || _acceptExternalReturns)
            {
                this.OnReturn(sceneEntity);
                _pooledEntities.Push(sceneEntity);
            }
            else
            {
                Debug.LogWarning($"[EntityPool] Attempted to return untracked entity: {entity}", sceneEntity);
            }
        }

        /// <summary>
        /// Disposes all pooled and rent entities by destroying them and clearing the internal pool.
        /// </summary>
        public virtual void Dispose()
        {
            foreach (P entity in _pooledEntities)
            {
                this.OnDispose(entity);
                MonoEntity.Destroy(entity);
            }

            foreach (P entity in _rentEntities)
            {
                this.OnDispose(entity);
                MonoEntity.Destroy(entity);
            }

            _pooledEntities.Clear();
            _rentEntities.Clear();
        }

        /// <summary>
        /// Called when a new entity instance is created.
        /// </summary>
        /// <param name="entity">The newly created entity.</param>
        protected virtual void OnCreate(P entity) =>
            entity.gameObject.SetActive(false);

        /// <summary>
        /// Called when a pooled entity is being permanently destroyed during disposal.
        /// </summary>
        /// <param name="entity">The entity being destroyed.</param>
        protected virtual void OnDispose(P entity)
        {
        }

        /// <summary>
        /// Called when an entity is rented from the pool.
        /// </summary>
        /// <param name="entity">The entity being rented.</param>
        protected virtual void OnRent(P entity) =>
            entity.gameObject.SetActive(true);

        /// <summary>
        /// Called when an entity is returned to the pool.
        /// </summary>
        /// <param name="entity">The entity being returned.</param>
        protected virtual void OnReturn(P entity)
        {
            entity.gameObject.SetActive(false);
            entity.transform.SetParent(_container);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private P CreateEntity()
        {
            P entity = MonoEntity.Create(_prefab, _container);
            this.OnCreate(entity);
            return entity;
        }

        #region Static

        /// <summary>
        /// Arguments used to create a new <see cref="MonoEntityPool{E}"/> instance.
        /// </summary>
        [Serializable]
        public struct CreateArgs
        {
            [Tooltip("The name of the GameObject that will host the pool")]
            public string name;

            [Tooltip("The prefab used to instantiate pooled entities")]
            public P prefab;

            [Tooltip(
                "Optional transform under which pooled entities will be parented. Defaults to the pool's GameObject if null")]
            public Transform container;

            [Tooltip("Whether the pool should automatically initialize in Awake()")]
            public bool initOnAwake;

            [Tooltip("Number of entities to pre-instantiate in the pool")]
            public int initialCount;
        }

        /// <summary>
        /// Creates a new instance of <typeparamref name="TPool"/> (a <see cref="MonoEntityPool{E}"/>) in the scene.
        /// </summary>
        /// <typeparam name="TPool">The type of scene entity pool to create.</typeparam>
        /// <param name="args">Initialization parameters encapsulated in <see cref="CreateArgs"/>.</param>
        /// <returns>A new instance of <typeparamref name="TPool"/> added to a new GameObject in the scene.</returns>
        /// <example>
        /// <code>
        /// var poolArgs = new MonoEntityPool<E>.CreateArgs
        /// {
        /// name = "EnemyPool",
        /// prefab = enemyPrefab,
        /// container = parentTransform,
        /// initOnAwake = true,
        /// initialCount = 10
        /// };
        /// MonoEntityPool<EnemyEntity> pool = MonoEntityPool<EnemyEntity>.Create<MonoEntityPool<EnemyEntity>>(poolArgs);
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TPool Create<TPool>(in CreateArgs args) where TPool : MonoEntityPool<E, P>
        {
            var gameObject = new GameObject(args.name);
            gameObject.SetActive(false);
            TPool pool = gameObject.AddComponent<TPool>();
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
        /// MonoEntityPool<EnemyEntity> pool = ...;
        /// MonoEntityPool<EnemyEntity>.Destroy(pool, 1.0f); // Dispose and destroy after 1 second
        /// </code>
        /// </example>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Destroy(MonoEntityPool<E, P> pool, float t = 0)
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