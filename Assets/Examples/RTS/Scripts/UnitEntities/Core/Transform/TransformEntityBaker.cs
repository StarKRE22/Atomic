using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TransformEntityBaker : IEntityInstaller<IUnitEntity>
    {
        [SerializeField]
        private Transform _transform;
        
        public void Install(IUnitEntity entity)
        {
            entity.GetPosition().Value = _transform.position;
            entity.GetRotation().Value = _transform.rotation;
        }
    }
}