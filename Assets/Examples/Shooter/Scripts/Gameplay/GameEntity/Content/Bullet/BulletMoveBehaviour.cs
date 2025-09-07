using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletMoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedUpdate
    {
        private IVariable<Vector3> _position;
        private IValue<Quaternion> _rotation;
        private IValue<float> _moveSpeed;

        public void Init(IGameEntity entity)
        {
            _position = entity.GetPosition();
            _rotation = entity.GetRotation();
            _moveSpeed = entity.GetMovementSpeed();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            _position.Value += _moveSpeed.Value * deltaTime * (_rotation.Value * Vector3.forward);
        }
    }
}