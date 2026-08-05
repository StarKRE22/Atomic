using System;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using Unity.Profiling;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for entity systems that process entities from a source collection.
    /// </summary>
    /// <typeparam name="E">Type of entity processed by the system.</typeparam>
    public abstract class EntitySystemBase<E> : IDisposable
        where E : IEntity
    {
        /// <summary>
        /// Configuration for the entity system.
        /// </summary>
        [Serializable]
        public class Settings
        {
            /// <summary>
            /// Maximum amount of time (in seconds) the system should spend updating per frame.
            /// Used to automatically adjust the processing batch size.
            /// </summary>
            [SerializeField]
            public float frameBudget = 0.03f;

            /// <summary>
            /// Parameters controlling adaptive batch size adjustment.
            /// </summary>
            [SerializeField]
            public AdaptiveBatching batching = new();

            /// <summary>
            /// Settings for adaptive batch size scaling.
            /// </summary>
            [Serializable]
            public sealed class AdaptiveBatching
            {
                /// <summary>
                /// Minimum allowed batch size.
                /// </summary>
                public int minSize = 1024;

                /// <summary>
                /// Maximum allowed batch size.
                /// </summary>
                public int maxSize = 2048;

                /// <summary>
                /// Divisor applied to the current batch size when the frame budget is exceeded.
                /// </summary>
                public int scaleDown = 2;

                /// <summary>
                /// Amount by which the batch size increases when the frame budget is not exceeded.
                /// </summary>
                public int stepUp = 256;
            }
        }

#if ODIN_INSPECTOR
        [Title("Debug")]
        [ShowInInspector, HideInEditorMode]
#endif
        private Settings _settings;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private int _batchSize;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private bool _enabled;

#if ENABLE_PROFILER
        private ProfilerMarker _marker;
#endif

        /// <summary>
        /// Read-only collection of entities processed by this system.
        /// </summary>
        private protected readonly IReadOnlyEntityCollection<E> _source;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySystemBase{E}"/> class.
        /// </summary>
        /// <param name="source">Source collection of entities.</param>
        /// <param name="settings">System configuration.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="settings"/> is <c>null</c>.
        /// </exception>
        protected EntitySystemBase(IReadOnlyEntityCollection<E> source, Settings settings)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

#if ENABLE_PROFILER
            _marker = new ProfilerMarker(this.GetType().Name + ".Update");
#endif
        }

        /// <summary>
        /// Enables the system, subscribes to entity collection events,
        /// and registers all existing entities.
        /// </summary>
        public void Enable()
        {
            if (_enabled)
                return;

            _enabled = true;

            foreach (E entity in _source)
                this.OnAddEntity(entity);

            _source.OnAdded += this.OnAddEntity;
            _source.OnRemoved += this.OnRemoveEntity;

            this.OnEnable();
        }

        /// <summary>
        /// Called after the system has been enabled.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnEnable()
        {
        }

        /// <summary>
        /// Disables the system, unsubscribes from entity collection events,
        /// and unregisters all tracked entities.
        /// </summary>
        public void Disable()
        {
            if (!_enabled)
                return;

            _enabled = false;

            this.OnDisable();

            _source.OnAdded -= this.OnAddEntity;
            _source.OnRemoved -= this.OnRemoveEntity;

            foreach (E entity in _source)
                this.OnRemoveEntity(entity);
        }

        /// <summary>
        /// Called before the system is disabled.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDisable()
        {
        }

        /// <summary>
        /// Updates the system.
        /// The execution time is measured and the processing batch size
        /// is automatically adjusted to fit within the configured frame budget.
        /// </summary>
        /// <param name="deltaTime">Time elapsed since the previous update.</param>
        public void Update(float deltaTime)
        {
            if (!_enabled)
                return;

            long start = InternalUtils.GetTimestamp();

#if ENABLE_PROFILER
            using (_marker.Auto())
#endif
                this.OnUpdate(_batchSize, deltaTime);

            long end = InternalUtils.GetTimestamp();
            float frameSize = (end - start) * InternalUtils.DeltaTick;

            _batchSize = frameSize > _settings.frameBudget
                ? Math.Max(_batchSize / _settings.batching.scaleDown, _settings.batching.minSize)
                : Math.Min(_batchSize + _settings.batching.stepUp, _settings.batching.maxSize);
        }

        /// <summary>
        /// Performs the system update.
        /// </summary>
        /// <param name="batchSize">Maximum number of entities to process during this update.</param>
        /// <param name="deltaTime">Time elapsed since the previous update.</param>
        protected abstract void OnUpdate(int batchSize, float deltaTime);

        /// <summary>
        /// Called when an entity is added to the source collection
        /// or when the system is enabled.
        /// </summary>
        /// <param name="entity">The added entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnAddEntity(E entity)
        {
        }

        /// <summary>
        /// Called when an entity is removed from the source collection
        /// or when the system is disabled.
        /// </summary>
        /// <param name="entity">The removed entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnRemoveEntity(E entity)
        {
        }

        /// <summary>
        /// Releases all resources used by the system.
        /// </summary>
        public abstract void Dispose();
    }
}
