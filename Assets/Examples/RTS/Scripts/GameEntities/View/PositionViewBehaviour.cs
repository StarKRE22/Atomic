using System;
using Atomic.Elements;
using Atomic.Entities;
using Unity.Profiling;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class PositionViewBehaviour : IEntityInit<IGameEntity>, IEntityDispose
    {
        private static readonly ProfilerMarker s_marker = new("PositionViewBehaviour.OnPositionChanged");

        [SerializeField]
        private Transform _transform;

        [SerializeField, Min(0f)]
        private float _positionThreshold = 0.001f; // настраиваемая дельта

        private float _sqrThreshold;

        private IReactiveValue<Vector3> _position;
        private Subscription<Vector3> _subscription;

        private Vector3 _lastPosition;

        public void Init(IGameEntity entity)
        {
            _sqrThreshold = _positionThreshold * _positionThreshold;

            _position = entity.GetPosition();
            _lastPosition = _transform.position;

            _subscription = _position.Observe(OnPositionChanged);
        }

        public void Dispose(IEntity entity)
        {
            _subscription.Dispose();
        }

        private void OnPositionChanged(Vector3 position)
        {
            if (Vector3.SqrMagnitude(position - _lastPosition) <= _sqrThreshold)
                return;

            using (s_marker.Auto())
            {
                _transform.position = position;
            }
            
            _lastPosition = position;
        }
    }
}