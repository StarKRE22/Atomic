using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraFollowController :
        IPlayerContextInit,
        IPlayerContextEnable,
        IPlayerContextLateTick
    {
        private readonly Vector3 _cameraOffset;
        private Transform _camera;
        private IEntity _character;

        public CameraFollowController(Vector3 cameraOffset)
        {
            _cameraOffset = cameraOffset;
        }

        public void Init(PlayerContext entity)
        {
            _camera = entity.GetCamera().transform;
            _character = entity.GetCharacter();
        }

        public void Enable(PlayerContext entity)
        {
            this.UpdatePosition();
        }

        public void LateTick(PlayerContext entity, float deltaTime)
        {
            this.UpdatePosition();
        }

        private void UpdatePosition()
        {
            _camera.position = _character.GetPosition().Value + _cameraOffset;
        }
    }
}