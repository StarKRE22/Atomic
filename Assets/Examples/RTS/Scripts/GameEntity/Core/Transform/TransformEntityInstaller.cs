using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TransformEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _scale = 1;
        
        public void Install(IGameEntity entity)
        {
            entity.AddPosition(new ReactiveVector3());
            entity.AddRotation(new ReactiveQuaternion());
            entity.AddScale(_scale);
            entity.AddBehaviour<TransformGizmos>();
        }
    }
}