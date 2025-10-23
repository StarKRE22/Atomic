using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class TransformEntityInstaller : IEntityInstaller<IUnitEntity>
    {
        [SerializeField]
        private Const<float> _scale = 1;

        public void Install(IUnitEntity entity)
        {
            entity.AddPosition(new ReactiveVector3());
            entity.AddRotation(new ReactiveQuaternion());
            entity.AddScale(_scale);

#if UNITY_EDITOR
            entity.AddBehaviour<TransformGizmos>();
#endif
        }
    }
}