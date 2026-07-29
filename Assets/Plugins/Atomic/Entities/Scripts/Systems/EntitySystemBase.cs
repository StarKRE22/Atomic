using System;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using Unity.Profiling;
using UnityEngine;

namespace Atomic.Entities
{
    public abstract class EntitySystemBase<E> : IDisposable
        where E : IEntity
    {
        [Serializable]
        public class Settings
        {
            [SerializeField]
            public float frameBudget = 0.03f;

            [SerializeField]
            public AdaptiveBatching batching = new();
            
            [Serializable]
            public sealed class AdaptiveBatching
            {
                public int minSize = 1024;
                public int maxSize = 2048;
                public int scaleDown = 2;
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

        private protected readonly IReadOnlyEntityCollection<E> _source;

        protected EntitySystemBase(IReadOnlyEntityCollection<E> source, Settings settings)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
#if ENABLE_PROFILER
            _marker = new ProfilerMarker(this.GetType().Name + ".Update");
#endif
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnEnable()
        {
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnDisable()
        {
        }

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

        protected abstract void OnUpdate(int batchSize, float deltaTime);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnAddEntity(E entity)
        {
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual void OnRemoveEntity(E entity)
        {
        }

        public abstract void Dispose();
    }
}