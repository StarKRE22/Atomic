using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed;

        [SerializeField]
        private Const<float> _rotationSpeed;
        
        public void Install(IGameEntity entity)
        {
            entity.SetMoveSpeed(_moveSpeed);
            entity.SetRotationSpeed(_rotationSpeed);
        }
    }
}