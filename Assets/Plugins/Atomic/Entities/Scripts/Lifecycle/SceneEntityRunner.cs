#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;
using static Atomic.Entities.InternalUtils;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// A MonoBehaviour that manages a list of <see cref="SceneEntity"/> components,
    /// initializing and enabling them at runtime, and registering them with the global <see cref="SceneEntityUpdater"/>.
    /// </summary>
    /// <remarks>
    /// This component is useful for batch-controlling multiple <see cref="SceneEntity"/> instances defined in the scene.
    /// Entities are automatically initialized and registered on <see cref="Start"/>, and cleaned up on <see cref="OnDestroy"/>.
    /// </remarks>
    [AddComponentMenu("Atomic/Entities/Entity Runner")]
    [DisallowMultipleComponent]
    public sealed class SceneEntityRunner : MonoBehaviour
    {
        private static readonly IEqualityComparer<SceneEntity> s_entityComparer = EqualityComparer<SceneEntity>.Default;

        [SerializeField]
        [Tooltip("Scene entities that will be initialized, enabled and registered at runtime.")]
        private SceneEntity[] _entities;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        private int _entityCount;

        private bool started;

        /// <summary>
        /// Initializes the entity count based on the serialized array.
        /// </summary>
        private void Awake()
        {
            _entityCount = _entities.Length;
        }

        /// <summary>
        /// Initializes and enables all referenced entities.
        /// Registers them with the <see cref="SceneEntityUpdater"/>.
        /// </summary>
        private void Start()
        {
            for (int i = 0; i < _entityCount; i++)
            {
                SceneEntity entity = _entities[i];
                if (!entity)
                {
                    Debug.LogWarning("SceneEntityRunner: Ops: Detected null entity!", this);
                    continue;
                }

                entity.Init();
                entity.Enable();
                SceneEntityUpdater.Instance.Add(entity);
            }

            this.started = true;
        }

        /// <summary>
        /// Re-enables all entities when the runner is re-enabled in the scene.
        /// </summary>
        private void OnEnable()
        {
            if (!this.started)
                return;

            for (int i = 0; i < _entityCount; i++)
            {
                SceneEntity entity = _entities[i];
                if (entity)
                {
                    entity.Enable();
                    SceneEntityUpdater.Instance.Add(entity);
                }
            }
        }

        /// <summary>
        /// Disables all tracked entities and unregisters them when this runner is disabled.
        /// </summary>
        private void OnDisable()
        {
            if (!this.started)
                return;

            for (int i = 0; i < _entityCount; i++)
            {
                SceneEntity entity = this._entities[i];
                if (entity)
                {
                    entity.Disable();
                    SceneEntityUpdater.Instance.Del(entity);
                }
            }
        }

        /// <summary>
        /// Disposes all entities and unregisters them from the global updater on destruction.
        /// </summary>
        private void OnDestroy()
        {
            if (!this.started)
                return;

            for (int i = 0, count = _entityCount; i < count; i++)
            {
                SceneEntity entity = _entities[i];
                if (entity)
                {
                    entity.Denit();
                    SceneEntityUpdater.Instance.Del(entity);
                }
            }
        }

        /// <summary>
        /// Automatically populates the array of entities from child objects.
        /// </summary>
        private void Reset()
        {
            _entities = this.GetComponentsInChildren<SceneEntity>();
            _entityCount = _entities.Length;
        }

        /// <summary>
        /// Adds a <see cref="SceneEntity"/> to the runner and immediately initializes and enables it if the runner has started.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>True if the entity was added successfully, false if it was null or already present.</returns>
        public bool Add(SceneEntity entity)
        {
            if (entity == null)
                return false;

            if (!AddIfAbsent(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (this.started)
            {
                entity.Init();
                entity.Enable();
                SceneEntityUpdater.Instance.Add(entity);
            }

            return true;
        }

        /// <summary>
        /// Removes a <see cref="SceneEntity"/> from the runner and optionally disables and disposes it.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <returns>True if the entity was found and removed, false otherwise.</returns>
        public bool Del(SceneEntity entity)
        {
            if (!entity)
                return false;

            if (!Remove(ref _entities, ref _entityCount, entity, s_entityComparer))
                return false;

            if (this.started)
            {
                entity.Disable();
                entity.Denit();
                SceneEntityUpdater.Instance.Del(entity);
            }

            return true;
        }
    }
}
#endif