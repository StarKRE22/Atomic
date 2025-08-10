using Atomic.Elements;
using Atomic.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletMoveBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private IVariable<Vector3> _position;
        private IValue<Quaternion> _rotation;
        private IValue<float> _moveSpeed;

        public void OnSpawn(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            MovementUseCase.MovementStep(
                _position.Value,
                _rotation.Value * Vector3.forward,
                _moveSpeed.Value,
                deltaTime,
                out float3 position
            );

            _position.Value = position;
        }
    }
}