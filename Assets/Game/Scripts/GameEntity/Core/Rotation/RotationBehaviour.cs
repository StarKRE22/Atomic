using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace SampleGame
{
    public sealed class RotationBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private Transform _transform;
        private IValue<float> _rotationSpeed;
        private IValue<Vector3> _rotationDirection;

        public void OnSpawn(IGameEntity entity)
        {
            _transform = entity.GetTransform();
            _rotationSpeed = entity.GetRotationSpeed();
            _rotationDirection = entity.GetRotationDirection();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            Quaternion current = _transform.rotation;
            Quaternion target = Quaternion.LookRotation(_rotationDirection.Value);
            float angle = Quaternion.Angle(current, target);
            
            float maxStep = _rotationSpeed.Value * deltaTime;
            float t = Mathf.Clamp01(maxStep / angle);
            
            Quaternion next = Quaternion.Slerp(current, target, t);
            _transform.rotation = next;
        }
    }
}