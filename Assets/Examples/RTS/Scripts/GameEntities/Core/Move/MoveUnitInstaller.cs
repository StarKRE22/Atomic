using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveUnitInstaller : IGameEntityInstaller
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 12;
        
        public void Install(IGameEntity entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveRequest(new Request<Vector3>());
            entity.AddMoveCommand(new Command<Vector3, float>()
                .AddCondition((_, _) => entity.IsAlive())
                .AddAction(entity.MoveStep)
                .AddAction(entity.RotateStep)
            );

            entity.AddMoveSpeed(_moveSpeed);
            entity.AddRotationSpeed(_rotationSpeed);
            // entity.AddBehaviour<MoveBehaviour>();
        }
    }
}