using System;
using Atomic.Elements;
using Atomic.Entities;
using Unity.Profiling;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class RotationViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        private static readonly ProfilerMarker s_marker = new("RotationViewBehaviour.OnRotationChanged");
        
        [SerializeField]
        private Transform _transform;

        private IReactiveValue<Quaternion> _rotation;
        private Subscription<Quaternion> _subscription;

        public void Init(IGameEntity entity)
        {
            _rotation = entity.GetRotation();
            _subscription = _rotation.Observe(this.OnRotationChanged);
        }

        public void Dispose(IEntity entity)
        {
            _subscription.Dispose();
        }

        private void OnRotationChanged(Quaternion rotation)
        {
            using (s_marker.Auto())
                _transform.rotation = rotation;
        }
    }
}