using System;
using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [Serializable]
    public sealed class MultiTransformEntityInstaller : IEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _scale = 1;

        public void Install(IGameEntity entity)
        {
            entity.AddPosition(new ReactiveVariable<Vector3>());
            entity.AddRotation(new ReactiveVariable<Quaternion>());
            entity.AddScale(_scale);

#if UNITY_EDITOR
            entity.AddBehaviour<TransformGizmos>();
#endif
        }
    }
}