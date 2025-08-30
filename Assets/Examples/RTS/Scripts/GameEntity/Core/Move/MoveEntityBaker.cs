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
        private Optional<float> _moveSpeed;

        [SerializeField]
        private Optional<float> _rotationSpeed;

        public void Install(IGameEntity entity)
        {
            if (_moveSpeed) entity.SetMoveSpeed(new Const<float>(_moveSpeed));
            if (_rotationSpeed) entity.SetRotationSpeed(new Const<float>(_rotationSpeed));
        }
    }
}