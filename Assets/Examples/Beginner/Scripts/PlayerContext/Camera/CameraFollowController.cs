using System.Runtime.CompilerServices;
using Atomic.Entities;
using UnityEngine;

namespace BeginnerGame
{
    public sealed class CameraFollowController :
        IEntityInit<IPlayerContext>,
        IEntityEnable,
        IEntityLateTick
    {
        private readonly Vector3 _cameraOffset;
        private Transform _camera;
        private IGameEntity _character;

        public CameraFollowController(Vector3 cameraOffset)
        {
            _cameraOffset = cameraOffset;
        }

        public void Init(IPlayerContext entity)
        {
            _camera = entity.GetCamera().transform;
            _character = entity.GetCharacter();
        }

        public void Enable(IEntity entity) => this.UpdatePosition();

        public void LateTick(IEntity entity, float deltaTime) => this.UpdatePosition();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void UpdatePosition()
        {
            _camera.position = _character.GetPosition().Value + _cameraOffset;
        }
    }
}