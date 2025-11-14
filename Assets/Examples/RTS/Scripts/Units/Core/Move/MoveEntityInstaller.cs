using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveEntityInstaller : IEntityInstaller<IUnit>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 12;
        
        public void Install(IUnit entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddMoveRequest(new Request<Vector3>());
            entity.AddMoveEvent(new Event<Vector3>());
            entity.WhenFixedTick(deltaTime =>
            {
                if (LifeUseCase.IsAlive(entity) &&
                    entity.GetMoveRequest().Consume(out Vector3 direction) &&
                    direction != Vector3.zero)
                {
                    MoveUseCase.MoveStep(entity, direction, deltaTime);
                    RotateUseCase.RotateStep(entity, direction, deltaTime);
                    entity.GetMoveEvent().Invoke(direction);
                }
            });
        }
    }
}