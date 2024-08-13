using System;
using Atomic.Contexts;
using Atomic.Elements;
using Atomic.Entities;

namespace GameExample.Engine
{
    [Serializable]
    public sealed class CameraFollowSystem : IContextInit, IContextLateUpdate
    {
        private IValue<IEntity> _character;
        private CameraData _cameraData;
        
        public void Init(IContext context)
        {
            _character = context.GetCharacter();
            _cameraData = context.GetCameraData();
            this.UpdatePosition();
        }

        public void LateUpdate(IContext context, float deltaTime)
        {
            this.UpdatePosition();
        }

        private void UpdatePosition()
        {
            _cameraData.transform.position = _character.Value.GetPosition().Value + _cameraData.offset;
        }
    }
}