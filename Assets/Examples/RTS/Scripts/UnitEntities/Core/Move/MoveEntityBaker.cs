using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MoveEntityBaker : IEntityInstaller<IUnitEntity>
    {
        [SerializeField]
        private Optional<float> _moveSpeed;

        [SerializeField]
        private Optional<float> _rotationSpeed;

        public void Install(IUnitEntity entity)
        {
            if (_moveSpeed) entity.SetMoveSpeed(new Const<float>(_moveSpeed));
            if (_rotationSpeed) entity.SetRotationSpeed(new Const<float>(_rotationSpeed));
        }
    }
}