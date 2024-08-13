using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace GameExample.Engine
{
    public sealed class TransformBehaviour : IEntityInit, IEntityUpdate
    {
        private Transform _transform;
        private IReactiveValue<float3> _position;
        private IReactiveValue<quaternion> _rotation;

        //Calls like a MonoBehaviour.Start() 
        public void Init(IEntity entity)
        {
            _transform = entity.GetTransform();
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            
            _transform.SetPositionAndRotation(_position.Value, _rotation.Value);
        }

        //Calls like a MonoBehaviour.Update() 
        public void OnUpdate(IEntity entity, float deltaTime)
        {
            _transform.SetPositionAndRotation(_position.Value, _rotation.Value);
        }
    }
}