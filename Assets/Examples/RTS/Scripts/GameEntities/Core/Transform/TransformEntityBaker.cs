using System;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TransformEntityBaker : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Transform _transform;
        
        public void Install(IGameEntity entity)
        {
            entity.GetPosition().Value = _transform.position;
            entity.GetRotation().Value = _transform.rotation;
        }
    }
}